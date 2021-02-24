using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var ingredients = ParseInput(input);

            Console.WriteLine("Solving puzzle 1...");
            var combinations = GetAllowedCombinations(4, 100);
            var maxScore = combinations.Select(c => CalculateScore(c, ingredients)).Max();
            Console.WriteLine($"Maximum score is {maxScore}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var combinationsWith500Calories = combinations.Where(c => CalculateCalories(c, ingredients) == 500).ToList();
            maxScore = combinationsWith500Calories.Select(c => CalculateScore(c, ingredients)).Max();
            Console.WriteLine($"Maximum score for a cookie with 500 calories is {maxScore}");
        }

        private static Ingredient[] ParseInput(string[] input) {
            return input.Select(line => line.Split(' '))
                .Select(split => new Ingredient(int.Parse(split[2][..^1]),
                    int.Parse(split[4][..^1]),
                    int.Parse(split[6][..^1]),
                    int.Parse(split[8][..^1]),
                    int.Parse(split[10])))
                .ToArray();
        }

        static int CalculateScore(IList<int> combination, Ingredient[] ingredients) {
            var capacity = 0;
            var durability = 0;
            var flavor = 0;
            var texture = 0;
            for (var i = 0; i < ingredients.Length; i++) {
                capacity += ingredients[i].Capacity * combination[i];
                durability += ingredients[i].Durability * combination[i];
                flavor += ingredients[i].Flavor * combination[i];
                texture += ingredients[i].Texture * combination[i];
            }

            if (capacity < 0) capacity = 0;
            if (durability < 0) durability = 0;
            if (flavor < 0) flavor = 0;
            if (texture < 0) texture = 0;

            return capacity * durability * flavor * texture;
        }

        static int CalculateCalories(IList<int> combination, Ingredient[] ingredients) {
            return ingredients.Select((t, i) => t.Calories * combination[i]).Sum();
        }

        static IList<IList<int>> GetAllowedCombinations(int dimensions, int total) {
            var result = new List<IList<int>>();

            for (var i = 1; i <= dimensions; i++) {
                result = AddDimension(result, total);
            }

            return result.Where(r => r.Sum() == total).ToList();
        }

        private static List<IList<int>> AddDimension(List<IList<int>> input, int total) {
            var result = new List<IList<int>>();

            if (!input.Any()) {
                result.AddRange(Enumerable.Range(1, total).Select(i => new List<int> {i}));
                return result;
            }

            foreach (var inner in input) {
                foreach (var n in Enumerable.Range(1, total)) {
                    if (inner.Sum() + n <= total) result.Add(new List<int>(inner) {n});
                }
            }

            return result;
        }
    }
}