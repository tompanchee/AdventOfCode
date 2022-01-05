using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Executing program...");
            var registers = new Dictionary<string, Register>();
            var program = File.ReadAllLines("input.txt");
            var allTimeMax = int.MinValue;
            foreach (var line in program) {
                ExecuteLine(line);
            }

            Console.WriteLine("Completed");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Largest register value is {registers.Values.Select(r => r.Value).Max()}");

            Console.WriteLine("Solving puzzle 2...");
            Console.WriteLine($"All time max value is {allTimeMax}");

            Register GetOrCreateRegister(string name) {
                if (!registers.ContainsKey(name)) registers.Add(name, new Register());
                return registers[name];
            }

            void ExecuteLine(string line) {
                var split = line.Split(' ');
                var reg = split[0];
                var op = split[1];
                var modifier = int.Parse(split[2]);
                var condition = line[(line.IndexOf("if", StringComparison.InvariantCulture) + 3)..];

                var current = GetOrCreateRegister(reg);
                var conditionalRegister = GetOrCreateRegister(condition[..condition.IndexOf(' ')]);
                if (conditionalRegister.CheckCondition(condition[(condition.IndexOf(' ') + 1)..])) {
                    switch (op) {
                        case "inc":
                            current.Value += modifier;
                            break;
                        case "dec":
                            current.Value -= modifier;
                            break;
                    }
                }

                if (current.Value > allTimeMax) allTimeMax = current.Value;
            }
        }
    }

    class Register
    {
        private static readonly IDictionary<string, Func<int, int, bool>> evaluators = new Dictionary<string, Func<int, int, bool>> {
            { "==", (a, b) => a == b },
            { "!=", (a, b) => a != b },
            { "<", (a, b) => a < b },
            { ">", (a, b) => a > b },
            { "<=", (a, b) => a <= b },
            { ">=", (a, b) => a >= b }
        };

        public int Value { get; set; }

        // Condition is the actual condition for this register, eg. < 1
        public bool CheckCondition(string condition) {
            var @operator = condition[..condition.IndexOf(' ')];
            var oValue = int.Parse(condition[(condition.IndexOf(' ') + 1)..]);
            return evaluators[@operator](Value, oValue);
        }
    }
}