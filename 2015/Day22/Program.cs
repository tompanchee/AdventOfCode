using System;

namespace Day22
{
    class Program
    {
        static void Main(string[] args) {
            var hero = (50, 500);
            var boss = (58, 9);

            Console.WriteLine("Solving puzzle 1...");
            var solver = new Solver(hero, boss);
            solver.Solve(debug: true);
            Console.WriteLine($"Minimum total mana to win boss is {solver.BestGame.TotalMana}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            solver = new Solver(hero, boss, true);
            solver.Solve(debug: true);
            Console.WriteLine($"Minimum total mana to win boss in hard mode is {solver.BestGame.TotalMana}");
        }
    }
}