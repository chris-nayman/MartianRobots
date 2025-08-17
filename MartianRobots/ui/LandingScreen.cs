using Spectre.Console;

namespace MartianRobots.ui
{
    public enum LandingChoice
    {
        RunSampleCases,
        RunInteractiveMode,
        Exit
    }

    public static class LandingScreen
    {
        public static LandingChoice Show()
        {
            AnsiConsole.Clear();

            // Centered title
            AnsiConsole.Write(
                new FigletText("Martian Robots")
                    .Centered()
                    .Color(Color.Orange1));

            AnsiConsole.Write(new Rule().Centered().RuleStyle("grey37"));
            AnsiConsole.MarkupLine("[grey58]Use ↑/↓ to move and press [bold]Enter[/] to select.[/]");
            AnsiConsole.WriteLine();

            // Arrow-key selection prompt
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<LandingChoice>()
                    .Title("[bold orange1]Choose an option:[/]")
                    .HighlightStyle(new Style(foreground: Color.Orange1, decoration: Decoration.Bold))
                    .AddChoices(
                        LandingChoice.RunSampleCases,
                        LandingChoice.RunInteractiveMode,
                        LandingChoice.Exit)
                    .UseConverter(c => c switch
                    {
                        LandingChoice.RunSampleCases => "Run Sample Cases",
                        LandingChoice.RunInteractiveMode => "Run Interactive Mode",
                        LandingChoice.Exit => "Exit",
                        _ => c.ToString()
                    })
                    .PageSize(4)
            );

            return choice;
        }
    }
}
