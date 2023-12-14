using System.Text;
using Common;
using Common.Solver;
using Serilog;
using Serilog.Events;

namespace Day10;

[Day(2023, 10, "Pipe Maze")]
internal class Solver : SolverBase
{
    private readonly string[] map;
    private readonly List<(int x, int y)> path = new();
    private readonly char startPositionReplacement;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading pipe maze...");

        // Add empty entries (first, last) for index sanity
        map = inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => "." + l + ".").ToArray();

        // Walk the path
        logger.Information("Locating the Animal...");

        var animalX = -1;
        var animalY = -1;

        for (int y = 0; y < map.Length; y++)
        {
            animalX = map[y].IndexOf('S');
            if (animalX <= -1) continue;

            animalY = y;
            break;
        }

        logger.Information("Finding out initial directions away from the Animal...");
        var d = "";

        while (d.Length < 2)
        {
            // Can go north?
            var c = map[animalY - 1][animalX];
            if (c is '|' or 'F' or '7') d += "N";

            // Can go south?
            c = map[animalY + 1][animalX];
            if (c is '|' or 'L' or 'J') d += "S";

            // Can go west?
            c = map[animalY][animalX - 1];
            if (c is '-' or 'L' or 'F') d += "W";

            // Can go east?
            c = map[animalY][animalX + 1];
            if (c is '-' or 'J' or '7') d += "E";
        }

        startPositionReplacement = ReplaceStartingPositionWithPipe(d);

        logger.Information("Walking the path...");
        var walker = new Walker(map, (animalX, animalY));

        // First step
        path.Add((animalX, animalY));
        walker.WalkStep(d[0]);

        while (walker.Position.x != animalX || walker.Position.y != animalY)
        {
            path.Add((walker.Position.x, walker.Position.y));
            walker.WalkStep();
        }
    }

    public override Task Solve1()
    {
        DebugGrid(map);

        logger.Information("Finding path furthest away from the Animal..");
        logger.Information("Furthest point of the Animal is {steps} steps away", path.Count / 2);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Finding possible nest size...");

        // Create an enlarged pipeline with only the loop, to open up the outwards cavities
        var enlarged = new List<string>();
        var sb = new StringBuilder();
        for (var y = 0; y < map.Length; y++)
        {
            sb.Clear();
            for (var x = 0; x < map[y].Length; x++)
            {
                char c = '.';
                if (path.Contains((x, y))) c = map[y][x];
                if (c == 'S') c = startPositionReplacement;

                sb.Append(c).Append('.');
            }

            enlarged.Add(sb.ToString());
            enlarged.Add(new string(Enumerable.Repeat('.', map[y].Length * 2).ToArray()));
        }

        DebugGrid(enlarged);

        // Go through the enlarged map to fill out broken pipes

        // Build missing EW connection
        for (var y = 0; y < enlarged.Count; y++)
        {
            for (var x = 0; x <= enlarged[y].Length; x++)
            {
                var left = (x - 1, y);
                if (!IsValid(left.Item1, left.y)) continue;
                var right = (x + 1, y);
                if (!IsValid(right.Item1, right.y)) continue;

                var lp = enlarged[left.y][left.Item1];
                var rp = enlarged[right.y][right.Item1];
                if (lp == '.' || rp == '.') continue;
                if ((lp is '-' or 'L' or 'F') && (rp is '-' or '7' or 'J'))
                {
                    var old = enlarged[y];
                    enlarged[y] = old[..x] + "-" + old[(x + 1)..];
                }
            }
        }

        // Build missing NS connection
        for (var y = 0; y < enlarged.Count; y++)
        {
            for (var x = 0; x <= enlarged[y].Length; x++)
            {
                var up = (x, y - 1);
                if (!IsValid(up.x, up.Item2)) continue;
                var down = (x, y + 1);
                if (!IsValid(down.x, down.Item2)) continue;

                var upipe = enlarged[up.Item2][up.x];
                var dpipe = enlarged[down.Item2][down.x];
                if (upipe == '.' || dpipe == '.') continue;
                if ((upipe is '|' or '7' or 'F') && (dpipe is '|' or 'J' or 'L'))
                {
                    var old = enlarged[y];
                    enlarged[y] = old[..x] + "|" + old[(x + 1)..];
                }
            }
        }

        DebugGrid(enlarged);

        // Fill outer parts
        var stack = new Stack<(int x, int y)>();
        stack.Push((0, 0));

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            var old = enlarged[current.y];
            enlarged[current.y] = old[..current.x] + "+" + old[(current.x + 1)..];

            foreach ((int nx, int ny) in GetNeighbours(current))
            {
                if (enlarged[ny][nx] == '.')
                {
                    stack.Push((nx, ny));
                }
            }
        }

        DebugGrid(enlarged);

        // Calculate inner area (sum of '.') with every second row and column ("shrink" enlarged map)
        var area = 0;
        for (var y = 0; y < enlarged.Count; y += 2)
        {
            for (var x = 0; x < enlarged[y].Length; x += 2)
            {
                if (enlarged[y][x] == '.') area++;
            }
        }

        logger.Information("Total area inside the path is {area}", area);

        return Task.CompletedTask;

        IEnumerable<(int x, int y)> GetNeighbours((int x, int y) current)
        {
            var neighbours = new List<(int x, int y)>();
            foreach ((int dx, int dy) in Constants.Offsets.Values)
            {
                var newx = current.x + dx;
                var newy = current.y + dy;

                if (!IsValid(newx, newy)) continue;

                neighbours.Add((newx, newy));
            }

            return neighbours.ToArray();
        }

        bool IsValid(int x, int y)
        {
            if (x < 0 || x >= enlarged![0].Length) return false;
            if (y < 0 || y >= enlarged.Count) return false;

            return true;
        }
    }

    private static char ReplaceStartingPositionWithPipe(string s)
    {
        return s switch
        {
            "NS" or "SN" => '|',
            "EW" or "WE" => '-',
            "SE" or "ES" => 'F',
            "SW" or "WS" => '7',
            "NE" or "EN" => 'L',
            "NW" or "WN" => 'J',
            _ => throw new InvalidOperationException()
        };
    }

    private void DebugGrid(IEnumerable<string> grid)
    {
        if (!logger.IsEnabled(LogEventLevel.Debug)) return;

        var sb = new StringBuilder();
        foreach (string s in grid)
        {
            sb.AppendLine(s);
        }

        logger.Debug("Current grid {nl}{data}", Environment.NewLine, sb.ToString());
    }
}