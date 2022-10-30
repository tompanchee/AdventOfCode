using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var items = ParseInputAndInitialize(input);

            Console.WriteLine("Solving puzzle 1...");
            var bots = Bots();
            Bot askedBot = null;
            var twoChipBots = bots.Where(b => b.Chips.Count == 2).ToList();
            while (twoChipBots.Any()) {
                foreach (var bot in twoChipBots) {
                    if (bot.Chips.Contains(17) && bot.Chips.Contains(61)) {
                        askedBot = bot;
                    }

                    WriteToConsole(bot);
                    bot.Give();
                }

                twoChipBots = bots.Where(b => b.Chips.Count == 2).ToList();
            }

            Console.WriteLine();
            Console.WriteLine($"Id of bot handling chips 17 and 61 is {askedBot.ID}");

            Console.WriteLine();
            Console.WriteLine("Solving puzzle 2...");
            var outputs = Outputs();
            var total = outputs.Single(o => o.ID == 0).Chips[0]
                        * outputs.Single(o => o.ID == 1).Chips[0]
                        * outputs.Single(o => o.ID == 2).Chips[0];
            Console.WriteLine($"Product of outputs 0, 1 and 2 is {total}");

            IList<Bot> Bots() => items.Where(i => i.GetType() == typeof(Bot)).Cast<Bot>().ToList();
            IList<Output> Outputs() => items.Where(i => i.GetType() == typeof(Output)).Cast<Output>().ToList();
        }

        static void WriteToConsole(Bot bot) {
            var ht = bot.HighTarget.GetType() == typeof(Bot) ? "bot" : "output";
            var lt = bot.LowTarget.GetType() == typeof(Bot) ? "bot" : "output";
            Console.WriteLine($"Bot {bot.ID} gives {bot.LowChip} to {lt} {bot.LowTarget.ID} and {bot.HighChip} to {ht} {bot.HighTarget.ID}");
        }

        static IList<Item> ParseInputAndInitialize(string[] input) {
            var bots = new List<Bot>();
            var outputs = new List<Output>();
            var init = new List<(int target, int value)>();

            foreach (var line in input) {
                var split = line.Split(' ');
                if (split[0] == "value") {
                    init.Add((int.Parse(split[5]), int.Parse(split[1])));
                } else {
                    var id = int.Parse(split[1]);
                    var lt = int.Parse(split[6]);
                    var ht = int.Parse(split[11]);

                    var bot = ResolveBot(id);
                    if (split[5] == "output") {
                        bot.LowTarget = ResolveOutput(lt);
                    } else {
                        bot.LowTarget = ResolveBot(lt);
                    }

                    if (split[10] == "output") {
                        bot.HighTarget = ResolveOutput(ht);
                    } else {
                        bot.HighTarget = ResolveBot(ht);
                    }
                }
            }

            // Initialize
            foreach (var (target, value) in init) {
                var t = bots.Single(i => i.ID == target);
                t.Add(value);
            }

            var items = new List<Item>(bots);
            items.AddRange(outputs);
            return items;

            Bot ResolveBot(int id) {
                var bot = bots.FirstOrDefault(b => b.ID == id);
                if (bot == null) {
                    bot = new Bot(id);
                    bots.Add(bot);
                }

                return bot;
            }

            Output ResolveOutput(int id) {
                var output = outputs.FirstOrDefault(o => o.ID == id);
                if (output == null) {
                    output = new Output(id);
                    outputs.Add(output);
                }

                return output;
            }
        }
    }
}