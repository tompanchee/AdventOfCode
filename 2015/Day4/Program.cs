using System;
using System.Security.Cryptography;
using System.Text;

namespace Day4
{
    class Program
    {
        static void Main(string[] args) {
            var secret = "yzbqklnj";

            Console.WriteLine("Solving puzzle 1...");
            var hash = "";
            var number = 0L;
            while (!hash.StartsWith("00000")) {
                number++;
                hash = CalculateHash($"{secret}{number}");
            }

            Console.WriteLine($"Santa finds number {number}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            hash = "";
            number = 0L;
            while (!hash.StartsWith("000000")) {
                number++;
                hash = CalculateHash($"{secret}{number}");
            }

            Console.WriteLine($"Santa finds number {number}");
        }

        static string CalculateHash(string input) {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var b in hashBytes) {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}