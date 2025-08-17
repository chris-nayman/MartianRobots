using MartianRobots.model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.ui
{
    public class InputScreens
    {
        public static (InputModel? Model, string? Error) BuildModelFromConsole(int maxCoord)
        {
            try
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("Martian Robots").Centered().Color(Color.Orange1));
                AnsiConsole.Write(new Rule("Interactive Setup").Centered().RuleStyle("grey37"));
                AnsiConsole.WriteLine();

                // Grid
                var width = AnsiConsole.Prompt(
                    new TextPrompt<int>($"Grid width: 0-{maxCoord}")
                        .Validate(v => v is >= 0 and <= 50
                            ? ValidationResult.Success()
                            : ValidationResult.Error($"Enter a value between 0 and {maxCoord}")));

                var height = AnsiConsole.Prompt(
                    new TextPrompt<int>($"Grid height: 0-{maxCoord}")
                        .Validate(v => v is >= 0 and <= 50
                            ? ValidationResult.Success()
                            : ValidationResult.Error($"Enter a value between 0 and {maxCoord}")));

                // Scenarios
                var scenarios = new List<Scenario>();
                do
                {
                    var x = AnsiConsole.Prompt(
                        new TextPrompt<int>($"Robot start X: 0-{width}")
                            .Validate(v => v is >= 0 && v <= width
                                ? ValidationResult.Success()
                                : ValidationResult.Error($"Enter a value between 0 and {width}")));

                    var y = AnsiConsole.Prompt(
                        new TextPrompt<int>($"Robot start Y: 0-{height}")
                            .Validate(v => v is >= 0 && v <= height
                                ? ValidationResult.Success()
                                : ValidationResult.Error($"Enter a value between 0 and {height}")));

                    var orientation = AnsiConsole.Prompt(
                        new SelectionPrompt<char>()
                            .Title("Orientation")
                            .AddChoices('N', 'E', 'S', 'W'));

                    var instructions = AnsiConsole.Prompt(
                        new TextPrompt<string>("Instructions ([bold]L/R/F[/], < 100 chars)")
                            .Validate(s =>
                            {
                                bool ok = s.Length > 0
                                       && s.Length < 100 // spec: less than 100
                                       && s.All(c => c is 'L' or 'R' or 'F');
                                return ok
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("Use only L/R/F and keep length < 100.");
                            }));

                    scenarios.Add(new Scenario(x, y, orientation, instructions));

                } while (AnsiConsole.Confirm("Add another robot?"));

                var model = new InputModel
                {
                    Width = width,
                    Height = height,
                    Scenarios = scenarios
                };

                return (model, null);
            }
            catch (Exception ex)
            {
                return (null, $"Interactive input failed: {ex.Message}");
            }
        }
    }
}
