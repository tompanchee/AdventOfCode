using System;

namespace Day23
{
    class Program
    {
        static void Main(string[] args) {
            const string input = "253149867";

            Console.WriteLine("Solving puzzle 1...");
            var game = new CrabCups(input.Length, input);
            game.Play(100);
            var node = game.Cups.Find(1);
            var sum = 0L;
            for (var i = 0; i < 8; i++) {
                node = node.GetNextNode(game.Cups);
                sum = sum * 10 + node.Value;
            }
            Console.WriteLine($"Last sequence is {sum}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            game = new CrabCups(1_000_000, input);
            game.Play(10_000_000);
            node = game.Cups.Find(1);
            long v1 = node.GetNextNode(game.Cups).Value;
            long v2 = node.GetNextNode(game.Cups).GetNextNode(game.Cups).Value;
            var result = v1 * v2;
            Console.WriteLine($"Result is {result}");
        }
    }
}