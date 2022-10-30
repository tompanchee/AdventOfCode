using System;

namespace Day2
{
    class Keypad
    {
        readonly int[,] keys = {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        private int row;
        private int col;

        public Keypad(int row, int col) {
            this.row = row;
            this.col = col;
        }

        public void Resolve(string instructions) {
            foreach (var instruction in instructions) {
                switch (instruction) {
                    case 'U':
                        if (row > 0) row--;
                        break;
                    case 'D':
                        if (row < 2) row++;
                        break;
                    case 'L':
                        if (col > 0) col--;
                        break;
                    case 'R':
                        if (col < 2) col++;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public int Current => keys[row, col];
    }
}