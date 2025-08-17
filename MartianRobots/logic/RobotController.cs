using MartianRobots.model;
using MartianRobots.model.contracts;
using MartianRobots.ui.ViewModels;

public class RobotController
{
    public static (ResultsViewModel? ViewModel, string? Error) Execute(IInputProvider provider, int maxCoordinate = 50)
    {
        // TODO: move maxCoordinate to AppSettings

        // Able to inject an InputProvider here. 
        // For this implemetation I have added ones for file & console
        var (model, readError) = provider.Get();
        
        if (readError is not null)
            return (null, readError);

        if (model is null)
            return (null, "No input was provided");
               
        int gridWidth = model.Width;
        int gridHeight = model.Height;

        var grid = new Grid(gridWidth, gridHeight, maxCoordinate);
        var runner = new RobotRunner(grid);

        var vm = new ResultsViewModel
        {
            GridWidth = gridWidth,
            GridHeight = gridHeight
        };

        var index = 1;
        foreach (var scenario in model.Scenarios)
        {
            var robot = new Robot(scenario.StartX, scenario.StartY, scenario.Orientation, maxCoordinate);
            var result = runner.Run(robot, scenario.Instructions);

            vm.Runs.Add(new RobotRunViewModel
            {
                Index = index++,
                StartX = scenario.StartX,
                StartY = scenario.StartY,
                Orientation = scenario.Orientation.ToString(),
                Instructions = scenario.Instructions,
                Result = result,
                Lost = robot.IsLost
            });
        }

        return (vm, null);
    }
}