using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Santa ends on floor {input[0].Count(c => c == '(') - input[0].Count(c => c == ')')}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var idx = 0;
            var floor = 0;
            while (floor != -1) {
                floor += input[0][idx] == '(' ? 1 : -1;
                idx++;
            }

            Console.WriteLine($"Santa enters the basement at step {idx}");
        }
    }
}