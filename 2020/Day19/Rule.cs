using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    class Rule
    {
        #region Factory methods

        public static Rule FromInputLine(string input) {
            if (input.Contains("\"")) {
                return new Rule(null, input.Replace("\"", "").Trim());
            }

            if (input.Contains("|")) {
                var split = input.Split('|');
                return new Rule(new List<List<int>> {GetChildRules(split[0]), GetChildRules(split[1])}, null);
            }

            return new Rule(new List<List<int>> {GetChildRules(input)}, null);
        }

        private static List<int> GetChildRules(string childRules) {
            var split = childRules.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            return split.Select(int.Parse).ToList();
        }

        #endregion

        Rule(List<List<int>> childRules, string character) {
            ChildRules = childRules;
            Character = character;
        }

        public List<List<int>> ChildRules { get; }

        public string Character { get; }

        public bool IsCharacter => Character != null;
    }
}