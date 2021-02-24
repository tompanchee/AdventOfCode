using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var sues = input.Select(Sue.FromInputLine).ToList();
            var clues = new Dictionary<string, int> {
                {"children", 3},
                {"cats", 7},
                {"samoyeds", 2},
                {"pomeranians", 3},
                {"akitas", 0},
                {"vizslas", 0},
                {"goldfish", 5},
                {"trees", 3},
                {"cars", 2},
                {"perfumes", 1}
            };

            Console.WriteLine("Solving puzzle 1...");
            foreach (var (key, value) in clues) {
                var toRemove = sues.Where(s => s.Properties.ContainsKey(key) && s.Properties[key] != value).ToList();
                foreach (var sue in toRemove) {
                    sues.Remove(sue);
                }
            }

            Console.WriteLine($"Sue sending MFCSAM is {sues.Single().Id}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            sues = input.Select(Sue.FromInputLine).ToList(); // reset input
            foreach (var (key, value) in clues) {
                var toRemove = sues.Where(s => s.Properties.ContainsKey(key) && !IsMatch(key, s.Properties[key], value)).ToList();
                foreach (var sue in toRemove) {
                    sues.Remove(sue);
                }
            }

            Console.WriteLine($"The real Sue sending MFCSAM is {sues.Single().Id}");
        }

        private static bool IsMatch(string property, int propValue, int clueValue) {
            return property switch {
                "cats" or "trees" => clueValue < propValue,
                "pomeranians" or "goldfish" => clueValue > propValue,
                _ => clueValue == propValue,
            };
        }
    }
}