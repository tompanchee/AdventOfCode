using System;
using System.Collections.Generic;

namespace Day6.Puzzle1
{
    static class Solver
    {
        public static void Solve(IEnumerable<Instruction> instructions) {
            Console.WriteLine("Solving puzzle 1...");
            var onLights = new HashSet<(int, int)>();

            foreach (var instruction in instructions) {
                switch (instruction.Action) {
                    case Instruction.EAction.Off:
                        TurnOff(onLights, instruction.Range);
                        break;
                    case Instruction.EAction.On:
                        TurnOn(onLights, instruction.Range);
                        break;
                    case Instruction.EAction.Toggle:
                        Toggle(onLights, instruction.Range);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Console.WriteLine($"Finally there are {onLights.Count} lights on");
        }

        private static void TurnOff(ICollection<(int, int)> onLights, ((int row, int col) topLeft, (int row, int col) bottomRight) range) {
            for (var row = range.topLeft.row; row <= range.bottomRight.row; row++) {
                for (var col = range.topLeft.col; col <= range.bottomRight.col; col++) {
                    if (onLights.Contains((row, col))) onLights.Remove((row, col));
                }
            }
        }

        private static void TurnOn(ISet<(int, int)> onLights, ((int row, int col) topLeft, (int row, int col) bottomRight) range) {
            for (var row = range.topLeft.row; row <= range.bottomRight.row; row++) {
                for (var col = range.topLeft.col; col <= range.bottomRight.col; col++) {
                    if (!onLights.Contains((row, col))) onLights.Add((row, col));
                }
            }
        }

        private static void Toggle(ISet<(int, int)> onLights, ((int row, int col) topLeft, (int row, int col) bottomRight) range) {
            for (var row = range.topLeft.row; row <= range.bottomRight.row; row++) {
                for (var col = range.topLeft.col; col <= range.bottomRight.col; col++) {
                    if (onLights.Contains((row, col))) onLights.Remove((row, col));
                    else onLights.Add((row, col));
                }
            }
        }
    }
}