using System;
using System.IO;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var reindeer = ParseInput(input);

            Console.WriteLine("Solving puzzle 1...");
            var maxDistance = reindeer.Select(r => r.PositionAt(2503)).Max();
            Console.WriteLine($"At 2503 s reindeer leading has traveled {maxDistance} km");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var scores = reindeer.ToDictionary(r => r.Name, r => 0);
            for (var i = 1; i <= 2503; i++) {
                var reindeerDistances = reindeer.Select(r => (r.Name, r.PositionAt(i))).ToList();
                var max = reindeerDistances.Max(d => d.Item2);
                var leading = reindeerDistances.Where(d => d.Item2 == max).Select(d => d.Name);
                foreach (var name in leading) {
                    scores[name]++;
                }
            }

            var maxPoints = scores.Values.Max();
            Console.WriteLine($"At 2503 s reindeer leading has {maxPoints} points");
        }

        private static Reindeer[] ParseInput(string[] input) {
            return (from line
                        in input
                    select line.Split(' ')
                    into split
                    let name = split[0]
                    let speed = int.Parse(split[3])
                    let flyTime = int.Parse(split[6])
                    let restTime = int.Parse(split[13])
                    select new Reindeer(name, speed, flyTime, restTime))
                .ToArray();
        }
    }
}