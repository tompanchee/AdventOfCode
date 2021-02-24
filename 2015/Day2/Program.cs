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
            var boxes = ParseInput(input).ToList();

            Console.WriteLine("Solving puzzle 1...");
            var paperNeeded = boxes.Aggregate(0, (total, box) => total + box.TotalArea + box.FaceAreas.Min());
            Console.WriteLine($"The elves need to order {paperNeeded} square feet of wrapping paper.");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var ribbonNeeded = boxes.Aggregate(0, (total, box) => total + box.FacePerimeters.Min() + box.Volume);
            Console.WriteLine($"The elves need to order {ribbonNeeded} feet of ribbon.");
        }

        private static IEnumerable<Box> ParseInput(IEnumerable<string> input) {
            return input.Select(line => new Box(line.Split('x').Select(int.Parse).ToArray()));
        }
    }

    class Box
    {
        private Box(int length, int width, int height) {
            Length = length;
            Width = width;
            Height = height;
        }

        public Box(int[] dimensions) : this(dimensions[0], dimensions[1], dimensions[2]) { }

        public int[] FaceAreas => new[] {Length * Width, Length * Height, Width * Height};

        public int TotalArea => FaceAreas.Aggregate(0, (total, face) => total + 2 * face);

        public int[] FacePerimeters => new[] {2 * Length + 2 * Width, 2 * Length + 2 * Height, 2 * Width + 2 * Height};

        public int Volume => Length * Width * Height;

        private int Length { get; }
        private int Width { get; }
        private int Height { get; }
    }
}