using AOCUtils.Geometry._2D;
using Serilog.Core;

namespace Day24;

internal class Walker
{
    readonly HashSet<Point>[] forbiddenPoints;
    readonly Logger logger;
    readonly bool reverse;
    readonly Point start;
    readonly HashSet<(int time, Point position)> visitedStates = new();

    public Walker(HashSet<Point>[] forbiddenPoints, Point start, Logger logger, bool reverse = false) {
        this.forbiddenPoints = forbiddenPoints;
        this.start = start;
        this.logger = logger;
        this.reverse = reverse;
    }

    public int MinTime { get; private set; } = int.MaxValue;
    public List<Point>? ShortestPath { get; private set; }

    public void Walk(Point current, Point target, int time, List<Point> path) {
        if (time > MinTime) return;
        path.Add(current);
        visitedStates.Add((time % Constants.LEAST_COMMON_MULTIPLE, current));

        var moves = GetNextPositions(current);
        foreach (var point in moves) {
            if (point.Equals(target)) {
                if (time + 1 < MinTime) {
                    MinTime = time + 1;
                    ShortestPath = path;
                    logger.Information("New minimum time found {time}", MinTime);
                }

                break;
            }

            Walk(point, target, time + 1, new List<Point>(path));
        }

        List<Point> GetNextPositions(Point p) {
            var points = new List<Point>();
            foreach (var (dx, dy) in reverse ? Constants.OffsetsReverse : Constants.Offsets) {
                var newPos = new Point(p.X + dx, p.Y + dy);
                if (newPos.Equals(target)) return new List<Point> {target};
                if (!newPos.Equals(start)) {
                    if (newPos.X is <= Constants.EDGE_X_MIN or >= Constants.EDGE_X_MAX) continue;
                    if (newPos.Y is <= Constants.EDGE_Y_MIN or >= Constants.EDGE_Y_MAX) continue;
                }

                if (forbiddenPoints[(time + 1) % Constants.LEAST_COMMON_MULTIPLE].Contains(newPos)) continue;
                if (visitedStates.Contains(((time + 1) % Constants.LEAST_COMMON_MULTIPLE, newPos))) continue;

                points.Add(newPos);
            }

            return points.ToList();
        }
    }
}