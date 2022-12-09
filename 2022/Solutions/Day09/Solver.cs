using AOCCommon;
using Serilog.Core;

namespace Day09;

[Day(9, "Rope Bridge")]
internal class Solver : SolverBase
{
    static IDictionary<string, (int dx, int dy)> directions = new Dictionary<string, (int dx, int dy)> {
        {"U", (0, -1)},
        {"D", (0, 1)},
        {"L", (-1, 0)},
        {"R", (1, 0)}
    };

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        var visitedTailPositions = new HashSet<(int, int)>();

        var head = new Position();
        var tail = new Position();

        visitedTailPositions.Add((tail.x, tail.y));

        foreach (var row in data) {
            var (direction, amount) = ParseInstruction(row);
            Move(direction, amount);
        }

        logger.Information("Tail has visited {0} positions", visitedTailPositions.Count);

        void Move(string direction, int amount) {
            var (dx, dy) = directions[direction];

            for (var i = 0; i < amount; i++) {
                head.Move(dx, dy);
                if (tail.IsAdjacent(head)) continue;

                var diffX = Math.Sign(head.x - tail.x);
                var diffY = Math.Sign(head.y - tail.y);

                tail.Move(diffX, diffY);

                visitedTailPositions.Add((tail.x, tail.y));
            }
        }
    }

    public override void Solve2() {
        var visitedTailPositions = new HashSet<(int, int)>();
        var knots = new[] {
            new Position(),
            new Position(),
            new Position(),
            new Position(),
            new Position(),
            new Position(),
            new Position(),
            new Position(),
            new Position(),
            new Position()
        };

        visitedTailPositions.Add((knots[9].x, knots[9].y));

        foreach (var row in data) {
            var (direction, amount) = ParseInstruction(row);
            Move(direction, amount);
        }

        logger.Information("Tail has visited {0} positions", visitedTailPositions.Count);

        void Move(string direction, int amount) {
            var (dx, dy) = directions[direction];

            for (var i = 0; i < amount; i++) {
                knots[0].Move(dx, dy);
                for (var j = 1; j < knots.Length; j++) {
                    if (knots[j - 1].IsAdjacent(knots[j])) continue;

                    var diffX = Math.Sign(knots[j - 1].x - knots[j].x);
                    var diffY = Math.Sign(knots[j - 1].y - knots[j].y);

                    knots[j].Move(diffX, diffY);

                    visitedTailPositions.Add((knots[9].x, knots[9].y));
                }
            }
        }
    }

    protected override void PostConstruct() {
        logger.Information("Moving rope around...");

        //data = new[] {
        //    "R 5",
        //    "U 8",
        //    "L 8",
        //    "D 3",
        //    "R 17",
        //    "D 10",
        //    "L 25",
        //    "U 20"
        //};
    }

    (string direction, int amount) ParseInstruction(string instruction) {
        var split = instruction.Split(' ');
        return (split[0], int.Parse(split[1]));
    }
}

internal class Position
{
    public int x;
    public int y;

    public void Move(int dx, int dy) {
        x += dx;
        y += dy;
    }

    public bool IsAdjacent(Position other) {
        var dx = Math.Abs(x - other.x);
        var dy = Math.Abs(y - other.y);

        if (dx == 0) return dy < 2;
        if (dy == 0) return dx < 2;

        return dx < 2 && dy < 2;
    }
}