using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt").First();

            Solve1(input);
            Console.WriteLine();
            Solve2(input);
        }

        private static void Solve1(string input) {
            Console.WriteLine("Solving puzzle 1...");
            var houses = new HashSet<(int, int)>();

            // Walk Santa
            (int x, int y) location = (0, 0);
            houses.Add(location);

            foreach (var direction in input) {
                location = Move(direction, location);
                if (!houses.Contains(location)) houses.Add(location);
            }

            Console.WriteLine($"Santa visits {houses.Count} houses.");
        }

        private static void Solve2(string input) {
            Console.WriteLine("Solving puzzle 2...");
            var houses = new HashSet<(int, int)>();

            // Walk Santa and Robo-Santa
            var santa = (0, 0);
            var robo = (0, 0);
            houses.Add(santa);

            var isSanta = true;
            foreach (var direction in input) {
                var location = isSanta ? santa : robo;
                location = Move(direction, location);
                if (!houses.Contains(location)) houses.Add(location);
                if (isSanta) santa = location;
                else robo = location;
                isSanta = !isSanta;
            }

            Console.WriteLine($"Santa and Robo-Santa visit {houses.Count} houses.");
        }

        private static (int x, int y) Move(char direction, (int x, int y) location) {
            switch (direction) {
                case '>':
                    location.x += 1;
                    break;
                case 'v':
                    location.y += 1;
                    break;
                case '<':
                    location.x -= 1;
                    break;
                case '^':
                    location.y -= 1;
                    break;
                default:
                    throw new Exception("Invalid direction");
            }

            return location;
        }
    }
}