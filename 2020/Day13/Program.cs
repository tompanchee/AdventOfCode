using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Solve1(input);
            Console.WriteLine();
            Solve2(input[1]);
        }

        private static void Solve1(string[] input) {
            Console.WriteLine("Solving puzzle 1...");

            var myMinute = int.Parse(input[0]);
            var busIntervals = input[1]
                .Split(',', StringSplitOptions.TrimEntries)
                .Where(i => i != "x")
                .Select(int.Parse)
                .ToArray();

            var minWaitTime = int.MaxValue;
            var minWaitBus = 0;
            foreach (var bus in busIntervals) {
                var mod = myMinute % bus;
                var waitTime = bus - mod;
                if (waitTime < minWaitTime) {
                    minWaitTime = waitTime;
                    minWaitBus = bus;
                }
            }

            Console.WriteLine($"Min wait time {minWaitTime} for bus {minWaitBus} => {minWaitTime * minWaitBus}");
        }

        private static void Solve2(string input) {
            Console.WriteLine("Solving puzzle 2...");

            var buses = new List<Bus>();
            var split = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < split.Length; i++) {
                if (split[i] == "x") continue;
                buses.Add(new Bus {Offset = i, Interval = long.Parse(split[i])});
            }

            var current = buses[0];
            var minute = 0L;
            for (var i = 1; i < buses.Count; i++) {
                while (buses[i].Interval - minute % buses[i].Interval != buses[i].Offset % buses[i].Interval) minute += current.Interval;

                current = new Bus {
                    Interval = current.Interval * buses[i].Interval
                };
            }

            Console.WriteLine($"Prize timestamp is {minute}");
        }

        class Bus
        {
            public long Interval { get; set; }
            public long Offset { get; set; }
        }
    }
}