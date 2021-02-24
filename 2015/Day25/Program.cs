using System;

namespace Day25
{
    class Program
    {
        static void Main(string[] args) {
            // input
            const int TARGET_ROW = 2947;
            const int TARGET_COL = 3029;

            Console.WriteLine("Solving puzzle 1...");
            var current = 20151125L;
            var row = 1;
            var col = 1;
            while (true) {
                current = GetNext(current);
                if (row == 1) {
                    row += col;
                    col = 1;
                } else {
                    row--;
                    col++;
                }

                if (row == TARGET_ROW && col == TARGET_COL) break;
            }

            Console.WriteLine($"Passcode is {current}");
        }

        static long GetNext(long current) {
            return current * 252533 % 33554393;
        }
    }
}