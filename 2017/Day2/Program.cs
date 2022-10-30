using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args) {
            var input = ParseInput();

            Console.WriteLine("Solving puzzle 1...");
            var sum = input.Sum(row => row.Max() - row.Min());
            Console.WriteLine($"Checksum is {sum}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");

            sum = 0;
            foreach (var row in input) {
                var found = false;
                for (var i = 0; i < row.Length; i++) {
                    if (found) break;
                    for (var j = 0; j < row.Length; j++) {
                        if (i == j) continue;
                        if (row[i] % row[j] == 0) {
                            sum += row[i] / row[j];
                            found = true;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"Checksum is {sum}");
        }

        private static IList<int[]> ParseInput() {
            var data = File.ReadAllLines("input.txt");
            return data
                .Select(s => s.Split('\t', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToList();
        }
    }
}