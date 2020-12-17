using System;

namespace Day17.Puzzle2
{
    static class Solver
    {
        // Lazy copy-paste of Puzzle 1 with added 4th dimension
        public static void Solve(string[] input) {
            Console.WriteLine("Solving puzzle 2...");
            var current = ParseInput(input);
            for (var i = 0; i < 6; i++) {
                var next = new Hyperspace();
                for (var x = current.MinX - 1; x <= current.MaxX + 1; x++) {
                    for (var y = current.MinY - 1; y <= current.MaxY + 1; y++) {
                        for (var z = current.MinZ - 1; z <= current.MaxZ + 1; z++) {
                            for (var w = current.MinW - 1; w <= current.MaxW + 1; w++) {
                                var activeNeighbours = current.CountActiveNeighbours(x, y, z, w);
                                if (current[x, y, z, w] == Hyperspace.Status.Active) {
                                    if (activeNeighbours == 2 || activeNeighbours == 3) {
                                        next[x, y, z, w] = Hyperspace.Status.Active;
                                    } else {
                                        next[x, y, z, w] = Hyperspace.Status.Inactive;
                                    }
                                } else {
                                    if (activeNeighbours == 3) {
                                        next[x, y, z, w] = Hyperspace.Status.Active;
                                    }
                                }
                            }
                        }
                    }
                }

                current = next;
            }

            Console.WriteLine($"After 6 rounds there are {current.ActivePointsCount} active cubes");
        }

        private static Hyperspace ParseInput(string[] input) {
            var space = new Hyperspace();
            for (var y = 0; y < input.Length; y++) {
                if (string.IsNullOrWhiteSpace(input[y])) continue;
                for (var x = 0; x < input[y].Length; x++) {
                    if (input[y][x] == '#') space[x, y, 0, 0] = Hyperspace.Status.Active;
                }
            }

            return space;
        }
    }
}