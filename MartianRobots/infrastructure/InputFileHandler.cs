using MartianRobots.model;
using System.Globalization;

namespace MartianRobots.infrastructure
{
    public static partial class InputFileHandler
    {
        const int MaxInstructionString = 100;        

        public static InputModel ParseFile(string path)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, path);
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Input file not found.", filePath);

            var rawLines = File.ReadAllLines(filePath);

            // Clean up: trim, drop empty lines
            var lines = new List<string>();
            foreach (var raw in rawLines)
            {
                var s = raw.Trim();
                if (string.IsNullOrWhiteSpace(s)) continue;
                lines.Add(s);
            }

            if (lines.Count < 1)
                throw new InvalidDataException("Input file is empty.");

            // Line 1: grid size
            var (width, height) = ParseGridSize(lines[0]);

            var model = new InputModel { Width = width, Height = height };

            // Get Robot start position & instructions
            for (int i = 1; i < lines.Count; i += 2)
            {
                if (i + 1 >= lines.Count)
                    throw new InvalidDataException("Robot position without instructions at end of file.");

                var (x, y, o) = ParseRobotStart(lines[i]);
                var instructions = ParseInstructions(lines[i + 1]);

                model.Scenarios.Add(new Scenario(x, y, o, instructions));
            }

            return model;
        }

        private static (int width, int height) ParseGridSize(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                throw new InvalidDataException($"Invalid grid data: '{line}'");

            int width = int.Parse(parts[0], CultureInfo.InvariantCulture);
            int height = int.Parse(parts[1], CultureInfo.InvariantCulture);

            if (width < 0 || height < 0) // TODO: Should make the minimum dimentions 2 
                throw new InvalidDataException("Grid dimensions must be non-negative.");

            return (width, height);
        }

        private static (int x, int y, char o) ParseRobotStart(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
                throw new InvalidDataException($"Invalid robot start data: '{line}'");

            int x = int.Parse(parts[0], CultureInfo.InvariantCulture);
            int y = int.Parse(parts[1], CultureInfo.InvariantCulture);
            char o = parts[2][0];

            if ("NESW".IndexOf(o) < 0)
                throw new InvalidDataException($"Invalid orientation '{o}' in line: '{line}'");

            return (x, y, o);
        }

        private static string ParseInstructions(string line)
        {
            if (line.Length > MaxInstructionString)
                throw new InvalidDataException(
                    $"Instruction string exceeds {MaxInstructionString} characters {line.Length}.");

            // Allow only L, R, F 
            // TODO: Update this after refactoring in the Command or Strategy pattern to easily allow new instruction types
            foreach (var c in line)
            {
                if (c is not ('L' or 'R' or 'F'))
                    throw new InvalidDataException($"Invalid instruction character '{c}' in '{line}'");
            }
            return line;
        }
    }
}
