namespace Day12.Puzzle2
{
    class WayPoint
    {
        public WayPoint(int north, int east) {
            this.North = north;
            this.East = east;
        }

        public void Move(string instruction) {
            var operation = instruction[0];
            var parameter = int.Parse(instruction[1..]);

            switch (operation)
            {
                case 'N':
                    North += parameter;
                    break;
                case 'E':
                    East += parameter;
                    break;
                case 'S':
                    North -= parameter;
                    break;
                case 'W':
                    East -= parameter;
                    break;
                case 'L':
                    Turn(-parameter);
                    break;
                case 'R':
                    Turn(parameter);
                    break;
            }
        }

        private void Turn(int degrees) {
            if (degrees < 0) degrees = 360 + degrees;
            while (degrees > 0) {
                var e = East;
                East = North;
                North = -e;
                degrees -= 90;
            }
        }

        public int North { get; private set; }
        public int East { get; private set; }
    }
}