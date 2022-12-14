using AOCCommon;
using Serilog.Core;

namespace Day14;

[Day(14, "Regolith Reservoir")]
internal class Solver : SolverBase
{
    const char ROCK = '#';
    const char SAND = 'o';
    readonly Dictionary<(int row, int col), char> blockedTiles = new();
    int bottomRock;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        var count = 0;

        var freeFalling = false;
        while (!freeFalling) {
            freeFalling = DropSand(loc => blockedTiles.ContainsKey(loc), false);
            if (!freeFalling) count++;
        }

        logger.Information("The cave is filled with {0} units of sand", count);
    }

    public override void Solve2() {
        var count = 0;

        // Clean sand
        while (blockedTiles.ContainsValue(SAND)) {
            var loc = blockedTiles.First(p => p.Value == SAND).Key;
            blockedTiles.Remove(loc);
        }

        var full = false;
        while (!full) {
            full = DropSand(((int row, int col) loc) => blockedTiles.ContainsKey(loc) || loc.row == bottomRock + 2, true);
            if (!full) count++;
        }

        logger.Information("The cave is filled with {0} units of sand", count);
    }

    bool DropSand(Func<(int, int), bool> isBlocked, bool hasFloor) {
        (int row, int col) current = (0, 500);

        while (true) {
            var next = (row: current.row + 1, current.col);

            if (isBlocked(next)) {
                // Try left
                next = (row: current.row + 1, col: current.col - 1);
                if (isBlocked(next)) {
                    // Try right
                    next = (row: current.row + 1, col: current.col + 1);
                    logger.Debug("Row = {0}", next.row);
                    if (isBlocked(next)) {
                        logger.Debug("Adding sand at ({0},{1})", current.row, current.col);
                        try {
                            blockedTiles.Add(current, SAND);
                        } catch {
                            // Workaround for a bug(feature) that tries to add the start position twice :)
                            return true;
                        }

                        return false;
                    }
                }
            }

            current = next;

            if (hasFloor && blockedTiles.ContainsKey((0, 500))) return true;
            if (!hasFloor && current.row >= bottomRock) return true;
        }
    }

    protected override void PostConstruct() {
        logger.Information("Scanning cave...");

        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;

            var split = row.Split("->", StringSplitOptions.TrimEntries);
            for (var i = 0; i < split.Length - 1; i++) {
                var start = split[i];
                var end = split[i + 1];

                AddRocks(start, end);
            }
        }

        bottomRock = blockedTiles.Select(t => t.Key.row).Max();
    }

    void AddRocks(string start, string end) {
        var s = start.Split(',');
        var e = end.Split(',');

        var r1 = int.Parse(s[1]);
        var c1 = int.Parse(s[0]);
        var r2 = int.Parse(e[1]);
        var c2 = int.Parse(e[0]);

        if (r1 == r2) // Loop through columns
            for (var c = Math.Min(c1, c2); c <= Math.Max(c1, c2); c++)
                blockedTiles.TryAdd((r1, c), ROCK);
        else // Loop through rows
            for (var r = Math.Min(r1, r2); r <= Math.Max(r1, r2); r++)
                blockedTiles.TryAdd((r, c1), ROCK);
    }
}