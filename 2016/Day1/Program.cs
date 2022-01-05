using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static readonly Direction[] headings = new[] {
            Direction.North,
            Direction.East,
            Direction.South,
            Direction.West
        };

        private static readonly IDictionary<Direction, (int n, int e)> moveOffsets = new Dictionary<Direction, (int n, int e)> {
            {Direction.North, (1, 0)},
            {Direction.East, (0, 1)},
            {Direction.South, (-1, 0)},
            {Direction.West, (0, -1)}
        };

        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt").First();
            var instructions = input.Split(',', StringSplitOptions.TrimEntries);

            Console.WriteLine("Solving puzzle 1...");
            (int n, int e) location = (0, 0);
            var heading = Direction.North;

            foreach (var instruction in instructions) {
                var t = instruction[0];
                var a = int.Parse(instruction[1..]);
                heading = Turn(t, heading);
                location = Move(heading, a, location);
            }

            Console.WriteLine($"The bunny headquarters is {Math.Abs(location.n) + Math.Abs(location.e)} blocks away");

            Console.WriteLine("Solving puzzle 2...");
            var locations = new HashSet<(int, int)> {(0, 0)};
            location = (0, 0);
            heading = Direction.North;
            var found = false;

            foreach (var instruction in instructions) {
                var t = instruction[0];
                var a = int.Parse(instruction[1..]);
                heading = Turn(t, heading);
                for (var i = 0; i < a; i++) {
                    var (dn, de) = moveOffsets[heading];
                    location.n += dn;
                    location.e += de;
                    if (locations.Contains(location)) {
                        found = true;
                        break;
                    }

                    locations.Add(location);
                }

                if (found) break;
            }

            Console.WriteLine($"The bunny headquarters is {Math.Abs(location.n) + Math.Abs(location.e)} blocks away");
        }

        static (int n, int e) Move(Direction heading, int amount, (int n, int e) location) {
            var (dn, de) = moveOffsets[heading];
            return (location.n + amount * dn, location.e + amount * de);
        }

        static Direction Turn(char turn, Direction current) {
            var idx = Array.IndexOf(headings, current);
            idx += turn == 'R' ? 1 : -1;
            if (idx < 0) idx = headings.Length + idx;
            idx %= headings.Length;
            return headings[idx];
        }
    }

    enum Direction
    {
        North,
        East,
        South,
        West
    }
}