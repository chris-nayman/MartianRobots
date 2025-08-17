namespace MartianRobots.model
{
    // Chose a record over class or struct here as it is immutable by default
    public record Scenario(int StartX, int StartY, char Orientation, string Instructions);

    public class InputModel
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public List<Scenario>? Scenarios { get; set; } = new();
    }
}
