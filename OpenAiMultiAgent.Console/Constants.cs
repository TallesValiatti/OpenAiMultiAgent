namespace OpenAiMultiAgent.Console;

public static class Constants
{
    public const string HtmlAgentName = "HtmlAgent";
    public const string HtmlAgentInstructions = """
        You are an expert in HTML. Your goal is to create the HTML structure for a web application based on the user requirements.
        You should only focus on the HTML part and not include any CSS or JavaScript.
        You must only output the code.
        """;

    public const string CssAgentName = "CssAgent";
    public const string CssAgentInstructions = """
        You are an expert in CSS. Your goal is to create the CSS styles for a web application based on the user requirements.
        You should only focus on the CSS part and not include any HTML or JavaScript.
        You must only output the code.
        """;

    public const string JsAgentName = "JsAgent";
    public const string JsAgentInstructions = """
        You are an expert in JavaScript. Your goal is to create the JavaScript functionality for a web application based on the user requirements.
        You should only focus on the JavaScript part and not include any HTML or CSS.
        You must only output the code.
        """;

    public const string TechLeadName = "TechLead";
    public const string TechLeadInstructions = """
        You are a tech lead. Your goal is to integrate the HTML, CSS, and JavaScript parts into a single web application file.
        You should ensure that all parts work together seamlessly.
        """;

    public const string WebAppSelectionPrompt =
        $$$"""
           Your job is to determine which participant takes the next turn in a conversation according to the action of the most recent participant.
           State only the name of the participant to take the next turn.

           Choose only from these participants:
           - TechLead
           - HtmlAgent
           - CssAgent
           - JsAgent

           Always follow these steps when selecting the next participant:
           1) After user input, it is HtmlAgent's turn.
           2) After HtmlAgent replies, it's CssAgent's turn.
           3) After CssAgent replies, it's JsAgent's turn.
           4) After JsAgent replies, it's TechLead's turn to integrate the parts.
           5) If the web application is successfully built, the conversation ends.
           6) If not, it's HtmlAgent's turn again.

           History:
           {{$history}}
           """;

    public const string WebAppTerminatePrompt = $$$"""
           Determine if the web application has been successfully built. If so, respond with a single word: DONE.

           History:

           {{$history}}
           """;
}