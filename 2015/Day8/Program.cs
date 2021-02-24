using System;
using System.IO;

namespace Day8
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var diff = 0;
            foreach (var line in input) {
                diff += 2; // Start and end quote
                for (var i = 0; i < line.Length - 1; i++) {
                    if (line[i] == '\\') {
                        switch (line[i + 1]) {
                            case '\\':
                            case '\"':
                                diff += 1;
                                i++;
                                break;
                            case 'x':
                                diff += 3;
                                i += 3;
                                break;
                        }
                    }
                }
            }

            Console.WriteLine($"Difference is {diff}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            diff = 0;
            foreach (var line in input) {
                var d = 2;
                foreach (var c in line) {
                    switch (c) {
                        case '\\':
                        case '\"':
                            d += 1;
                            break;
                    }
                }

                diff += d;
            }

            Console.WriteLine($"Difference is  {diff}");
        }
    }
}