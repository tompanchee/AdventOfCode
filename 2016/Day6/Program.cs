using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day6
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            var columns = new List<string>();
            for (var i = 0; i < input[0].Length; i++) {
                columns.Add(new string(input.Select(l => l[i]).ToArray()));
            }

            Console.WriteLine("Solving puzzle 1...");
            var corrected = new StringBuilder();
            foreach (var column in columns) {
                var ordered = column
                    .GroupBy(c => c)
                    .ToDictionary(g => g.Key, g => g.Count())
                    .OrderByDescending(p => p.Value);
                corrected.Append(ordered.First().Key);
            }

            Console.WriteLine($"Corrected message is {corrected}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2..");
            corrected = new StringBuilder();
            foreach (var column in columns) {
                var ordered = column
                    .GroupBy(c => c)
                    .ToDictionary(g => g.Key, g => g.Count())
                    .OrderBy(p => p.Value);
                corrected.Append(ordered.First().Key);
            }

            Console.WriteLine($"Corrected message is {corrected}");
        }
    }
}