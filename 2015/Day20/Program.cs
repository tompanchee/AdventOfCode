using System;

namespace Day20
{
    class Program
    {
        static void Main(string[] args) {
            var input = 33100000;

            // NOTE: Extremely slow solution

            Console.WriteLine("Solving puzzle 1...");
            var min = 0;
            for (var house = 1; house < input / 10; house++) {
                var presents = 0;
                for (var elf = house; elf > 0; elf--) {
                    if (house % elf == 0) presents += 10 * elf;
                }

                if (presents >= input) {
                    min = house;
                    break;
                }
            }

            Console.WriteLine($"House {min} is the first one to receive at least {input} presents");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var min2 = 0;
            var minElf = 0;
            for (var house = 1; house < input; house++) {
                var presents = 0;
                for (var elf = house; elf > minElf; elf--) {
                    if (house / elf <= 50) {
                        if (house % elf == 0) presents += 11 * elf;
                    } else {
                        minElf = elf;
                    }
                }

                if (presents >= input) {
                    min2 = house;
                    break;
                }
            }

            Console.WriteLine($"House {min2} is the first one to receive at least {input} presents");
        }
    }
}