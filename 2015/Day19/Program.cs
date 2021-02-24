using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day19
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var (replacements, molecule) = ParseInput(input);

            Console.WriteLine("Solving puzzle 1...");
            var molecules = new List<string>();
            foreach (var (old, @new) in replacements) {
                molecules.AddRange(molecule.AllIndexesOf(old).Select(idx => molecule.ReplaceAtIndex(@new, idx, old.Length)));
            }

            Console.WriteLine($"After one substitution there are {molecules.Distinct().Count()} distinct molecules");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var count = 0;
            while (molecule != "e") {
                foreach (var (old, @new) in replacements) {
                    var idx = molecule.IndexOf(@new, StringComparison.InvariantCulture);
                    if (idx < 0) continue;
                    molecule = molecule.ReplaceAtIndex(old, idx, @new.Length);
                    count++;
                }
            }

            Console.WriteLine($"Medicine can be reached with {count} substitutions");
        }

        private static (IList<(string, string)>, string) ParseInput(string[] input) {
            var replacements = new List<(string, string)>();
            var i = 0;
            while (!string.IsNullOrEmpty(input[i])) {
                var split = input[i].Split("=>", StringSplitOptions.TrimEntries);
                replacements.Add((split[0], split[1]));
                i++;
            }

            i++;

            return (replacements, input[i]);
        }
    }
}