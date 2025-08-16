using MartianRobots.model;
using static MartianRobots.infrastructure.InputFileHandler;

namespace MartianRobots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InputModel model;
            try
            {
                model = ParseFile("input.txt");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse input: {ex.Message}");
                return;
            }

            // Constants
            const int MaxCoordinate = 50;

            // Inputs
            int gridWidth = model.Width;
            int gridHeight = model.Height;

            // Initialize
            var grid = new Grid(gridWidth, gridHeight, MaxCoordinate);
            var runner = new RobotRunner(grid);

            // Prime
            Console.WriteLine("Martian Robots");
            Console.WriteLine("-----------");
            Console.WriteLine($"Grid | Width: {gridWidth} Height: {gridHeight}");
            Console.WriteLine();

            // Execute
            var index = 1;
            foreach (var scenario in model.Scenarios)
            {
                var r = new Robot(scenario.StartX, scenario.StartY, scenario.Orientation, MaxCoordinate);
                Console.WriteLine($"Robot {index} input | {r.PositionX} {r.PositionY} {scenario.Orientation} | Instructions: {scenario.Instructions}");

                var result = runner.Run(r, scenario.Instructions);
                Console.WriteLine($"Robot {index} result: {result}");

                index++;
                Console.WriteLine();
            }

            // Clean & close up
            Console.WriteLine();
            Console.WriteLine("Program End: Press any key to exit");
            Console.ReadKey();
        }
    }
}
