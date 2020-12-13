namespace Day12.Puzzle2
{
    class Ship
    {
        public void Move(int north, int east) {
            North += north;
            East += east;
        }

        public int North { get; private set; }
        public int East { get; private set; }
    }
}