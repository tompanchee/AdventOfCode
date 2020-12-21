using System;
using System.IO;
using System.Linq;

namespace Day18
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var total = input.Sum(line => new Expression(line).Evaluate());
            Console.WriteLine($"Total sum of all expressions is {total}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var total2 = input.Sum(line => new Expression(line, new[] {'+', '*'}).Evaluate());
            Console.WriteLine($"Total sum of all expressions is {total2}");
        }
    }
}