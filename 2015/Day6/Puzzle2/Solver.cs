using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6.Puzzle2
{
    static class Solver
    {
        public static void Solve(Instruction[] instructions) {
            Console.WriteLine("Solving puzzle 2...");
            IDictionary<(int, int), int> lights = new Dictionary<(int, int), int>();

            foreach (var instruction in instructions) {
                switch (instruction.Action) {
                    case Instruction.EAction.Off:
                        AdjustLight(lights, instruction.Range, -1);
                        break;
                    case Instruction.EAction.On:
                        AdjustLight(lights, instruction.Range, 1);
                        break;
                    case Instruction.EAction.Toggle:
                        AdjustLight(lights, instruction.Range, 2);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Console.WriteLine($"Total brightness of lights is {lights.Values.Sum()}");
        }

        private static void AdjustLight(IDictionary<(int, int), int> lights, ((int row, int col) topLeft, (int row, int col) bottomRight) range, int adjustment) {
            for (var row = range.topLeft.row; row <= range.bottomRight.row; row++) {
                for (var col = range.topLeft.col; col <= range.bottomRight.col; col++) {
                    if (!lights.ContainsKey((row, col))) lights.Add((row, col), 0);
                    lights[(row, col)] = lights[(row, col)] + adjustment;
                    if (lights[(row, col)] < 0) lights.Remove((row, col));
                }
            }
        }
    }
}