namespace Day12.Puzzle1
{
    class Ferry
    {
        private int east = 0;
        private int north = 0;
        private int direction = 90;

        public void Navigate(string instruction) {
            var operation = instruction[0];
            var parameter = int.Parse(instruction[1..]);

            switch (operation) {
                case 'N':
                    north += parameter;
                    break;
                case 'E':
                    east += parameter;
                    break;
                case 'S':
                    north -= parameter;
                    break;
                case 'W':
                    east -= parameter;
                    break;
                case 'F':
                    Forward(parameter);
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
            direction += degrees;
            if (direction < 0) direction += 360;
            if (direction >= 360) direction -= 360;
        }

        private void Forward(in int parameter) {
            if (direction == 0) north += parameter;
            if (direction == 90) east += parameter;
            if (direction == 180) north -= parameter;
            if (direction == 270) east -= parameter;
        }

        public (int, int) Position => (north, east);
    }
}