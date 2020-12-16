using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            var (rules, myTicket, nearbyTickets) = ParseInput(input);

            Solve1(rules, nearbyTickets);
            Console.WriteLine();
            Solve2(rules, nearbyTickets, myTicket);
        }

        private static void Solve1(Rule[] rules, IEnumerable<int[]> nearbyTickets) {
            Console.WriteLine("Solving puzzle 1...");
            var scanningErrorRate = nearbyTickets.Select(ValidateTicket).Select(invalidValues => invalidValues.Sum()).Sum();
            Console.WriteLine($"Scanning error rate is {scanningErrorRate}");

            IEnumerable<int> ValidateTicket(IEnumerable<int> ticket) {
                foreach (var value in ticket) {
                    if (!rules.Any(r => r.IsValid(value))) yield return value;
                }
            }
        }

        private static void Solve2(Rule[] rules, List<int[]> nearbyTickets, int[] myTicket) {
            Console.WriteLine("Solving puzzle 2...");
            var validTickets = nearbyTickets.Where(IsValid).ToList();
            var candidates = GetCandidates();

            var fields = new List<(string, int)>();
            while (candidates.Count > 0) {
                var (key, value) = candidates.FirstOrDefault(p => p.Value.Count == 1);
                fields.Add((key, value[0]));
                foreach (var (_, values) in candidates.Where(candidate => candidate.Key != key)) {
                    values.Remove(value[0]);
                }
                candidates.Remove(key);
            }

            var total = fields
                .Where(field => field.Item1.StartsWith("departure"))
                .Aggregate(1L, (current, field) => current * myTicket[field.Item2]);

            Console.WriteLine($"My ticket's departure product is {total}");

            bool IsValid(int[] ticket) {
                return ticket.Aggregate(true, (current, value) => current && rules.Any(r => r.IsValid(value)));
            }

            IDictionary<string, List<int>> GetCandidates() {
                var result = new Dictionary<string, List<int>>();
                foreach (var rule in rules) {
                    var candidateFields = Enumerable.Range(0, rules.Length).ToList();
                    foreach (var ticket in validTickets) {
                        for (var i = 0; i < ticket.Length; i++) {
                            if (!rule.IsValid(ticket[i])) {
                                candidateFields.Remove(i);
                            }
                        }
                    }

                    result.Add(rule.Name, candidateFields);
                }

                return result;
            }
        }

        private static (Rule[], int[], List<int[]>) ParseInput(string[] input) {
            var rulePattern = new Regex("(.*): (.*) or (.*)");
            var i = 0;

            var rules = new List<Rule>();
            while (!string.IsNullOrEmpty(input[i])) {
                var matches = rulePattern.Matches(input[i]);
                rules.Add(new Rule(matches[0].Groups[1].Value,
                    ValueRange.FromString(matches[0].Groups[2].Value),
                    ValueRange.FromString(matches[0].Groups[3].Value)));
                i++;
            }

            i += 2;
            var myTicket = input[i].Split(',').Select(int.Parse).ToArray();

            i += 3;
            var nearbyTickets = new List<int[]>();
            while (i < input.Length) {
                nearbyTickets.Add(input[i].Split(',').Select(int.Parse).ToArray());
                i++;
            }

            return (rules.ToArray(), myTicket, nearbyTickets);
        }
    }
}