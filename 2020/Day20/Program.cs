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
            CalculatePossibleNeighbours(tiles);

            Console.WriteLine("Solving puzzle 1...");
            var corners = tiles.Where(t => t.PossibleNeighbours.Count == 2);
            var product = corners.Aggregate(1L, (current, tile) => current * tile.Id);
            Console.WriteLine($"Product of corner tile ids is {product}");
        }

        private static void CalculatePossibleNeighbours(Tile[] tiles) {
            foreach (var tile in tiles) {
                foreach (var edge in tile.Edges) {
                    var candidates = tiles.Where(t => t.Id != tile.Id && t.Edges.Contains(edge));
                    foreach (var candidate in candidates) {
                        if (!tile.PossibleNeighbours.Contains(candidate)) tile.PossibleNeighbours.Add(candidate);
                    }
                }
            }
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

            return tiles.ToArray();

            void AddTile() {
                if (tileLines.Count < 2) return;
                var id = int.Parse(tileLines[0][5..^1]);
                tiles.Add(new Tile(id, tileLines.Skip(1).ToArray()));
            }
        }
    }
}