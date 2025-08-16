using System;
using Xunit;
using MartianRobots;

namespace MartianRobots.Tests
{
    public class GridTests
    {
        [Theory]
        [InlineData(2, 2, 50)]
        [InlineData(5, 3, 50)]
        [InlineData(50, 50, 50)]
        public void Constructor_ValidSizes_SetsWidthAndHeight(int w, int h, int max)
        {
            var grid = new Grid(w, h, max);

            Assert.Equal(w, grid.Width);
            Assert.Equal(h, grid.Height);
        }

        [Theory]
        // too small
        [InlineData(-1, 2, 50)]
        [InlineData(0, 2, 50)]
        [InlineData(1, 2, 50)]
        // too large
        [InlineData(51, 2, 50)]
        [InlineData(999, 2, 50)]
        public void Constructor_WidthOutOfRange_Throws(int w, int h, int max)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(w, h, max));
            Assert.Equal("width", ex.ParamName);
        }

        [Theory]
        // too small
        [InlineData(2, -1, 50)]
        [InlineData(2, 0, 50)]
        [InlineData(2, 1, 50)]
        // too large
        [InlineData(2, 51, 50)]
        [InlineData(2, 999, 50)]
        public void Constructor_HeightOutOfRange_Throws(int w, int h, int max)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(w, h, max));
            Assert.Equal("height", ex.ParamName);
        }

        [Theory]
        // corners and inside for a 5x3 grid
        [InlineData(0, 0, 5, 3, true)]
        [InlineData(5, 0, 5, 3, true)]
        [InlineData(0, 3, 5, 3, true)]
        [InlineData(5, 3, 5, 3, true)]
        [InlineData(2, 2, 5, 3, true)]
        // out of bounds (negative)
        [InlineData(-1, 0, 5, 3, false)]
        [InlineData(0, -1, 5, 3, false)]
        // out of bounds (beyond max)
        [InlineData(6, 2, 5, 3, false)]
        [InlineData(2, 4, 5, 3, false)]
        public void IsInsideGrid_ReturnsExpected(int x, int y, int w, int h, bool expected)
        {
            var grid = new Grid(w, h, 50);

            Assert.Equal(expected, grid.IsInsideGrid(x, y));
        }

        [Fact]
        public void LeaveScent_ThenHasScent_IsTrue()
        {
            var grid = new Grid(5, 3, 50);

            grid.LeaveScent(1, 1, 'N');

            Assert.True(grid.HasScent(1, 1, 'N'));
        }

        [Fact]
        public void HasScent_IsOrientationSpecific()
        {
            var grid = new Grid(5, 3, 50);

            grid.LeaveScent(2, 2, 'E');

            Assert.True(grid.HasScent(2, 2, 'E'));
            Assert.False(grid.HasScent(2, 2, 'N')); // different orientation
            Assert.False(grid.HasScent(2, 1, 'E')); // different coordinate
        }

        [Fact]
        public void LeaveScent_CanBeCalledMultipleTimes_IdempotentBehavior()
        {
            var grid = new Grid(5, 3, 50);

            grid.LeaveScent(3, 3, 'S');
            grid.LeaveScent(3, 3, 'S'); // still present, should not throw

            Assert.True(grid.HasScent(3, 3, 'S'));
        }

        [Theory]
        [InlineData('N')]
        [InlineData('E')]
        [InlineData('S')]
        [InlineData('W')]
        public void HasScent_RecognizesAllCompassOrientations(char o)
        {
            var grid = new Grid(5, 3, 50);

            grid.LeaveScent(4, 1, o);

            Assert.True(grid.HasScent(4, 1, o));
        }

        [Fact]
        public void IsInsideGrid_InclusiveUpperBounds()
        {
            var grid = new Grid(50, 50, 50);

            Assert.True(grid.IsInsideGrid(50, 50)); // inclusive
            Assert.False(grid.IsInsideGrid(51, 50));
            Assert.False(grid.IsInsideGrid(50, 51));
        }

        [Fact]
        public void MinSizedGrid_IsAtLeastTwoByTwo_AndInclusive()
        {
            var grid = new Grid(2, 2, 50);
            // All corners and inside are valid: (0..2, 0..2)
            Assert.True(grid.IsInsideGrid(0, 0));
            Assert.True(grid.IsInsideGrid(2, 2));
            Assert.True(grid.IsInsideGrid(1, 1));
            Assert.False(grid.IsInsideGrid(3, 2));
            Assert.False(grid.IsInsideGrid(2, 3));
        }
    }
}
