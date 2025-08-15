using MartianRobots;

public class Grid
{
    public int Width { get; }
    public int Height { get; }

    // HashSet to drop scent markers | why?...
    // - only stores unique values
    // - fast lookups, uses Hash Table ~O(1) internally 
    // - additionally the tuple syntax is readable and concise
    private readonly HashSet<(int x, int y, char o)> _scents = new();

    public Grid(int width, int height, int MaxCoordinate)
    {
        if (width < 0 || width > MaxCoordinate)
            throw new ArgumentOutOfRangeException(nameof(width), $"Grid width must be between 0 and {MaxCoordinate}.");
        if (height < 0 || height > MaxCoordinate)
            throw new ArgumentOutOfRangeException(nameof(height), $"Grid height must be between 0 and {MaxCoordinate}.");

        Width = width;
        Height = height;
    }

    public bool IsInsideGrid(int x, int y)
        => x >= 0 && x <= Width && y >= 0 && y <= Height;

    public bool HasScent(int x, int y, char orientation)
        => _scents.Contains((x, y, orientation));

    public void LeaveScent(int x, int y, char orientation)
        => _scents.Add((x, y, orientation));
}
