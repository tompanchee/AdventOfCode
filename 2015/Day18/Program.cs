using System;
using System.IO;
using System.Linq;

namespace Day18
{
    class Program
    {
        const int MAX_SIZE = 100;

        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var current = ParseInput(input);

            Console.WriteLine("Solving puzzle 1...");
            for (var i = 0; i < 100; i++) {
                current = GetNextGeneration(current);
            }

            var count = current.Cast<bool>().Count(b => b);
            Console.WriteLine($"After 100 steps {count} lights are on.");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            current = ParseInput(input); // reset initial state
            for (var i = 0; i < 100; i++) {
                current = GetNextGeneration(current);
                // Lit corners
                current[0, 0] = true;
                current[0, MAX_SIZE - 1] = true;
                current[MAX_SIZE - 1, 0] = true;
                current[MAX_SIZE - 1, MAX_SIZE - 1] = true;
            }

            count = current.Cast<bool>().Count(b => b);
            Console.WriteLine($"After 100 steps {count} lights are on.");
        }

        private static bool[,] GetNextGeneration(bool[,] current) {
            var next = new bool[MAX_SIZE, MAX_SIZE];
            for (var row = 0; row < MAX_SIZE; row++) {
                for (var col = 0; col < MAX_SIZE; col++) {
                    var neighbours = CountNeighbours(row, col);

                    if (current[row, col]) {
                        next[row, col] = neighbours == 2 || neighbours == 3;
                    } else {
                        next[row, col] = neighbours == 3;
                    }
                }
            }

            return next;

            int CountNeighbours(int row, int col) {
                var count = 0;

                for (var dr = -1; dr <= 1; dr++) {
                    for (var dc = -1; dc <= 1; dc++) {
                        if (dr == 0 && dc == 0) continue;
                        var r = row + dr;
                        var c = col + dc;
                        if (r < 0 || r >= MAX_SIZE) continue;
                        if (c < 0 || c >= MAX_SIZE) continue;
                        if (current[r, c]) count++;
                    }
                }

                return count;
            }
        }

        private static bool[,] ParseInput(string[] input) {
            var result = new bool[MAX_SIZE, MAX_SIZE];

            for (var row = 0; row < MAX_SIZE; row++) {
                for (var col = 0; col < MAX_SIZE; col++) {
                    result[row, col] = input[row][col] == '#';
                }
            }

            return result;
        }
    }
}