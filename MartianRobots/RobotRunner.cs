using MartianRobots;

public class RobotRunner
{
    private readonly Grid _grid;

    public RobotRunner(Grid grid)
    {
        _grid = grid;
    }

    public string Run(Robot robot, string instructions)
    {
        foreach (var i in instructions)
        {
            if (robot.IsLost) break;

            switch (i)
            {
                case 'L':
                    robot.TurnLeft();
                    break;
                case 'R':
                    robot.TurnRight();
                    break;
                case 'F':
                    {
                        var (nx, ny) = robot.NextForwardMovePosition();

                        if (!_grid.IsInsideGrid(nx, ny))
                        {
                            // If there is a scent at the current cell+orientation combo, do nothing.
                            if (_grid.HasScent(robot.PositionX, robot.PositionY, robot.Orientation))
                            {
                                continue;
                            }

                            // Otherwise the robot is LOST, leave a scent and move to the next instruction
                            _grid.LeaveScent(robot.PositionX, robot.PositionY, robot.Orientation);
                            robot.IsLost = true;
                        }
                        else
                        {
                            robot.MoveForward(nx, ny);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException($"Invalid instruction: {i}");
            }
        }

        return robot.ToString();
    }
}
