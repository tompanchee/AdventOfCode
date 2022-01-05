using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day9
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt").First().Replace(" ", "");

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Decompressed string length is {Decompress(input).Length}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            Console.WriteLine($"Decompressed string length is {GetDecompressedLengthV2(input)}");
        }

        static string Decompress(string value) {
            var result = new StringBuilder();
            var pos = 0;

            while (pos < value.Length) {
                var startPos = value[pos..].IndexOf('(');
                var endPos = value[pos..].IndexOf(')');

                if (startPos < 0) {
                    // no compression left, add rest of string
                    result.Append(value[pos..]);
                    break;
                }

                var mod = value[(pos + startPos + 1)..(pos + endPos)].Split('x');
                var chars = int.Parse(mod[0]);
                var repeats = int.Parse(mod[1]);

                result.Append(value[..startPos]); // Non compressed
                var toRepeat = value[(endPos + 1)..(endPos + chars + 1)];
                var repeated = new StringBuilder().Insert(0, toRepeat, repeats).ToString();
                result.Append(repeated);
                pos += endPos + chars + 1;
            }

            return result.ToString();
        }

        static long GetDecompressedLengthV2(string value) {
            if (!value.Contains('(')) return value.Length;

            var total = 0L;
            var pos = 0;

            while (pos < value.Length) {
                if (value[pos] != '(') {
                    total++;
                    pos++;
                    continue;
                }

                var endPos = value[pos..].IndexOf(')');
                var mod = value[(pos + 1)..(pos + endPos)].Split('x');
                var chars = int.Parse(mod[0]);
                var repeats = int.Parse(mod[1]);

                var subString = value[(pos + endPos + 1)..(pos + endPos + chars + 1)];
                total += repeats * GetDecompressedLengthV2(subString);
                pos += endPos + 1 + chars;
            }

            return total;
        }
    }
}