using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace Day24
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();

            // Not too fast :)
            Console.WriteLine("Solving puzzle 1...");
            var groupWeight = input.Sum() / 3;
            var allowedGroups = input.Subsets()
                .Where(s => s.Sum() == groupWeight)
                .ToList();
            var minGroupSize = allowedGroups.Select(g => g.Count).Min();
            var minGroups = allowedGroups.Where(g => g.Count == minGroupSize);
            var minQE = minGroups.Select(CalculateQE).Min();
            Console.WriteLine($"Minimum QE is {minQE}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            groupWeight = input.Sum() / 4;
            allowedGroups = input.Subsets()
                .Where(s => s.Sum() == groupWeight)
                .ToList();
            minGroupSize = allowedGroups.Select(g => g.Count).Min();
            minGroups = allowedGroups.Where(g => g.Count == minGroupSize);
            minQE = minGroups.Select(CalculateQE).Min();
            Console.WriteLine($"Minimum QE is {minQE}");
        }

        static long CalculateQE(IEnumerable<int> list) {
            return list.Aggregate(1L, (a, b) => a * b);
        }
    }
}