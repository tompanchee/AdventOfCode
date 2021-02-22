using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day20
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");
            var tiles = ParseInput(input);

            Puzzle1.Solver.Solve(tiles);
            Console.WriteLine();
            Puzzle2.Solver.Solve(tiles);
        }


        private static Tile[] ParseInput(string[] input) {
            var tiles = new List<Tile>();

            var tileLines = new List<string>();
            foreach (var line in input) {
                if (string.IsNullOrWhiteSpace(line)) {
                    AddTile();
                    tileLines = new List<string>();
                    continue;
                }

                tileLines.Add(line);
            }

            AddTile(); // Add last tile
            return tiles.ToArray();

            void AddTile() {
                if (tileLines.Count < 2) return;
                var id = int.Parse(tileLines[0][5..^1]);
                tiles.Add(new Tile(id, tileLines.Skip(1).ToArray()));
            }
        }
    }
}