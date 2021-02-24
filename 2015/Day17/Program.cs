using System;
using System.IO;
using System.Linq;
using MoreLinq;

namespace Day17
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var containers = input.Select(int.Parse).ToList();

            var subsets = containers.Subsets();
            var subsetsWith150l = subsets.Where(s => s.Sum() == 150).ToList();

            Console.WriteLine("Solving puzzle 1...");
            var count = subsetsWith150l.Count;
            Console.WriteLine($"There are {count} combinations that sum to 150l.");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var minLength = subsetsWith150l.Select(s => s.Count).Min();
            var minLengthCombinations = subsetsWith150l.Where(s => s.Count == minLength);
            Console.WriteLine($"There are {minLengthCombinations.Count()} ways to use minimum amount of containers.");
        }
    }
}