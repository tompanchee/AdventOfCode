using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            Solve(new Puzzle1Validator(), input);

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            Solve(new Puzzle2Validator(), input);
        }

        static void Solve(IValidator validator, IEnumerable<string> input) {
            Console.WriteLine($"Count of valid passwords: {input.Count(validator.IsValid)}");
        }
    }
}