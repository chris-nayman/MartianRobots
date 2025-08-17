namespace MartianRobots.ui.ViewModels
{
    public sealed class ResultsViewModel
    {
        public int GridWidth { get; init; }
        public int GridHeight { get; init; }
        public List<RobotRunViewModel> Runs { get; } = new();
    }

    public sealed class RobotRunViewModel
    {
        public int Index { get; init; }

        // Input
        public int StartX { get; init; }
        public int StartY { get; init; }
        public string Orientation { get; init; } = "";
        public string Instructions { get; init; } = "";

        // Output
        public string Result { get; init; } = "";
        public bool Lost { get; init; }
    }
}
