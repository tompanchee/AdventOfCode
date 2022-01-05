using System;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllText("input.txt");
            var length = input.Length;

            Console.WriteLine("Solving puzzle 1...");
            var sum = 0;
            for (var i = 0; i < length; i++) {
                var i2 = i + 1;
                if (i2 == length) i2 = 0;
                if (input[i] == input[i2]) sum += int.Parse(new[] {input[i]});
            }
            Console.WriteLine($"Captcha is {sum}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");

            sum = 0;
            var half = length / 2;
            for (var i = 0; i < length; i++) {
                var i2 = i + half;
                if (i2 >= length) i2 -= length;
                if (input[i] == input[i2])
                    sum += int.Parse(new[] {
                        input[i]
                    });
            }
            Console.WriteLine($"Captcha is {sum}");
        }
    }
}