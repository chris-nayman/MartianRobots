using MartianRobots.ui.ViewModels;
using Spectre.Console;

namespace MartianRobots.ui
{
    public static class SampleResultsScreen
    {
        public static void Show(ResultsViewModel vm)
        {
            AnsiConsole.Clear();

            // Title + divider
            AnsiConsole.Write(
                new FigletText("Martian Robots")
                    .Centered()
                    .Color(Color.Orange1));

            AnsiConsole.Write(new Rule("Sample Results").Centered().RuleStyle("grey37"));
            AnsiConsole.WriteLine();

            // Grid line
            AnsiConsole.MarkupLine($"[grey58]Grid |[/] Width: [bold]{vm.GridWidth}[/] Height: [bold]{vm.GridHeight}[/]");
            AnsiConsole.WriteLine();

            // For each robot
            foreach (var run in vm.Runs)
            {
                AnsiConsole.MarkupLine(
                    $"Robot {run.Index} input | [bold]{run.StartX} {run.StartY} {run.Orientation}[/] | Instructions: [bold]{run.Instructions}[/]");
                AnsiConsole.MarkupLine(
                    $"Robot {run.Index} result: [bold]{run.Result}[/]");
                AnsiConsole.WriteLine();
            }

            // End panel
            var end = new Panel("[grey58]Program End:[/] Press any key to return")
            {
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Grey)
            };
            AnsiConsole.Write(end);

            // Wait for key
            Console.ReadKey(true);
        }

        public static void ShowError(string message)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Martian Robots")
                    .Centered()
                    .Color(Color.Orange1));

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
