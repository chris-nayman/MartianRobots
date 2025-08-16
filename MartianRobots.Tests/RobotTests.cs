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

        [Theory]
        [InlineData(0, 0, 'N', 50)]
        [InlineData(5, 3, 'E', 50)]
        [InlineData(50, 50, 'S', 50)] // on the max edge is allowed
        public void Constructor_ValidInputs_SetsProperties(int x, int y, char o, int max)
        {
            var r = new Robot(x, y, o, max);

            Assert.Equal(x, r.PositionX);
            Assert.Equal(y, r.PositionY);
            Assert.Equal(o, r.Orientation);
            Assert.False(r.IsLost);
        }

        [Theory]
        [InlineData(51, 0, 'N', 50)]
        [InlineData(999, 0, 'N', 50)]
        public void Constructor_XGreaterThanMax_Throws(int x, int y, char o, int max)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Robot(x, y, o, max));
            Assert.Equal("positionX", ex.ParamName);
        }

        [Theory]
        [InlineData(0, 51, 'N', 50)]
        [InlineData(0, 999, 'N', 50)]
        public void Constructor_YGreaterThanMax_Throws(int x, int y, char o, int max)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Robot(x, y, o, max));
            Assert.Equal("positionY", ex.ParamName);
        }

        [Fact]
        public void Constructor_AllowsNegativePositions_AsImplemented()
        {
            // Current implementation doesn’t guard against negatives.
            var r = new Robot(-1, -2, 'N', 50);
            Assert.Equal(-1, r.PositionX);
            Assert.Equal(-2, r.PositionY);
        }

        [Theory]
        [InlineData('N', 'W')]
        [InlineData('W', 'S')]
        [InlineData('S', 'E')]
        [InlineData('E', 'N')]
        public void TurnLeft_RotatesCorrectly(char start, char expected)
        {
            var r = new Robot(0, 0, start, 50);
            r.TurnLeft();
            Assert.Equal(expected, r.Orientation);
        }

        [Theory]
        [InlineData('N', 'E')]
        [InlineData('E', 'S')]
        [InlineData('S', 'W')]
        [InlineData('W', 'N')]
        public void TurnRight_RotatesCorrectly(char start, char expected)
        {
            var r = new Robot(0, 0, start, 50);
            r.TurnRight();
            Assert.Equal(expected, r.Orientation);
        }

        [Fact]
        public void TurnLeftRight_FullCycles()
        {
            var r = new Robot(0, 0, 'N', 50);

            r.TurnLeft();  // W
            r.TurnLeft();  // S
            r.TurnLeft();  // E
            r.TurnLeft();  // N
            Assert.Equal('N', r.Orientation);

            r.TurnRight(); // E
            r.TurnRight(); // S
            r.TurnRight(); // W
            r.TurnRight(); // N
            Assert.Equal('N', r.Orientation);
        }

        [Theory]
        [InlineData(1, 1, 'N', 1, 2)]
        [InlineData(1, 1, 'S', 1, 0)]
        [InlineData(1, 1, 'E', 2, 1)]
        [InlineData(1, 1, 'W', 0, 1)]
        public void NextForwardMovePosition_FromEachOrientation(int x, int y, char o, int nx, int ny)
        {
            var r = new Robot(x, y, o, 50);
            var (fx, fy) = r.NextForwardMovePosition();
            Assert.Equal(nx, fx);
            Assert.Equal(ny, fy);
        }

        [Fact]
        public void NextForwardMovePosition_UnknownOrientation_NoChange()
        {
            var r = new Robot(2, 3, 'X', 50);
            var (fx, fy) = r.NextForwardMovePosition();
            Assert.Equal(2, fx);
            Assert.Equal(3, fy);
        }

        [Fact]
        public void MoveForward_SetsNewPosition()
        {
            var r = new Robot(0, 0, 'N', 50);
            r.MoveForward(5, 7);
            Assert.Equal(5, r.PositionX);
            Assert.Equal(7, r.PositionY);
        }

        [Fact]
        public void MovementSequence_UsingNextThenMoveForward_UpdatesState()
        {
            var r = new Robot(1, 1, 'E', 50);

            // Equivalent to RFRFRFRF from sample: end at (1,1,E)
            r.TurnRight();                                      // S
            var (x, y) = r.NextForwardMovePosition(); r.MoveForward(x, y); // (1,0)
            r.TurnRight();                                      // W
            (x, y) = r.NextForwardMovePosition(); r.MoveForward(x, y);     // (0,0)
            r.TurnRight();                                      // N
            (x, y) = r.NextForwardMovePosition(); r.MoveForward(x, y);     // (0,1)
            r.TurnRight();                                      // E
            (x, y) = r.NextForwardMovePosition(); r.MoveForward(x, y);     // (1,1)

            Assert.Equal(1, r.PositionX);
            Assert.Equal(1, r.PositionY);
            Assert.Equal('E', r.Orientation);
            Assert.False(r.IsLost);
        }

        [Fact]
        public void ToString_Formats_AsExpected()
        {
            var r = new Robot(3, 3, 'N', 50);
            Assert.Equal("3 3 N", r.ToString());

            r.IsLost = true;
            Assert.Equal("3 3 N LOST", r.ToString());
        }
    }
}
