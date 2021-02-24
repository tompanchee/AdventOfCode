using System;
using System.Text;

namespace Day10
{
    class Program
    {
        static void Main(string[] args) {
            const string INPUT = "1321131112";

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Sequence length after 40 rounds is {Play(INPUT, 40).Length}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            Console.WriteLine($"Sequence length after 50 rounds is {Play(INPUT, 50).Length}");
        }

        static string Play(string input, int rounds) {
            for (var i = 0; i < rounds; i++) {
                input = LookAndSay(input);
            }

            return input;
        }

        static string LookAndSay(string input) {
            var result = new StringBuilder();
            var length = input.Length;

            for (var i = 0; i < length;) {
                var current = input[i];
                var count = 1;
                for (var j = i + 1; j < length; j++) {
                    if (current == input[j]) count++;
                    else break;
                }

                result.Append(count).Append(current);
                i += count;
            }

            return result.ToString();
        }
    }
}