using MartianRobots;

public class Grid
{
    private readonly int _width;
    private readonly int _height;

    // HashSet to drop scent markers | why?...
    // - only stores unique values
    // - fast lookups, uses Hash Table ~O(1) internally 
    // - additionally the tuple syntax is readable and concise
    private readonly HashSet<(int x, int y, char o)> _scents = new();

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public int Width => _width;
    public int Height => _height;

    public bool IsInsideGrid(int x, int y)
        => x >= 0 && x <= Width && y >= 0 && y <= Height;

    public bool HasScent(int x, int y, char orientation)
        => _scents.Contains((x, y, orientation));

    public void LeaveScent(int x, int y, char orientation)
        => _scents.Add((x, y, orientation));

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
