using System.Linq;

namespace Day5.Puzzle1
{
    class Validator : IValidator
    {
        private const string VOWELS = "aeiou";
        static readonly string[] illegalStrings = {"ab", "cd", "pq", "xy"};

        public bool IsNice(string input) {
            return HasThreeVowels(input)
                   && ContainsDoubleLetter(input)
                   && HasNoIllegalString(input);
        }

        private static bool HasThreeVowels(string input) {
            var count = 0;
            foreach (var vowel in VOWELS) {
                count += input.Count(c => c == vowel);
                if (count > 2) return true;
            }

            return false;
        }

        private static bool ContainsDoubleLetter(string input) {
            for (var i = 0; i < input.Length - 1; i++) {
                if (input[i] == input[i + 1]) return true;
            }

            return false;
        }

        private static bool HasNoIllegalString(string input) {
            return illegalStrings.All(illegalString => !input.Contains(illegalString));
        }
    }
}