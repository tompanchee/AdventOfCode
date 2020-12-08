using System;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt").Where(i => !string.IsNullOrWhiteSpace(i)).ToArray();

            Solve1(input);
            Console.WriteLine();
            Solve2(input);
        }

        private static void Solve2(string[] input) {
            Console.WriteLine("Solving puzzle 1...");
            for (var i = 0; i < input.Length; i++) {
                var @fixed = (string[]) input.Clone();
                if (@fixed[i].StartsWith("acc")) continue;
                if (@fixed[i].StartsWith("nop")) @fixed[i] = @fixed[i].Replace("nop", "jmp");
                else @fixed[i] = @fixed[i].Replace("jmp", "nop");

                var console = new GameConsole(@fixed);
                if (console.Execute(true) == 0) {
                    Console.WriteLine($"Acc after fixed program is executed is {console.Acc}");
                    break;
                }
            }
        }

        private static void Solve1(string[] input) {
            Console.WriteLine("Solving puzzle 1...");
            var console = new GameConsole(input);
            console.Execute(true);
            Console.WriteLine($"Acc value at loop is {console.Acc}");
        }
    }
}