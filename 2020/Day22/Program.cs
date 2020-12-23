using System;
using System.Collections.Generic;
using System.IO;
namespace Day22
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var (deck1, deck2) = ParseInput(input);

            Puzzle1.Solver.Solve(deck1, deck2);
            Console.WriteLine();
            Puzzle2.Solver.Solve(deck1, deck2);
        }

        private static (int[], int[]) ParseInput(IEnumerable<string> input) {
            int[] deck1 = null;
            var deck = new List<int>();

            foreach (var line in input) {
                if (string.IsNullOrWhiteSpace(line)) {
                    deck1 = deck.ToArray();
                    deck = new List<int>();
                    continue;
                }

                if (int.TryParse(line, out var n)) deck.Add(n);
            }

            var deck2 = deck.ToArray();
            return (deck1, deck2);
        }
    }
}