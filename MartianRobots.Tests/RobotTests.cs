using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Tests
{
    public class RobotTests
    {
        [Theory]
        [InlineData(5, 3, 1, 1, 'E', "RFRFRFRF", "1 1 E")]
        // [InlineData(5, 3, 3, 2, 'N', "FRRFLLFFRRFLL", "3 3 N LOST")]
        // [InlineData(5, 3, 0, 3, 'W', "LLFFFLFLFL", "2 3 S")]
        public void Run_And_Get_Expected_Results_For_Sample_Scenarios(
            int gridWidth, int gridHeight, int robotStartX, int robotStartY, 
            char startOrientation, string instructions, string expected)
        {
            // Arrange
            var grid = new Grid(gridWidth, gridHeight);
            var robot = new Robot(robotStartX, robotStartY, startOrientation);

            // Act
            foreach (var i in instructions)
            {
                robot.ExecuteCommand(i);
            }

            // Assert
            Assert.Equal(expected, robot.ToString());
        }
    }
}
