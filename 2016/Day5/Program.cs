using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Day5
{
    class Program
    {
        static readonly char[] allowedPosition = {'0', '1', '2', '3', '4', '5', '6', '7'};

        static void Main(string[] args) {
            var input = "uqwqemis";

            Console.WriteLine("Solving puzzle 1...");
            var builder = new StringBuilder();
            var idx = 0L;
            while (builder.Length < 8) {
                var md5 = CalculateMd5($"{input}{idx}");
                if (md5.StartsWith("00000")) {
                    builder.Append(md5[5]);
                    Console.WriteLine($"{builder} ({idx})");
                }

                idx++;
            }

            Console.WriteLine($"Password is {builder}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            builder = new StringBuilder("________");
            idx = 0L;

            while (builder.ToString().Contains('_')) {
                var md5 = CalculateMd5($"{input}{idx}");
                if (md5.StartsWith("00000") && allowedPosition.Contains(md5[5])) {
                    var pos = int.Parse(md5[5..6]);
                    if (builder[pos] == '_') {
                        builder[pos] = md5[6];
                        Console.WriteLine($"{builder} ({idx})");
                    }
                }

                idx++;
            }

            Console.WriteLine($"Password is {builder}");
        }

        static string CalculateMd5(string input) {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var b in hashBytes) {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}