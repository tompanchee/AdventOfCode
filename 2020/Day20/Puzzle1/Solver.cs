using System;
using System.Linq;

namespace Day20.Puzzle1
{
    static class Solver
    {
        public static void Solve(Tile[] tiles) {
            Console.WriteLine("Solving puzzle 1...");
            var corners = tiles.Where(t => t.GetNeighbours(tiles).Count == 2);
            var product = corners.Aggregate(1L, (current, tile) => current * tile.Id);
            Console.WriteLine($"Product of corner tile ids is {product}");
        }
    }
}