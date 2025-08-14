using MartianRobots;

public class Grid
{
    private readonly int _width;
    private readonly int _height;

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public int Width => _width;
    public int Height => _height;

    public void Draw(Robot robot)
    {
        // Print from top to bottom
        for (int y = _height - 1; y >= 0; y--)
        {
            for (int x = 0; x < _width; x++)
            {
                if (x == robot.PositionX && y == robot.PositionY)
                    Console.Write(robot.Orientation);
                else
                    Console.Write(".");
            }
            Console.WriteLine();
        }
    }
}
