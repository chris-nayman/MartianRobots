namespace MartianRobots
{
    public class Robot
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Orientation { get; set; }

        public Robot(int positionX, int positionY, char orientation)
        {
            PositionX = positionX;
            PositionY = positionY;
            Orientation = orientation;
        }        

        public void ExecuteCommand(char instruction)
        {
            switch (instruction)
            {
                case 'L':
                    Orientation = TurnLeft(Orientation);
                    break;

                case 'R':
                    Orientation = TurnRight(Orientation);
                    break;

                case 'F':
                    MoveForward();
                    break;

                default:
                    throw new ArgumentException($"Invalid instruction: {instruction}");
            }
        }

        private char TurnLeft(char current)
        {
            return current switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => current
            };
        }

        private char TurnRight(char current)
        {
            return current switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => current
            };
        }

        private void MoveForward()
        {
            switch (Orientation)
            {
                case 'N': PositionY++; break;
                case 'S': PositionY--; break;
                case 'E': PositionX++; break;
                case 'W': PositionX--; break;
            }
        }

        public override string ToString()
        {
            return $"{PositionX} {PositionY} {Orientation}";             
        }
    }
}
