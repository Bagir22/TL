using Spectre.Console;

namespace CarFactory.Utils;

public static class PromptHelper
{
    public static TEnum PromptSelection<TEnum>( string message, Dictionary<string, TEnum> options ) where TEnum : Enum
    {
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title( $"[blue]{message}[/]" )
                .PageSize( 14 )
                .AddChoices( options.Keys )
        );

        return options[ choice ];
    }
}