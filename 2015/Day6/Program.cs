using System;
using System.IO;
using System.Linq;
using Day6.Puzzle1;

namespace Day6
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var instructions = input.Select(Instruction.FromInputLine).ToArray();

            Solver.Solve(instructions);
            Console.WriteLine();
            Puzzle2.Solver.Solve(instructions);
        }
    }

    class Instruction
    {
        private static readonly int onLen = "turn on".Length;
        private static readonly int offLen = "turn off".Length;
        private static readonly int toggleLen = "toggle".Length;

        public static Instruction FromInputLine(string line) {
            var action = EAction.Toggle;
            if (line.StartsWith("turn on")) action = EAction.On;
            if (line.StartsWith("turn off")) action = EAction.Off;

            var rangeStr = action switch {
                EAction.On => line[onLen..],
                EAction.Off => line[offLen..],
                EAction.Toggle => line[toggleLen..],
                _ => throw new ArgumentOutOfRangeException()
            };

            var split = rangeStr.Split("through");
            var range = (ParsePoint(split[0]), ParsePoint(split[1]));

            return new Instruction(action, range);

            static (int, int ) ParsePoint(string point) {
                var r = point.Trim().Split(',');
                return (int.Parse(r[0]), int.Parse(r[1]));
            }
        }

        Instruction(EAction action, ((int row, int col) topLeft, (int row, int col) bottomRight) range) {
            this.Action = action;
            this.Range = range;
        }

        public EAction Action { get; }

        public ((int row, int col) topLeft, (int row, int col) bottomRight) Range { get; }

        public enum EAction
        {
            On,
            Off,
            Toggle
        }
    }
}