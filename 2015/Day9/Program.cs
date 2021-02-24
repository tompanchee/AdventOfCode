using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq.Extensions;

namespace Day9
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var routes = ParseInput(input);

            Console.WriteLine("Solving puzzles 1 and 2...");
            var locations = new List<string>(routes.Keys.Select(k => k.from));
            locations.AddRange(routes.Keys.Select(k => k.to));
            var allRoutes = locations.Distinct().Permutations(); // Use MoreLinq to get permutations
            // Brute force
            var min = int.MaxValue;
            var max = int.MinValue;
            foreach (var route in allRoutes) {
                var length = 0;
                for (var i = 0; i < route.Count - 1; i++) {
                    var key = (route[i], route[i + 1]);
                    if (!routes.ContainsKey(key)) key = (route[i + 1], route[i]);
                    length += routes[key];
                }

                if (length < min) min = length;
                if (length > max) max = length;
            }

            // Puzzle 1
            Console.WriteLine($"Shortest route is {min}");
            Console.WriteLine();
            // Puzzle 2
            Console.WriteLine($"Longest route is {max}");
        }

        private static IDictionary<(string from, string to), int> ParseInput(string[] input) {
            var result = new Dictionary<(string from, string to), int>();
            foreach (var line in input) {
                var split1 = line.Split('=', StringSplitOptions.TrimEntries);
                var split2 = split1[0].Split("to", StringSplitOptions.TrimEntries);
                result.Add((split2[0], split2[1]), int.Parse(split1[1]));
            }

            return result;
        }
    }
}