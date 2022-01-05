using System;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var d = new Display();
            foreach (var c in input) {
                d.Execute(c);
            }

            Console.WriteLine($"After card is swiped there are {d.Pixels.Cast<bool>().Count(p => p)} pixels lit");

            Console.WriteLine();
            Console.WriteLine("Solving puzzle 2...");
            d.Write();
        }
    }
}