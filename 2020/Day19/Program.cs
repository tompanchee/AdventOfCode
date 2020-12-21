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

            var (rules, messages) = ParseInput(input);

            Console.WriteLine("Solving puzzle 1...");
            var count = messages.Count(message => IsMessageValid(rules, message));
            Console.WriteLine($"{count} of messages match rule 0");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            rules[8] = Rule.FromInputLine("42 | 42 8"); //8: 42 | 42 8
            rules[11] = Rule.FromInputLine("42 31 | 42 11 31"); //11: 42 31 | 42 11 31
            count = messages.Count(message => IsMessageValid(rules, message));
            Console.WriteLine($"{count} of messages match rule 0");
        }

        static bool IsMessageValid(IDictionary<int, Rule> rules, string message) {
            // GetFilteredEntries filters start of string returning strings that match from the beginning
            // the last characters might still differ from the message tested
            return GetFilteredEntriesFor(rules, 0, message).Contains(message);
        }

        static IEnumerable<string> GetFilteredEntriesFor(IDictionary<int, Rule> rules, int idx, string test) {
            var rule = rules[idx];
            if (rule.IsCharacter) {
                yield return rule.Character;
            } else {
                foreach (var child in rule.ChildRules) {
                    switch (child.Count) {
                        case 1: {
                            foreach (var entry in GetFilteredEntriesFor(rules, child[0], test)) {
                                yield return entry;
                            }

                            break;
                        }
                        case 2: {
                            foreach (var s1 in GetFilteredEntriesFor(rules, child[0], test)) {
                                if (!test.StartsWith(s1)) continue; // Skip if entry does not match
                                foreach (var s2 in GetFilteredEntriesFor(rules, child[1], test[s1.Length..])) {
                                    yield return s1 + s2;
                                }
                            }

                            break;
                        }
                        // Part 2
                        case 3: {
                            foreach (var s1 in GetFilteredEntriesFor(rules, child[0], test)) {
                                if (!test.StartsWith(s1)) continue; // Skip if entry does not match
                                foreach (var s2 in GetFilteredEntriesFor(rules, child[1], test[s1.Length..])) {
                                    if (!test.StartsWith(s1 + s2)) continue; // Skip if entry does not match
                                    foreach (var s3 in GetFilteredEntriesFor(rules, child[2], test[(s1 + s2).Length..])) {
                                        yield return s1 + s2 + s3;
                                    }
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }

        static (IDictionary<int, Rule>, string[]) ParseInput(IEnumerable<string> input) {
            var rules = new Dictionary<int, Rule>();
            var messages = new List<string>();

            var rulesRead = false;
            foreach (var line in input) {
                if (string.IsNullOrEmpty(line)) {
                    rulesRead = true;
                    continue;
                }

                if (!rulesRead) {
                    var split = line.Split(':');
                    var id = int.Parse(split[0]);
                    var rule = Rule.FromInputLine(split[1]);
                    rules.Add(id, rule);
                }

                if (rulesRead) messages.Add(line);
            }

            return (rules, messages.ToArray());
        }
    }
}