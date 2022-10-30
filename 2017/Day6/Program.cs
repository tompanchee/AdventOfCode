using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    internal class Program
    {
        private static void Main(string[] args) {
            var memory = File.ReadAllText("input.txt")
                .Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Console.WriteLine("Solving puzzle 1...");
            var history = new HashSet<string>();
            var count = 0;

            while (!history.Contains(Stringify(memory))) {
                history.Add(Stringify(memory));
                Redistribute(memory);
                count++;
            }

            Console.WriteLine($"Duplicate allocation found after {count} rounds");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var previous = Array.IndexOf(history.ToArray(), Stringify(memory));
            Console.WriteLine($"Loop length is {count - previous} rounds");
        }

        private static string Stringify(int[] memory) {
            return string.Join('|', memory);
        }

        private static void Redistribute(int[] memory) {
            var max = memory.Max();
            var idx = Array.IndexOf(memory, max);
            memory[idx] = 0;

            while (max > 0) {
                idx++;
                if (idx == memory.Length) idx = 0;
                memory[idx]++;
                max--;
            }
        }
    }
}