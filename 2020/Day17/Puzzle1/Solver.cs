using System;

namespace Day17.Puzzle1
{
    static class Solver
    {
        public static void Solve(string[] input) {
            Console.WriteLine("Solving puzzle 1...");
            var current = ParseInput(input);
            for (var i = 0; i < 6; i++) {
                var next = new Space();
                for (var x = current.MinX - 1; x <= current.MaxX + 1; x++) {
                    for (var y = current.MinY - 1; y <= current.MaxY + 1; y++) {
                        for (var z = current.MinZ - 1; z <= current.MaxZ + 1; z++) {
                            var activeNeighbours = current.CountActiveNeighbours(x, y, z);
                            if (current[x, y, z] == Space.Status.Active) {
                                if (activeNeighbours == 2 || activeNeighbours == 3) {
                                    next[x, y, z] = Space.Status.Active;
                                } else {
                                    next[x, y, z] = Space.Status.Inactive;
                                }
                            } else {
                                if (activeNeighbours == 3) {
                                    next[x, y, z] = Space.Status.Active;
                                }
                            }
                        }
                    }
                }

                current = next;
            }

            Console.WriteLine($"After 6 rounds there are {current.ActivePointsCount} active cubes");
        }

        private static Space ParseInput(string[] input) {
            var space = new Space();
            for (var y = 0; y < input.Length; y++) {
                if (string.IsNullOrWhiteSpace(input[y])) continue;
                for (var x = 0; x < input[y].Length; x++) {
                    if (input[y][x] == '#') space[x, y, 0] = Space.Status.Active;
                }
            }

            return space;
        }
    }
}