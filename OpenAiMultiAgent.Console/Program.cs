using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAiMultiAgent.Console;

#pragma warning disable SKEXP0110

var model = Environment.GetEnvironmentVariable("OPENAI_MODEL") ?? throw new InvalidOperationException("OPENAI_MODEL environment variable is not set.");
var endpoint = Environment.GetEnvironmentVariable("OPENAI_ENDPOINT") ?? throw new InvalidOperationException("OPENAI_ENDPOINT environment variable is not set.");
var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException("OPENAI_API_KEY environment variable is not set.");

// Create a kernel with Azure OpenAI chat completion
var kernel = Kernel
    .CreateBuilder()
    .AddAzureOpenAIChatCompletion(model, endpoint, apiKey)
    .Build();

ChatCompletionAgent htmlAgent = Agents.CreateHtmlAgent(kernel);
ChatCompletionAgent cssAgent = Agents.CreateCssAgent(kernel);
ChatCompletionAgent jsAgent = Agents.CreateJsAgent(kernel);
ChatCompletionAgent techLead = Agents.CreateTechLead(kernel);

KernelFunction webAppTerminateFunction = KernelFunctionFactory.CreateFromPrompt(Constants.WebAppTerminatePrompt);
KernelFunction webAppSelectionFunction = KernelFunctionFactory.CreateFromPrompt(Constants.WebAppSelectionPrompt);

AgentGroupChat webAppChat = new(techLead, htmlAgent, cssAgent, jsAgent)
{
    ExecutionSettings = new()
    {
        TerminationStrategy = new KernelFunctionTerminationStrategy(webAppTerminateFunction, kernel)
        {
            Agents = [techLead],
            ResultParser = (result) => result.GetValue<string>()?.Contains("DONE", StringComparison.OrdinalIgnoreCase) ?? false,
            HistoryVariableName = "history",
            MaximumIterations = 5
        },
        SelectionStrategy = new KernelFunctionSelectionStrategy(webAppSelectionFunction, kernel)
        {
            AgentsVariableName = "agents",
            HistoryVariableName = "history"
        }
    }
};

string webAppPrompt = """"
                      Create a beautiful calculator with a modern and user-friendly interface. The calculator should support basic arithmetic operations (addition, subtraction, multiplication, division) and have a responsive design that works well on both desktop and mobile devices.
                      """";

webAppChat.AddChatMessage(new ChatMessageContent(AuthorRole.User, webAppPrompt));
await foreach (var content in webAppChat.InvokeAsync())
{
    Console.WriteLine();
    Console.WriteLine($"-> {content.AuthorName}:");
    Console.WriteLine();
    Console.WriteLine($"{content.Content}");
    Console.WriteLine();
}

var isCompleted = webAppChat.IsComplete ? "YES" : "NO!";
Console.WriteLine($"Chat with agents was completed? {isCompleted}");