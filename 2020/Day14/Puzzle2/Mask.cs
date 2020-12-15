using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14.Puzzle2
{
    class Mask
    {
        private readonly string mask;

        public Mask(string mask) {
            this.mask = mask;
        }

        public long[] GetMaskedValues(long input) {
            // Using strings instead of a bitwise operation
            var masked = new List<char>(Convert.ToString(input, 2).PadLeft(36, '0'));

            // Set bits
            var maskSets = mask.Select((c, i) => c.Equals('1') ? i : -1).Where(i => i > -1);
            foreach (var idx in maskSets) {
                masked[idx] = '1';
            }

            // Fluctuate
            var fluctuating = mask.Select((c, i) => c.Equals('X') ? i : -1).Where(i => i > -1).ToArray();
            var combinations = GetCombinations(fluctuating.Length);

            var result = new List<long>();
            foreach (var combination in combinations) {
                for (var j = 0; j < combination.Length; j++) {
                    var newValue = combination[j];
                    var index = fluctuating[j];
                    masked[index] = newValue;
                }

                result.Add(Convert.ToInt64(new string(masked.ToArray()), 2));
            }

            return result.ToArray();
        }

        private static IEnumerable<string> GetCombinations(int count) {
            var amount = Math.Pow(2, count);
            var numbers = Enumerable.Range(0, (int) amount);

            return numbers.Select(n => Convert.ToString(n, 2).PadLeft(count, '0'));
        }
    }
}