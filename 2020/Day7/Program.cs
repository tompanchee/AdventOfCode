using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var bags = (from line in input where !string.IsNullOrEmpty(line) select Bag.Parse(line)).ToList();
            BuildBagTrees(bags);

            Solve1(bags);
            Console.WriteLine();
            Solve2(bags);
        }

        private static void Solve1(IEnumerable<Bag> bags) {
            Console.WriteLine("Solving puzzle 1...");
            var count = bags.Count(bag => bag.HasChildWithColor("shiny gold"));

            Console.WriteLine($"There are {count} bags that can carry at least one shiny gold bag");
        }

        private static void Solve2(IEnumerable<Bag> bags) {
            Console.WriteLine("Solving puzzle 2...");
            var shinyGold = bags.Single(b => b.Color == "shiny gold");
            Console.WriteLine($"Shiny gold bag contains {shinyGold.TotalInnerBagCount - 1} bags"); // Remove self
        }

        private static void BuildBagTrees(IReadOnlyCollection<Bag> bags) {
            foreach (var bag in bags) {
                foreach (var innerBag in bag.InnerBags) {
                    var inner = bags.Single(b => b.Color == innerBag.Bag.Color);
                    innerBag.Bag = inner;
                }
            }
        }
    }
}