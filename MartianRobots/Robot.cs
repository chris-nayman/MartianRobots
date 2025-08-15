namespace MartianRobots
{
    public class Robot
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Orientation { get; set; }
        public bool IsLost { get; set; }

        public Robot(int positionX, int positionY, char orientation, int MaxCoordinate)
        {
            if (positionX > MaxCoordinate)
                throw new ArgumentOutOfRangeException(nameof(positionX), $"Robot X starting position is outside of grid bounds: {MaxCoordinate}.");
            if (positionY > MaxCoordinate)
                throw new ArgumentOutOfRangeException(nameof(positionY), $"Robot Y starting position is outside of grid bounds: {MaxCoordinate}.");

            PositionX = positionX;
            PositionY = positionY;
            Orientation = orientation;
            IsLost = false;
        }        

        public void TurnLeft()
        {
            Orientation = Orientation switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => Orientation
            };
        }

        public void TurnRight()
        {
            Orientation = Orientation switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => Orientation
            };
        }

        public void MoveForward(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        public (int nx, int ny) NextForwardMovePosition()
        {
            int x = PositionX, y = PositionY;
            switch (Orientation)
            {
                case 'N': y++; break;
                case 'S': y--; break;
                case 'E': x++; break;
                case 'W': x--; break;
            }
            return (x, y);
        }

        public override string ToString()
        {
            return IsLost 
                ? $"{PositionX} {PositionY} {Orientation} LOST"
                : $"{PositionX} {PositionY} {Orientation}";
        }
    }
}
