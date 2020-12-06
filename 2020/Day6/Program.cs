using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var groups = new List<List<string>>();
            var group = new List<string>();
            foreach(var line in input) {
                if (string.IsNullOrWhiteSpace(line)) {
                    groups.Add(group);
                    group = new List<string>();
                    continue;
                }
                group.Add(line);
            }
            groups.Add(group);

            Console.WriteLine("Solving puzzle 1...");
            var sum = groups.Sum(g => g.Aggregate("", (a, b) => $"{a}{b}").Distinct().Count());
            Console.WriteLine($"Requested sum is {sum}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var sum2 = 0;
            foreach(var g in groups) {
                var groupsSum = 0;
                foreach(var c in g.First()) {
                    if (g.All(current => current.Contains(c))) groupsSum++;
                }
                sum2 += groupsSum;
            }
            Console.WriteLine($"Requested sum is {sum2}");
        }
    }
}
