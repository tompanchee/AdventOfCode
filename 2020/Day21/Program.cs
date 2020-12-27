using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var recipes = input.Select(Recipe.FromInput).ToList();

            var allergens = GetAllergens(recipes);

            Console.WriteLine("Solving puzzle 1...");
            var harmfulIngredients = allergens.Values.ToArray();
            var count = recipes.Select(r => r.Ingredients).Sum(ingredients => ingredients.Count(i => !harmfulIngredients.Contains(i)));
            Console.WriteLine($"Safe ingredients appear {count} times");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var orderedIngredients = allergens.OrderBy(a => a.Key).Select(p => p.Value);
            Console.WriteLine($"Allergenic ingredients in order {string.Join(',', orderedIngredients)}");
        }

        private static IDictionary<string, string> GetAllergens(List<Recipe> recipes) {
            var allergens = recipes.SelectMany(r => r.Allergens).Distinct().ToDictionary(r => r, r => new List<string>());
            foreach (var (allergen, ingredients) in allergens) {
                foreach (var recipe in recipes.Where(r => r.Allergens.Contains(allergen))) {
                    if (ingredients.Count == 0) ingredients.AddRange(recipe.Ingredients);
                    else allergens[allergen] = allergens[allergen].Intersect(recipe.Ingredients).ToList();
                }
            }

            // Eliminate
            var handledAllergens = new List<string>();
            while (allergens.Values.Any(i => i.Count > 1)) {
                var min = allergens.Select(a => a.Value).Select(i => i.Count).Min();
                var current = allergens.FirstOrDefault(a => a.Value.Count == min && !handledAllergens.Contains(a.Key));
                var ingredient = current.Value[0];
                foreach (var (allergen, ingredients) in allergens.Where(a => a.Key != current.Key)) {
                    allergens[allergen].Remove(ingredient);
                }

                handledAllergens.Add(current.Key);
            }

            return allergens.ToDictionary(a => a.Key, a => a.Value[0]);
        }
    }

    class Recipe
    {
        public static Recipe FromInput(string input) {
            var i = input.Substring(0, input.IndexOf('(') - 1)
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();

            var a = input[input.IndexOf('(')..]["(contains".Length..^1]
                .Trim()
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();

            return new Recipe(i, a);
        }

        Recipe(List<string> ingredients, List<string> allergens) {
            this.Ingredients = ingredients;
            this.Allergens = allergens;
        }

        public List<string> Ingredients { get; }

        public List<string> Allergens { get; }
    }
}