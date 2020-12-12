using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(int.Parse)
                .OrderBy(i => i)
                .ToArray();

            Solve1(input);
            Console.WriteLine();
        }

        private static (int, int) Solve1(int[] input) {
            var count1 = 0;
            var count3 = 0;

            Console.WriteLine("Solving puzzle 1...");
            for (var i = 0; i < input.Length - 1; i++) {
                if (input[i + 1] - input[i] == 1) count1++;
                if (input[i + 1] - input[i] == 3) count3++;
            }

            Console.WriteLine($"Required response is {(count1 + 1) * (count3 + 1)}"); // Add socket and device

            return (count1, count3);
        }

        private static void Solve2(List<int> input) {
            Console.WriteLine("Solving puzzle 2...");
            input.Insert(0, 0);
            input.Add(input.Max() + 3);

            var pathCounts = new long[input.Count];
            var numberOfPaths = CalculatePathCount(input.ToArray(), 0);

            Console.WriteLine($"Number of allowed combinations {numberOfPaths}");

            long CalculatePathCount(int[] path, int index) {
                var count = 0L;
                var current = path[0];
                if (pathCounts[index] > 0) return pathCounts[index];
                if (path.Length <= 1) return 1;

                if (path[1] - current <= 3) count += CalculatePathCount(path.Skip(1).ToArray(), index + 1);
                if (path.Length > 2 && path[2] - current <= 3) count += CalculatePathCount(path.Skip(2).ToArray(), index + 2);
                if (path.Length > 3 && path[3] - current <= 3) count += CalculatePathCount(path.Skip(3).ToArray(), index + 3);

                pathCounts[index] = count;
                return count;
            }
        }
    }
}