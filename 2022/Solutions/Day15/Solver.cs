using AOCCommon;
using AOCUtils.Geometry._2D;
using Serilog.Core;

namespace Day15;

[Day(15, "Beacon Exclusion Zone")]
internal class Solver : SolverBase
{
    const long PART1_Y = 2000000;

    HashSet<Point> beacons = new();
    List<ManhattanCircle> sensors = new();

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Checking row y={y}...", PART1_Y);

        var offset = 1;
        var x = (long) sensors.Select(s => s.Centre.X).Average();

        var count = sensors.Any(s => s.IsInCircle(new Point(x, PART1_Y))) ? 1 : 0;

        while (true) {
            var p = new Point(x - offset, PART1_Y);
            var isLeftIn = !beacons.Contains(p) && sensors.Any(s => s.IsInCircle(p));
            p = new Point(x + offset, PART1_Y);
            var isRightIn = !beacons.Contains(p) && sensors.Any(s => s.IsInCircle(p));

            if (isLeftIn) count++;
            if (isRightIn) count++;

            offset++;

            if (!isLeftIn && !isRightIn) break;
        }

        logger.Information("There are {count} positions where the beacon can't be", count);
    }

    public override void Solve2() {
        logger.Information("Scanning area...");
        const long MIN = 0;
        const long MAX = 4000000;

        HashSet<Point> candidates = new();

        foreach (var circumference in sensors
                     .Select(sensor => new ManhattanCircle(sensor.Centre, sensor.Radius + 1))
                     .Select(outerCircle => outerCircle.CircumferencePoints()
                         .Where(p => p.X is >= MIN and <= MAX && p.Y is >= MIN and <= MAX))) candidates.UnionWith(circumference);

        var beacon = candidates.FirstOrDefault(candidate => !sensors.Any(s => s.IsInCircle(candidate)));

        logger.Information("Tuning frequency is {f}", 4000000 * beacon!.X + beacon.Y);
    }

    protected override void PostConstruct() {
        logger.Information("Mapping beacons...");

        // Sensor at x=2557568, y=3759110: closest beacon is at x=2594124, y=3746832
        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;

            var at = row.IndexOf("at", StringComparison.InvariantCulture);
            var colon = row.IndexOf(":", StringComparison.InvariantCulture);
            var sensorPos = ParsePoint(row[at..colon]);

            at = row.LastIndexOf("at", StringComparison.InvariantCulture);
            var beaconPos = ParsePoint(row[at..]);

            sensors.Add(new ManhattanCircle(sensorPos, beaconPos.ManhattanDistanceTo(sensorPos)));
            beacons.Add(beaconPos);
        }

        static Point ParsePoint(string value) {
            var split = value.Split(',', StringSplitOptions.TrimEntries);

            var x = long.Parse(split[0][5..]);
            var y = long.Parse(split[1][2..]);

            return new Point(x, y);
        }
    }
}