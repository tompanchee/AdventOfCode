using System;
using System.Collections.Generic;

namespace Day15
{
    class Program
    {
        static void Main(string[] args) {
            var input = new[] {1, 0, 15, 2, 10, 13};

            Console.WriteLine("Solving puzzle 1...");
            var result = FindNumber(input, 2020);
            Console.WriteLine($"Number at 2020 is {result}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            result = FindNumber(input, 30000000);
            Console.WriteLine($"Number at 2020 is {result}");
        }

        private static int FindNumber(IReadOnlyList<int> input, int index) {
            var game = new Dictionary<int, int>();

            for (var i = 0; i < input.Count - 1; i++) {
                game.Add(input[i], i);
            }

            var round = input.Count - 1;
            var current = input[^1];
            while (round < index - 1) {
                var next = 0;
                if (game.ContainsKey(current)) {
                    next = round - game[current];
                }

                game[current] = round;
                current = next;
                round++;
            }

            return current;
        }
    }
}