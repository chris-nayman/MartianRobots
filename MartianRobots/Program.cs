namespace MartianRobots
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            // Inputs
            int gridWidth = 5;
            int gridHeight = 3;
            var instructions = "RFRFRFRF";

            // Initialize
            var robot = new Robot(1, 1, 'E');
            var grid = new Grid(gridWidth, gridHeight);
            var runner = new RobotRunner(grid);

            // Prime
            Console.WriteLine(robot);
            grid.Draw(robot);
            Console.WriteLine();

            // Execute
            var result = runner.Run(robot, instructions);

            Console.WriteLine(result);
            //grid.Draw(robot);
            //Console.WriteLine();

            // Clean up
            Console.WriteLine();
            Console.WriteLine("Program End");
        }
    }
}
