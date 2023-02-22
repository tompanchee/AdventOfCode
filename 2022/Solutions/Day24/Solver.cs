using AOCCommon;
using AOCUtils.Geometry._2D;
using Serilog.Core;

namespace Day24;

[Day(24, "Blizzard Basin")]
internal class Solver : SolverBase
{
    Point? end;
    HashSet<Point>[] forbiddenPoints = Array.Empty<HashSet<Point>>();
    Point? start;
    int t1; // Time through the valley 1st time

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Walking through the blizzards...");

        var walker = new Walker(forbiddenPoints, start!, logger);
        walker.Walk(start!, end!, 0, new List<Point>());

        logger.Information("The goal can be reached in {time} minutes", walker.MinTime);
        t1 = walker.MinTime;
    }

    public override void Solve2() {
        logger.Information("Going back for snacks...");

        var walker = new Walker(forbiddenPoints, end!, logger, true);
        walker.Walk(end!, start!, t1, new List<Point>());
        var t2 = walker.MinTime;

        logger.Information("Going back to the other side...");
        walker = new Walker(forbiddenPoints, start!, logger);
        walker.Walk(start!, end!, t2, new List<Point>());

        logger.Information("Snacks retrieved in {time} min", walker.MinTime);
    }

    protected override void PostConstruct() {
        logger.Information("Mapping blizzards...");

        var initialBlizzards = new List<Blizzard>();

        var y = 0;
        foreach (var row in data) {
            if (y == Constants.EDGE_Y_MAX) break;
            if (y == Constants.EDGE_Y_MIN) {
                y++;
                continue;
            }

            for (var x = Constants.EDGE_X_MIN + 1; x < Constants.EDGE_X_MAX; x++)
                if (row[x] != '.')
                    initialBlizzards.Add(new Blizzard(new Point(x, y), row[x]));

            y++;
        }

        logger.Information("Moving blizzards...");
        var fp = new List<HashSet<Point>> {new(initialBlizzards.Select(b => b.Position))};
        var current = initialBlizzards;
        for (var t = 1; t < Constants.LEAST_COMMON_MULTIPLE; t++) {
            var newBlizzards = current.Select(blizzard => blizzard.Move()).ToList();
            fp.Add(new HashSet<Point>(newBlizzards.Select(b => b.Position)));
            current = newBlizzards;
        }

        forbiddenPoints = fp.ToArray();

        start = new Point(data[0].IndexOf('.'), 0);
        end = new Point(data[Constants.EDGE_Y_MAX].IndexOf('.'), Constants.EDGE_Y_MAX);
    }
}