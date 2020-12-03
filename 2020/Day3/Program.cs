using System;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args) {
            var forest = Forest.Init("input.txt");

            Solve1(forest);
            Console.WriteLine();
            Solve2(forest);
        }

        private static void Solve1(Forest forest) {
            Console.WriteLine("Solving puzzle 1...");
            CountEncountersOnPath(forest, (3, 1));
        }

        private static void Solve2(Forest forest) {
            Console.WriteLine("Solving puzzle 2...");

            var slopes = new[] {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            var counts = slopes.Select(slope => CountEncountersOnPath(forest, slope)).ToList();

            Console.WriteLine($"{string.Join('*', counts)} = {counts.Aggregate(1, (a, b) => a * b)}");
        }

        private static int CountEncountersOnPath(Forest forest, (int, int) slope) {
            var top = 1;
            var left = 1;
            var count = 0;

            var (deltaLeft, deltaTop) = slope;
            Console.Write($"Traversing slope ({deltaLeft}, {deltaTop})");
            while (forest[top, left] != null) {
                left += deltaLeft;
                top += deltaTop;
                if (forest[top, left] == '#') count++;
            }

            Console.WriteLine($" - Encounters {count}");

            return count;
        }
    }
}