using System;

namespace Day2
{
    class Keypad2
    {
        private readonly char[,] keys = {
            {' ', ' ', '1', ' ', ' '},
            {' ', '2', '3', '4', ' '},
            {'5', '6', '7', '8', '9'},
            {' ', 'A', 'B', 'C', ' '},
            {' ', ' ', 'D', ' ', ' '},
        };

        private int row;
        private int col;

        public Keypad2(int row, int col) {
            this.row = row;
            this.col = col;
        }

        public void Resolve(string instructions) {
            foreach (var instruction in instructions) {
                switch (instruction) {
                    case 'U':
                        if (row > 0 && keys[row - 1, col] != ' ') row--;
                        break;
                    case 'D':
                        if (row < 4 && keys[row + 1, col] != ' ') row++;
                        break;
                    case 'L':
                        if (col > 0 && keys[row, col - 1] != ' ') col--;
                        break;
                    case 'R':
                        if (col < 4 && keys[row, col + 1] != ' ') col++;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        public char Current => keys[row, col];
    }
}