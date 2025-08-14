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
            var grid = new Grid(gridWidth, gridHeight);

            Console.WriteLine(robot);
            grid.Draw(robot);
            Console.WriteLine();

            foreach (var i in instructions)
            {
                robot.ExecuteCommand(i);
                Console.WriteLine(robot);
                grid.Draw(robot);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Program End");
        }
    }
}
