using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var modifiers = ParseModifiers(input);

            Console.WriteLine("Solving puzzle 1...");
            var persons = modifiers.Keys.Select(k => k.person1).Distinct().ToList();
            var sittingOrders = persons.Permutations();
            var maxChange = CalculateMaxHappinessChange(sittingOrders, modifiers);
            Console.WriteLine($"Max happiness change is {maxChange}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            foreach (var person in persons) {
                modifiers.Add(("Me", person), 0);
                modifiers.Add((person, "Me"), 0);
            }

            persons.Add("Me");
            sittingOrders = persons.Permutations();
            maxChange = CalculateMaxHappinessChange(sittingOrders, modifiers);
            Console.WriteLine($"Max happiness change is {maxChange}");
        }

        private static int CalculateMaxHappinessChange(IEnumerable<IList<string>> sittingOrders, IDictionary<(string person1, string person2), int> modifiers) {
            var maxChange = int.MinValue;
            foreach (var order in sittingOrders) {
                var change = 0;
                for (var i = 0; i < order.Count; i++) {
                    (string p1, string p2) pair = i == order.Count - 1 ? (order[i], order[0]) : (order[i], order[i + 1]);
                    change += modifiers[pair];
                    change += modifiers[(pair.p2, pair.p1)];
                }

                if (change > maxChange) maxChange = change;
            }

            return maxChange;
        }

        static IDictionary<(string person1, string person2), int> ParseModifiers(string[] input) {
            var result = new Dictionary<(string, string), int>();

            foreach (var line in input) {
                var split = line.Split(' ');
                var person1 = split[0];
                var person2 = split[10][..^1];
                var modifier = int.Parse(split[3]);
                if (split[2] == "lose") modifier = -modifier;

                result.Add((person1, person2), modifier);
            }

            return result;
        }
    }
}