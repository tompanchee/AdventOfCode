using System.Text;
using AOCCommon;
using Serilog.Core;

namespace Day23;

[Day(23, "Unstable Diffusion")]
internal class Solver : SolverBase
{
    static readonly IDictionary<string, (int dx, int dy)> offsets = new Dictionary<string, (int dx, int dy)> {
        {"NE", (1, -1)},
        {"N", (0, -1)},
        {"NW", (-1, -1)},
        {"W", (-1, 0)},
        {"E", (1, 0)},
        {"SE", (1, 1)},
        {"S", (0, 1)},
        {"SW", (-1, 1)}
    };

    static IList<string[]> moveDirections = new List<string[]> {
        new[] {"N", "NE", "NW"},
        new[] {"S", "SE", "SW"},
        new[] {"W", "NW", "SW"},
        new[] {"E", "NE", "SE"}
    };

    IDictionary<int, (int x, int y)> current = new Dictionary<int, (int x, int y)>();
    int currentGeneration;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Elves moving 10 rounds...");
        for (var i = 0; i < 10; i++) _ = NextGeneration();

        var area = (current.Values.Max(v => v.x) - current.Values.Min(v => v.x) + 1)
                   * (current.Values.Max(v => v.y) - current.Values.Min(v => v.y) + 1);

        var emptyTiles = area - current.Count;

        logger.Information("There are {count} empty tiles after 10 rounds", emptyTiles);
    }

    public override void Solve2() {
        logger.Information("Keep on moving until no elves move...");

        int elvesMoved;
        do {
            elvesMoved = NextGeneration();
        } while (elvesMoved != 0);

        logger.Information("After {count} moves there are no moves", currentGeneration);
    }

    int NextGeneration() {
        var elvesMoved = 0;

        // Get new generation candidates
        var candidates = current.ToDictionary(elf => elf.Key, elf => GetNextPossiblePosition(elf.Value.x, elf.Value.y));

        var nextGeneration = new Dictionary<int, (int x, int y)>();
        foreach (var candidate in candidates) {
            var candidatePos = candidate.Value;
            var currentPos = current[candidate.Key];
            if (candidates.Count(c => c.Value.x == candidate.Value.x && c.Value.y == candidate.Value.y) == 1
                && (candidatePos.x != currentPos.x || candidatePos.y != currentPos.y)) {
                nextGeneration.Add(candidate.Key, candidate.Value);
                elvesMoved++;
            } else {
                nextGeneration.Add(candidate.Key, current[candidate.Key]);
            }
        }

        currentGeneration++;
        current = nextGeneration;

        return elvesMoved;
    }

    (int x, int y) GetNextPossiblePosition(int x, int y) {
        if (!HasNeighbours(x, y)) return (x, y);

        for (var i = 0; i < 4; i++) {
            var direction = moveDirections[(currentGeneration + i) % 4];
            if (!HasNeighbours(x, y, direction)) {
                var (dx, dy) = offsets[direction[0]];
                return (x + dx, y + dy);
            }
        }

        return (x, y);
    }

    bool HasNeighbours(int x, int y, string[]? directions = null) {
        directions ??= offsets.Keys.ToArray();

        foreach (var direction in directions) {
            var (dx, dy) = offsets[direction];
            if (current.Values.Contains((x + dx, y + dy))) return true;
        }

        return false;
    }

    protected override void PostConstruct() {
        logger.Information("Checking elf positions...");

        //data = new[] {
        //    "..............",
        //    "..............",
        //    ".......#......",
        //    ".....###.#....",
        //    "...#...#.#....",
        //    "....#...##....",
        //    "...#.###......",
        //    "...##.#.##....",
        //    "....#..#......",
        //    "..............",
        //    "..............",
        //    ".............."
        //};

        var y = 0;
        var id = 0;
        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;
            for (var x = 0; x < row.Length; x++)
                if (row[x] == '#')
                    current.Add(id++, (x, y));

            y++;
        }
    }

    // For debugging purposes
    void OutputCurrent() {
        var output = new StringBuilder().AppendLine();

        var miny = current.Values.Min(v => v.y);
        var maxy = current.Values.Max(v => v.y);
        var minx = current.Values.Min(v => v.x);
        var maxx = current.Values.Max(v => v.x);

        for (var y = miny; y <= maxy; y++) {
            for (var x = minx; x <= maxx; x++) output.Append(current.Values.Contains((x, y)) ? "#" : ".");

            output.AppendLine();
        }

        logger.Information("After {round} rounds", currentGeneration);
        logger.Information(output.ToString());
    }
}