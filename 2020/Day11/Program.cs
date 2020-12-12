using System;
using System.IO;

namespace Day11
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Solve1(input);
            Console.WriteLine();
            Solve2(input);
        }

        private static void Solve1(string[] input) {
            Console.WriteLine("Solving puzzle 1...");
            var area = new WaitingArea(input);
            var next = area.GetNextGeneration();
            while (!next.Equals(area)) {
                area = next;
                next = area.GetNextGeneration();
            }

            Console.WriteLine($"Occupied seats after stabilization {next.CountWithStatus('#')}");
        }

        private static void Solve2(string[] input) {
            Console.WriteLine("Solving puzzle 2...");
            var area = new WaitingArea(input);
            var next = area.GetNextGeneration(true);
            while (!next.Equals(area)) {
                area = next;
                next = area.GetNextGeneration(true);
            }

            Console.WriteLine($"Occupied seats after stabilization {next.CountWithStatus('#')}");
        }
    }
}