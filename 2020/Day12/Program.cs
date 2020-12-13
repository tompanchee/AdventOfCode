using System;
using System.IO;
using Day12.Puzzle1;
using Day12.Puzzle2;

namespace Day12
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
            var ferry = new Ferry();
            foreach (var i in input) {
                ferry.Navigate(i);
            }

            Console.WriteLine($"Distance from original position is {Math.Abs(ferry.Position.Item1) + Math.Abs(ferry.Position.Item2)}");
        }

        private static void Solve2(string[] input) {
            Console.WriteLine("Solving puzzle 2...");
            var wayPoint = new WayPoint(1, 10);
            var ship = new Ship();

            foreach (var i in input) {
                if (i[0] == 'F') {
                    var amount = int.Parse(i[1..]);
                    ship.Move(wayPoint.North * amount, wayPoint.East * amount);
                } else {
                    wayPoint.Move(i);
                }
            }

            Console.WriteLine($"Distance from original position is {Math.Abs(ship.North) + Math.Abs(ship.East)}");
        }
    }
}