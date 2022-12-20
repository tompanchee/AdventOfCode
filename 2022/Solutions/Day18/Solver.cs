using AOCCommon;
using AOCUtils.Geometry._3D;
using Serilog.Core;

namespace Day18;

[Day(18, "Boiling Boulders")]
internal class Solver : SolverBase
{
    readonly HashSet<Point> lavaBits = new();

    int surfaceArea;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        surfaceArea = lavaBits.Sum(lavaBit => 6 - CalculateNeighbours(lavaBit, lavaBits));

        logger.Information("Total surface area of the lava droplet is {0}", surfaceArea);
    }

    public override void Solve2() {
        var minx = lavaBits.Select(l => l.X).Min();
        var maxx = lavaBits.Select(l => l.X).Max();
        var miny = lavaBits.Select(l => l.Y).Min();
        var maxy = lavaBits.Select(l => l.Y).Max();
        var minz = lavaBits.Select(l => l.Z).Min();
        var maxz = lavaBits.Select(l => l.Z).Max();

        var cave = new Substance[maxx + 1, maxy + 1, maxz + 1];

        // Fill with input lava
        foreach (var lavaBit in lavaBits) cave[lavaBit.X, lavaBit.Y, lavaBit.Z] = Substance.Lava;

        // BFS to fill with water from outside
        var start = new Point(minx, miny, minz);
        var queue = new Queue<Point>();
        queue.Enqueue(start);

        while (queue.Count > 0) {
            var current = queue.Dequeue();
            var neighbours = GetNeighbours(current);
            foreach (var neighbor in neighbours) {
                // Bounds check
                if (neighbor.X < 0 || neighbor.X > maxx
                                   || neighbor.Y < 0 || neighbor.Y > maxy
                                   || neighbor.Z < 0 || neighbor.Z > maxz) continue;

                // Queue only not filled neighbours and do not queue multiple times
                if (cave[neighbor.X, neighbor.Y, neighbor.Z] == Substance.Air && !queue.Contains(neighbor)) queue.Enqueue(neighbor);
            }

            cave[current.X, current.Y, current.Z] = Substance.Water;
        }

        // Get remaining air
        var air = new HashSet<Point>();

        for (var x = 0; x < cave.GetLength(0); x++)
        for (var y = 0; y < cave.GetLength(1); y++)
        for (var z = 0; z < cave.GetLength(2); z++)
            if (cave[x, y, z] == Substance.Air)
                air.Add(new Point(x, y, z));

        var airArea = air.Sum(a => 6 - CalculateNeighbours(a, air));

        logger.Information("Exterior surface area is {0}", surfaceArea - airArea);
    }

    int CalculateNeighbours(Point lavaBit, IReadOnlySet<Point> setToCheck) {
        var neighbours = GetNeighbours(lavaBit);

        return neighbours.Count(setToCheck.Contains);
    }

    static IEnumerable<Point> GetNeighbours(Point lavaBit) {
        List<(int dx, int dy, int dz)> offsets = new() {
            (-1, 0, 0),
            (1, 0, 0),
            (0, -1, 0),
            (0, 1, 0),
            (0, 0, -1),
            (0, 0, 1)
        };

        return offsets.Select(lavaBit.CreateNewWithOffset);
    }

    protected override void PostConstruct() {
        logger.Information("Scanning lava...");

        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;

            var split = row.Split(',', StringSplitOptions.TrimEntries);
            var lavaBit = new Point(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
            lavaBits.Add(lavaBit);
        }
    }

    enum Substance
    {
        Air,
        Lava,
        Water
    }
}