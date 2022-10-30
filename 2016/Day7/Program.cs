using System;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var count = input.Select(s => new IpV7(s)).Count(i => i.SupportsTLS);
            Console.WriteLine($"{count} addresses support TLS");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            count = input.Select(s => new IpV7(s)).Count(i => i.SupportsSSL);
            Console.WriteLine($"{count} addresses support SSL");
        }
    }
}