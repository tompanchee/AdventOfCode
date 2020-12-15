using System;
using System.Linq;

namespace Day14.Puzzle2
{
    static class Solver
    {
        public static void Solve(string[] input) {
            Console.WriteLine("Solving puzzle 2...");

            var decoder = new Decoder(input);
            decoder.Execute();
            Console.WriteLine($"Sum of memory values {decoder.Memory.Values.Sum()}");
        }
    }
}