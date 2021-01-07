using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20.Puzzle2
{
    class Solver
    {
        static readonly string[] monster = {
            "                  # ",
            "#    ##    ##    ###",
            " #  #  #  #  #  #   "
        };

        public static void Solve(Tile[] tiles) {
            Console.WriteLine("Solving puzzle 2...");
            var water = ImageAssembler.AssembleImage(tiles);
            var monsterCount = CalculateMonsters(water);

            var monsterPoints = monster.Aggregate(0, (a, r) => a + r.Count(c => c == '#'));
            var points = water.Rows.Aggregate(0, (a, r) => a + r.Count(c => c == '#'));

            Console.WriteLine($"Total roughness is {points - monsterCount * monsterPoints}");
        }

        static int CalculateMonsters(Tile water) {
            var monsterCoords = GetMonsterCoords();
            var height = monster.Length;
            var width = monster[0].Length;
            var count = 0;
            for (var i = 0; i < 4; i++) {
                var monsterCount = 0;
                for (var row = 0; row < water.Size - height; row++) {
                    for (var col = 0; col < water.Size - width; col++) {
                        var subArray = water.GetSubArray((row, col), (height, width));
                        if (HasMonster(subArray, monsterCoords)) monsterCount++;
                    }
                }

                if (monsterCount > count) count = monsterCount;
                water.Rotate();
            }

            return count;
        }

        private static bool HasMonster(IReadOnlyList<string> subArray, IEnumerable<(int, int )> monsterCoords) {
            foreach (var (row, col) in monsterCoords) {
                if (subArray[row][col] != '#') return false;
            }

            return true;
        }

        private static (int, int)[] GetMonsterCoords() {
            var coords = new List<(int, int)>();
            for (var row = 0; row < monster.Length; row++) {
                for (var col = 0; col < monster[row].Length; col++) {
                    if (monster[row][col] == '#') coords.Add((row, col));
                }
            }

            return coords.ToArray();
        }
    }
}