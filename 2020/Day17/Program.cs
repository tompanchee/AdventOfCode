using System;
using System.IO;

namespace Day17
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Puzzle1.Solver.Solve(input);
            Console.WriteLine();
            Puzzle2.Solver.Solve(input);
        }
    }
}