using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var ints = input.Select(int.Parse).ToArray();

            Solve1(ints);
            Solve2(ints);
        }

        private static void Solve1(int[] input) {
            Console.WriteLine();
            Console.WriteLine("Solving puzzle 1...");

            for (var i = 0; i < input.Length; i++) {
                for (var j = i; j < input.Length; j++) {
                    if (input[i] + input[j] == 2020) {
                        Console.WriteLine($"{input[i]} + {input[j]} = 2020");
                        Console.WriteLine($"{input[i]} * {input[j]} = {input[i] * input[j]}");
                        return;
                    }
                }
            }
        }

        private static void Solve2(int[] input) {
            Console.WriteLine();
            Console.WriteLine("Solving puzzle 2...");

            for (var i = 0; i < input.Length; i++) {
                for (var j = i; j < input.Length; j++) {
                    for (var k = j; k < input.Length; k++) {
                        if (input[i] + input[j] + input[k]== 2020) {
                            Console.WriteLine($"{input[i]} + {input[j]} + {input[k]} = 2020");
                            Console.WriteLine($"{input[i]} * {input[j]} * {input[k]} = {input[i] * input[j] * input[k]}");
                            return;
                        }
                    }
                }
            }
        }
    }
}