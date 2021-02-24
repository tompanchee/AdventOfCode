using System;
using System.IO;

namespace Day23
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var computer = new Computer(input);
            computer.Execute();
            Console.WriteLine($"Register B has value {computer.B} when program is executed");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 1...");
            // Reset program and set register A to 1
            computer = new Computer(input) {
                A = 1
            };
            computer.Execute();
            Console.WriteLine($"Register B has value {computer.B} when program is executed");
        }
    }
}