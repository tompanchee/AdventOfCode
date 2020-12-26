using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine("Solving puzzle 1...");
            var blackTiles = new HashSet<(int, int)>();
            foreach (var line in input) {
                var tile = new Hex();
                tile = ParseMoves(line).Aggregate(tile, (current, move) => current.Move(move));
                if (blackTiles.Contains(tile.Coordinates)) blackTiles.Remove(tile.Coordinates);
                else blackTiles.Add(tile.Coordinates);
            }

            Console.WriteLine($"Finally there are {blackTiles.Count} black tiles");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            for (var i = 0; i < 100; i++) {
                var minX = blackTiles.Min(h => h.Item1) - 4;
                var maxX = blackTiles.Max(h => h.Item1) + 4;
                var minY = blackTiles.Min(h => h.Item2) - 2;
                var maxY = blackTiles.Max(h => h.Item2) + 2;

                var nextGen = new HashSet<(int, int)>();
                for (var x = minX; x < maxX; x++) {
                    for (var y = minY; y < maxY; y++) {
                        if ((x + y) % 2 != 0) continue;
                        var hex = new Hex(x, y);
                        var blackNeighbourCount = hex.GetNeighbourCoordinates().Count(neighbour => blackTiles.Contains(new Hex(neighbour).Coordinates));
                        if (blackTiles.Contains(hex.Coordinates)) {
                            if (blackNeighbourCount > 0 && blackNeighbourCount <= 2) nextGen.Add(hex.Coordinates);
                        } else {
                            if (blackNeighbourCount == 2) nextGen.Add(hex.Coordinates);
                        }
                    }
                }

                blackTiles = nextGen;
            }

            Console.WriteLine($"Finally there are {blackTiles.Count} black tiles");
        }

        private static IEnumerable<string> ParseMoves(string line) {
            var moves = new List<string>();
            for (var i = 0; i < line.Length; i++) {
                if (line[i] == 'e' || line[i] == 'w') {
                    moves.Add(line.Substring(i, 1));
                    continue;
                }

                moves.Add(line.Substring(i, 2));
                i += 1;
            }

            return moves;
        }
    }
}