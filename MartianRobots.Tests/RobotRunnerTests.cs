using MartianRobots.model;

namespace MartianRobots.Tests
{
    public class RobotRunnerTests
    {
        [Fact]
        public void Run_SampleCase1_ReturnsExpected()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);
            var robot = new Robot(1, 1, 'E', 50);

            var result = runner.Run(robot, "RFRFRFRF");

            Assert.Equal("1 1 E", result);
            Assert.False(robot.IsLost);
        }

        [Fact]
        public void Run_SampleCase2_ReturnsExpected_LeavesScent()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);
            var robot = new Robot(3, 2, 'N', 50);

            var result = runner.Run(robot, "FRRFLLFFRRFLL");

            Assert.Equal("3 3 N LOST", result);
            Assert.True(robot.IsLost);
            // Scent should be on the last valid cell with that orientation
            Assert.True(grid.HasScent(3, 3, 'N'));
        }

        [Fact]
        public void Run_SampleCase3_RespectsScent_ReturnsExpected()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);

            // First robot leaves scent at (3,3,N)
            var r1 = new Robot(3, 2, 'N', 50);
            runner.Run(r1, "FRRFLLFFRRFLL");
            Assert.True(grid.HasScent(3, 3, 'N'));

            // Third sample case
            var r3 = new Robot(0, 3, 'W', 50);
            var result = runner.Run(r3, "LLFFFLFLFL");

            Assert.Equal("2 3 S", result);
            Assert.False(r3.IsLost);
        }

        [Fact]
        public void Run_MoveWithinBounds_EndsAtExpectedPosition()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);
            var robot = new Robot(0, 0, 'N', 50);

            var result = runner.Run(robot, "FFRFF");

            Assert.Equal("2 2 E", result);
            Assert.False(robot.IsLost);
        }

        [Fact]
        public void Run_OffGrid_LostAndStopsProcessingFurtherInstructions()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);
            var robot = new Robot(5, 3, 'N', 50);

            var result = runner.Run(robot, "FLLL"); // first F goes off grid; L's should be ignored

            Assert.Equal("5 3 N LOST", result);
            Assert.True(robot.IsLost);
            // Orientation should remain the one that caused the loss
            Assert.Equal('N', robot.Orientation);
        }

        //[Fact]
        //public void Run_ScentPreventsSecondLoss_IgnoresForward_ThenContinues()
        //{
        //    var grid = new Grid(5, 3, 50);
        //    var runner = new RobotRunner(grid);

        //    // First robot at (0,0,S) moves off grid, leaves scent
        //    var r1 = new Robot(0, 0, 'S', 50);
        //    var res1 = runner.Run(r1, "F");
        //    Assert.Equal("0 0 S LOST", res1);
        //    Assert.True(grid.HasScent(0, 0, 'S'));

        //    // Second robot attempts same F from same scented cell/orientation:
        //    // F should be ignored, then R turns to E, F moves to (1,0)
        //    var r2 = new Robot(0, 0, 'S', 50);
        //    var res2 = runner.Run(r2, "FRF");

        //    Assert.Equal("1 0 E", res2);
        //    Assert.False(r2.IsLost);
        //}

        [Fact]
        public void Run_InvalidInstruction_Throws()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);
            var robot = new Robot(1, 1, 'E', 50);

            var ex = Assert.Throws<ArgumentException>(() => runner.Run(robot, "FRAX")); // 'A' invalid
            Assert.Contains("Invalid instruction", ex.Message);
        }

        [Fact]
        public void Run_EmptyInstructions_ReturnsInitialState()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);
            var robot = new Robot(2, 2, 'W', 50);

            var result = runner.Run(robot, string.Empty);

            Assert.Equal("2 2 W", result);
            Assert.False(robot.IsLost);
        }

        [Fact]
        public void Run_DoesNotChangePositionWhenScentBlocksForward()
        {
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);

            // Leave scent at (0,0,S)
            var r1 = new Robot(0, 0, 'S', 50);
            runner.Run(r1, "F");
            Assert.True(grid.HasScent(0, 0, 'S'));

            // Second robot: first F should be ignored due to scent; stays put until next commands
            var r2 = new Robot(0, 0, 'S', 50);
            var result = runner.Run(r2, "F"); // only F, so ends where it started

            Assert.Equal("0 0 S", result);
            Assert.False(r2.IsLost);
            Assert.Equal(0, r2.PositionX);
            Assert.Equal(0, r2.PositionY);
            Assert.Equal('S', r2.Orientation);
        }
    }
}
