using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
#pragma warning disable SKEXP0110

namespace OpenAiMultiAgent.Console;

public static class Agents
{
    public static ChatCompletionAgent CreateHtmlAgent(Kernel kernel)
    {
        return new ChatCompletionAgent
        {
            Name = Constants.HtmlAgentName,
            Instructions = Constants.HtmlAgentInstructions,
            Kernel = kernel
        };
    }

    public static ChatCompletionAgent CreateCssAgent(Kernel kernel)
    {
        return new ChatCompletionAgent
        {
            Name = Constants.CssAgentName,
            Instructions = Constants.CssAgentInstructions,
            Kernel = kernel
        };
    }

    public static ChatCompletionAgent CreateJsAgent(Kernel kernel)
    {
        return new ChatCompletionAgent
        {
            Name = Constants.JsAgentName,
            Instructions = Constants.JsAgentInstructions,
            Kernel = kernel
        };
    }

    public static ChatCompletionAgent CreateTechLead(Kernel kernel)
    {
        return new ChatCompletionAgent
        {
            Name = Constants.TechLeadName,
            Instructions = Constants.TechLeadInstructions,
            Kernel = kernel
        };
    }
}