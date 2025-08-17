using MartianRobots.ui.ViewModels;
using Spectre.Console;

namespace MartianRobots.ui
{
    public static class ConsoleResultsScreen
    {
        public static void Show(ResultsViewModel vm)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("Martian Robots")
                    .Centered()
                    .Color(Color.Orange1));

            AnsiConsole.Write(new Rule("Interactive Results").Centered().RuleStyle("grey37"));
            AnsiConsole.WriteLine();

            // Grid header
            AnsiConsole.MarkupLine($"[grey58]Grid |[/] Width: [bold]{vm.GridWidth}[/] Height: [bold]{vm.GridHeight}[/]");
            AnsiConsole.WriteLine();

            if (vm.Runs.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No robots were provided.[/]");
            }
            else
            {
                foreach (var run in vm.Runs)
                {
                    var instr = Markup.Escape(run.Instructions ?? string.Empty);
                    var orient = Markup.Escape(run.Orientation ?? string.Empty);
                    var result = Markup.Escape(run.Result ?? string.Empty);

                    AnsiConsole.MarkupLine(
                        $"Robot {run.Index} input | [bold]{run.StartX} {run.StartY} {orient}[/] | Instructions: [bold]{instr}[/]");
                    AnsiConsole.MarkupLine(
                        run.Lost
                            ? $"Robot {run.Index} result: [bold]{result}[/]"
                            : $"Robot {run.Index} result: [bold]{result}[/]");
                    AnsiConsole.WriteLine();
                }
            }

            var end = new Panel("[grey58]Done:[/] Press any key to return to menu")
            {
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Grey)
            };
            AnsiConsole.Write(end);

            Console.ReadKey(true);
        }

        public static void ShowError(string message)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
                new FigletText("Martian Robots")
                    .Centered()
                    .Color(Color.Orange1));

            AnsiConsole.Write(new Rule("Interactive Mode").Centered().RuleStyle("red"));

            var panel = new Panel($"[red]Error:[/] {Markup.Escape(message)}")
            {
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Red)
            };
            AnsiConsole.Write(panel);

            AnsiConsole.MarkupLine("[grey58]Press any key to return to menu[/]");
            Console.ReadKey(true);
        }
    }
}
