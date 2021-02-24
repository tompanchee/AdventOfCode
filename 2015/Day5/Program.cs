using System;
using System.IO;
using System.Linq;
using Day5.Puzzle1;

namespace Day5
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            IValidator validator = new Validator();
            var countOfNice = input.Count(s => validator.IsNice(s));
            Console.WriteLine($"There are {countOfNice} nice strings.");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            validator = new Puzzle2.Validator();
            countOfNice = input.Count(s => validator.IsNice(s));
            Console.WriteLine($"There are {countOfNice} nice strings.");
        }
    }

    interface IValidator
    {
        bool IsNice(string input);
    }
}