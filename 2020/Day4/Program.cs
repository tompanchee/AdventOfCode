using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            var passports = new List<Passport>();
            var record = "";
            foreach (var line in input) {
                if (string.IsNullOrWhiteSpace(line)) {
                    passports.Add(Passport.Init(record.TrimStart()));
                    record = "";
                    continue;
                }

                record = $"{record} {line}";
            }

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"There are {passports.Count(p => p.IsValid1)} valid passports");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            Console.WriteLine($"There are {passports.Count(p => p.IsValid2)} valid passports");
        }
    }
}