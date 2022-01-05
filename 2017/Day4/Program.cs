using System;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args) {
            var phrases = File.ReadAllLines("input.txt").Select(l => l.Split(' ').ToArray()).ToArray();

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Input contains {phrases.Count(IsValid)} valid phrases");

            Console.WriteLine();
            Console.WriteLine("Solving puzzle 2...");
            Console.WriteLine($"Input contains {phrases.Count(IsValid2)} valid phrases");

        }

        static bool IsValid(string[] phrase) {
            for (var i = 0; i < phrase.Length - 1; i++) {
                if (phrase.Skip(i + 1).Contains(phrase[i])) return false;
            }

            return true;
        }

        static bool IsValid2(string[] phrase) {
            for (var i = 0; i < phrase.Length - 1; i++) {
                var ordered = Order(phrase[i]);
                if (phrase.Skip(i + 1).Select(Order).Contains(ordered)) return false;
            }

            return true;

            static string Order(string value) => new(value.OrderBy(c => c).ToArray());
        }
    }
}