using MartianRobots.model;

namespace MartianRobots.Tests
{
    public class RobotTests
    {
        [Fact]
        public void Run_And_Get_Expected_Results_For_Sample_Scenarios()
        {
            // Arrange
            var grid = new Grid(5, 3, 50);
            var runner = new RobotRunner(grid);
            var r1 = new Robot(1, 1, 'E', 50);
            var r2 = new Robot(3, 2, 'N', 50);
            var r3 = new Robot(0, 3, 'W', 50);

            // Act
            runner.Run(r1, "RFRFRFRF");
            runner.Run(r2, "FRRFLLFFRRFLL");
            runner.Run(r3, "LLFFFLFLFL");

            // Assert
            Assert.Equal("1 1 E", r1.ToString());
            Assert.Equal("3 3 N LOST", r2.ToString());
            Assert.Equal("2 3 S", r3.ToString());
        }       
    }
}
