using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var boss = ParseInput(input);
            var equipments = Shop.GetEquipmentCombinations();
            var hero = new Fighter {HitPoints = 100};

            Console.WriteLine("Solving puzzle 1...");
            var minCost = int.MaxValue;

            foreach (var equipment in equipments) {
                var cost = equipment.Sum(e => e.Cost);
                hero.Damage = equipment.Sum(e => e.Damage);
                hero.Armor = equipment.Sum(e => e.Armor);
                if (Fight(hero, boss) == hero) {
                    if (cost < minCost) minCost = cost;
                }
            }

            Console.WriteLine($"Minimum cost to win the battle is {minCost}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var maxCost = 0;

            foreach (var equipment in equipments) {
                var cost = equipment.Sum(e => e.Cost);
                hero.Damage = equipment.Sum(e => e.Damage);
                hero.Armor = equipment.Sum(e => e.Armor);
                if (Fight(hero, boss) == boss) {
                    if (cost > maxCost) maxCost = cost;
                }
            }

            Console.WriteLine($"Maximum cost to lose the battle is {maxCost}");
        }

        static Fighter Fight(Fighter p1, Fighter p2) {
            var p1Damage = Math.Max(p1.Damage - p2.Armor, 1);
            var p2Damage = Math.Max(p2.Damage - p1.Armor, 1);

            var p1Hp = p1.HitPoints;
            var p2Hp = p2.HitPoints;

            while (true) {
                p2Hp -= p1Damage;
                if (p2Hp <= 0) return p1;

                p1Hp -= p2Damage;
                if (p1Hp <= 0) return p2;
            }
        }

        private static Fighter ParseInput(IEnumerable<string> input) {
            var person = new Fighter();
            foreach (var line in input) {
                if (line.StartsWith("Hit Points:")) person.HitPoints = int.Parse(line[11..]);
                if (line.StartsWith("Damage:")) person.Damage = int.Parse(line[8..]);
                if (line.StartsWith("Armor:")) person.Armor = int.Parse(line[6..]);
            }

            return person;
        }
    }
}