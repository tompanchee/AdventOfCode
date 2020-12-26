using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    class Hex
    {
        private static readonly Dictionary<string, (int, int)> moves = new() {
            {"ne", (1, -1)},
            {"e", (2, 0)},
            {"se", (1, 1)},
            {"sw", (-1, 1)},
            {"w", (-2, 0)},
            {"nw", (-1, -1)},
        };

        private readonly int x;
        private readonly int y;

        public Hex() : this(0, 0) { }

        public Hex(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Hex((int, int) coordinates) : this(coordinates.Item1, coordinates.Item2) { }

        public (int x, int y) Coordinates => (x, y);

        public Hex Move(string move) {
            var m = move.ToLowerInvariant();
            if (!moves.ContainsKey(m)) throw new InvalidOperationException($"Unknown move {move}");
            return new Hex(x + moves[m].Item1, y + moves[m].Item2);
        }

        public IEnumerable<(int, int)> GetNeighbourCoordinates() => moves.Select(d => (x + d.Value.Item1, y + d.Value.Item2)).ToList();
    }
}