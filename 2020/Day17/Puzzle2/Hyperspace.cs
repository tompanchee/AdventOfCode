using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17.Puzzle2
{
    class Hyperspace
    {
        readonly HashSet<(int, int, int, int)> activePoints = new HashSet <(int, int, int, int)>();

        public Status this[int x, int y, int z, int w]
        {
            get => activePoints.Contains((x, y, z, w)) ? Status.Active : Status.Inactive;
            set
            {
                var exists = activePoints.Contains((x, y, z, w));
                if (exists && value == Status.Inactive) activePoints.Remove((x, y, z, w));
                if (!exists && value == Status.Active) activePoints.Add((x, y, z, w));
            }
        }

        public int CountActiveNeighbours(int x, int y, int z, int w) {
            var count = 0;
            for (var dx = -1; dx <= 1; dx++) {
                for (var dy = -1; dy <= 1; dy++) {
                    for (var dz = -1; dz <= 1; dz++) {
                        for (var dw = -1; dw <= 1; dw++) {
                            if (dx == 0 && dy == 0 && dz == 0 && dw == 0) continue;
                            if (activePoints.Contains((x + dx, y + dy, z + dz, w + dw))) count++;
                        }
                    }
                }
            }

            return count;
        }

        public int ActivePointsCount => activePoints.Count;

        public int MinX => Min(p => p.Item1);
        public int MinY => Min(p => p.Item2);
        public int MinZ => Min(p => p.Item3);
        public int MinW => Min(p => p.Item4);

        public int MaxX => Max(p => p.Item1);
        public int MaxY => Max(p => p.Item2);
        public int MaxZ => Max(p => p.Item3);
        public int MaxW => Max(p => p.Item4);

        int Min(Func<(int, int, int, int), int> selector) {
            return ActivePointsCount == 0 ? 0 : activePoints.Min(selector);
        }

        int Max(Func<(int, int, int, int), int> selector) {
            return ActivePointsCount == 0 ? 0 : activePoints.Max(selector);
        }

        public enum Status
        {
            Inactive,
            Active
        }
    }
}