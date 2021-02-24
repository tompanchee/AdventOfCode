using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Day12
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllText("input.txt");
            dynamic o = JsonConvert.DeserializeObject(input);

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Sum of all numbers is {GetSum(o)}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            Console.WriteLine($"Sum of all numbers is {GetSum(o, "red")}");
        }

        // Checked solution megathread to find this elegant solution
        static long GetSum(JObject o, string avoid = null) {
            var hasAvoidedValue = o.Properties()
                .Select(a => a.Value).OfType<JValue>()
                .Select(v => v.Value).Contains(avoid);
            if (hasAvoidedValue) return 0;

            return o.Properties().Sum((dynamic a) => (long) GetSum(a.Value, avoid));
        }

        static long GetSum(JArray arr, string avoid = null) => arr.Sum((dynamic a) => (long) GetSum(a, avoid));

        static long GetSum(JValue val, string avoid) => val.Type == JTokenType.Integer ? (long) val.Value : 0;
    }
}