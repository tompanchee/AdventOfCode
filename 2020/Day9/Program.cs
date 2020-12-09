using System;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(long.Parse).ToArray();

            var invalid = Solve1(input);
            Console.WriteLine();
            Solve2(input, invalid);
        }

        private static long Solve1(long[] input) {
            Console.WriteLine("Solving puzzle 1...");
            for (var i = 25; i < input.Length; i++) {
                var found = false;
                for (var j = 1; j <= 25; j++) {
                    for (var k = j; k <= 25; k++) {
                        if (input[i] == input[i - j] + input[i - k]) {
                            found = true;
                            break;
                        }
                    }

                    if (found) break;
                }

                if (!found) {
                    Console.WriteLine($"First invalid number is {input[i]}");
                    return input[i];
                }
            }

            throw new Exception("No invalid number found");
        }

        private static void Solve2(long[] input, long invalid) {
            Console.WriteLine("Solving puzzle 2...");
            var first = 0;
            var count = 1;

            while (true) {
                var subset = input.Skip(first).Take(count).ToList();
                if (subset.Sum() == invalid) {
                    Console.WriteLine($"Encryption weakness is {subset.Min() + subset.Max()}");
                    return;
                }

                if (subset.Sum() > invalid) {
                    first++;
                    count = 1;
                    continue;
                }

                count++;
            }
        }
    }
}