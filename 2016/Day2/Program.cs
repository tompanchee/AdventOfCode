using System;
using System.Globalization;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var pad = new Keypad(1, 1);  // start from 5
            var code = "";
            foreach (var instructions in input) {
                pad.Resolve(instructions);
                code += pad.Current.ToString(CultureInfo.InvariantCulture);
            }

            Console.WriteLine($"Bathroom code is {code}");
            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var pad2 = new Keypad2(2, 0);  // start from 5
            var code2 = "";
            foreach (var instructions in input) {
                pad2.Resolve(instructions);
                code2 += pad2.Current;
            }

            Console.WriteLine($"Bathroom code is {code2}");
        }
    }
}