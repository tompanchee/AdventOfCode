using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Solving puzzle 1...");
            var jumps = ParseInput();
            var pc = 0;
            var count = 0;

            while (pc >= 0 && pc < jumps.Length) {
                var jump = jumps[pc];
                jumps[pc]++;
                count++;
                pc += jump;
            }

            Console.WriteLine($"Exits after {count} jumps");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            jumps = ParseInput();
            pc = 0;
            count = 0;

            while (pc >= 0 && pc < jumps.Length) {
                var jump = jumps[pc];
                jumps[pc] += jump >= 3 ? -1 : 1;
                count++;
                pc += jump;
            }

            Console.WriteLine($"Exits after {count} jumps");
        }

        private static int[] ParseInput() {
            return File.ReadAllLines("input.txt")
                .Select(int.Parse)
                .ToArray();
        }
    }
}