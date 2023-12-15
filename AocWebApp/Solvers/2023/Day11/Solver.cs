using Common;
using Common.Solver;
using Serilog;

namespace Day11;

[Day(2023, 11, "Cosmic Expansion")]
internal class Solver : SolverBase
{
    private readonly List<(int x, int y)> galaxies = new();
    private readonly List<int> emptyRows = new();
    private readonly List<int> emptyColumns = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Scanning image...");

        var trimmed = inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        for (var y = 0; y < trimmed.Count; y++)
        {
            if (trimmed[y].IndexOf('#') < 0) emptyRows.Add(y);
            for (var x = 0; x < trimmed[y].Length; x++)
            {
                if (trimmed[y][x] == '#') galaxies.Add((x, y));
            }
        }

        for (var x = 0; x < trimmed[0].Length; x++)
        {
            bool emptyCol = true;
            for (var y = 0; y < trimmed.Count; y++)
            {
                if (trimmed[y][x] == '#')
                {
                    emptyCol = false;
                    break;
                }
            }

            if (emptyCol) emptyColumns.Add(x);
        }
    }

    public override Task Solve1()
    {
        logger.Information("Calculating galaxy distances in expanded universe...");

        var sum = 0;

        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                var d = galaxies[i].ManhattanDistanceTo(galaxies[j]);

                int x1 = galaxies[i].x < galaxies[j].x ? galaxies[i].x : galaxies[j].x;
                int x2 = galaxies[i].x > galaxies[j].x ? galaxies[i].x : galaxies[j].x;
                int y1 = galaxies[i].y < galaxies[j].y ? galaxies[i].y : galaxies[j].y;
                int y2 = galaxies[i].y > galaxies[j].y ? galaxies[i].y : galaxies[j].y;
                ;

                d += emptyRows.Count(r => r > y1 && r < y2);
                d += emptyColumns.Count(r => r > x1 && r < x2);

                sum += d;
            }
        }

        logger.Information("Sum of galaxy distances is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Calculating galaxy distances in very old expanded universe...");

        var sum = 0L;

        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                var d = galaxies[i].ManhattanDistanceTo(galaxies[j]);

                int x1 = galaxies[i].x < galaxies[j].x ? galaxies[i].x : galaxies[j].x;
                int x2 = galaxies[i].x > galaxies[j].x ? galaxies[i].x : galaxies[j].x;
                int y1 = galaxies[i].y < galaxies[j].y ? galaxies[i].y : galaxies[j].y;
                int y2 = galaxies[i].y > galaxies[j].y ? galaxies[i].y : galaxies[j].y;

                d += emptyRows.Count(r => r > y1 && r < y2) * 999999;
                d += emptyColumns.Count(r => r > x1 && r < x2) * 999999;

                sum += d;
            }
        }

        logger.Information("Sum of galaxy distances is {sum}", sum);

        return Task.CompletedTask;
    }
}

internal static class Extensions
{
    public static int ManhattanDistanceTo(this (int x, int y) p1, (int x, int y) p2)
    {
        return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
    }
}