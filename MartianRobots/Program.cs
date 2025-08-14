namespace MartianRobots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // INPUT
            // 5 3 - Grid size
            // 1 1 E - Robot start position + Orientation
            // RFRFRFRF - Instructions for Robot

            // 3 2 N - Robot start position + Orientation
            // FRRFLLFFRRFLL - Instructions for Robot

            // 0 3 W - Robot start position + Orientation
            // LLFFFLFLFL - Instructions for Robot

            // ----------------------------------------------

            // OUTPUT (Robot end coord + orientation + LOST if therobot fell off the grid)
            // 1 1 E
            // 3 3 N LOST
            // 2 3 S

            // ----------------------------------------------


            int gridWidth = 5;
            int gridHeight = 3;

            var instructions = "RFRFRFRF";

            var robot = new Robot(1, 1, 'E');

            Console.WriteLine(robot.ToString());
            DrawGrid(gridWidth, gridHeight, robot);
            Console.WriteLine();

            foreach (var i in instructions)
            {
                robot.ExecuteCommand(i);
                Console.WriteLine(robot.ToString());
                DrawGrid(gridWidth, gridHeight, robot);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Program End");
        }

        static void DrawGrid(int maxX, int maxY, Robot r)
        {
            for (int y = maxY; y > 0; y--) // top row first
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (x == r.PositionX && y == r.PositionY+1)
                        Console.Write(r.Orientation);
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }
    }

    public class Robot
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Orientation { get; set; }

        public Robot(int positionX, int positionY, char orientation)
        {
            PositionX = positionX;
            PositionY = positionY;
            Orientation = orientation;
        }        

        public void ExecuteCommand(char instruction)
        {
            switch (instruction)
            {
                case 'L':
                    Orientation = TurnLeft(Orientation);
                    break;

                case 'R':
                    Orientation = TurnRight(Orientation);
                    break;

                case 'F':
                    MoveForward();
                    break;

                default:
                    throw new ArgumentException($"Invalid instruction: {instruction}");
            }
        }

        private char TurnLeft(char current)
        {
            return current switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => current
            };
        }

        private char TurnRight(char current)
        {
            return current switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => current
            };
        }

        private void MoveForward()
        {
            switch (Orientation)
            {
                case 'N': PositionY++; break;
                case 'S': PositionY--; break;
                case 'E': PositionX++; break;
                case 'W': PositionX--; break;
            }
        }

        public override string ToString()
        {
            return $"{PositionX} {PositionY} {Orientation}";             
        }
    }
}
