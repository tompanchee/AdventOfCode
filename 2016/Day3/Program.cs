using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var triangles = input.Where(IsTriangle);
            Console.WriteLine($"There are {triangles.Count()} triangles");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var (col1, col2, col3) = ParseColumns(input);
            var triangleCount = CountTriangles(col1) + CountTriangles(col2) + CountTriangles(col3);
            Console.WriteLine($"There are {triangleCount} triangles");
        }

        private static int CountTriangles(int[] col) {
            var i = 0;
            var count = 0;
            while (i < col.Length) {
                if (IsTriangle(col.Skip(i).Take(3))) count++;
                i += 3;
            }

            return count;
        }

        static bool IsTriangle(string line) {
            var sides = new[] {int.Parse(line[..5]), int.Parse(line[7..10]), int.Parse(line[12..])};
            return IsTriangle(sides);
        }

        static bool IsTriangle(IEnumerable<int> sides) {
            var sorted = sides.OrderBy(s => s).ToArray();
            return sorted[0] + sorted[1] > sorted[2];
        }

        private static (int[], int[], int[]) ParseColumns(string[] input) {
            var col1 = new List<int>();
            var col2 = new List<int>();
            var col3 = new List<int>();

            foreach (var line in input) {
                var (item1, item2, item3) = (int.Parse(line[..5]), int.Parse(line[7..10]), int.Parse(line[12..]));
                col1.Add(item1);
                col2.Add(item2);
                col3.Add(item3);
            }

            return (
                col1.ToArray(),
                col2.ToArray(),
                col3.ToArray()
            );
        }
    }
}