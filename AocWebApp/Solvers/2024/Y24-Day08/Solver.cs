using Common;
using Common.Solver;
using Common.Utils.Grid2D;
using Serilog;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
namespace Y24_Day08;

[Day(2024, 8, "Resonant Collinearity")]
public class Solver : SolverBase
{
    private readonly Grid map;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        map = new Grid(inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray());
    }

    public override Task Solve1()
    {
        logger.Information("Searching for antinodes...");

        HashSet<Point> antinodes = [];

        foreach ((Point point, char value) in map)
        {
            if (value == '.')
            {
                continue;
            }

            foreach ((Point point2, char value2) in map)
            {
                if (point == point2 || value2 != value)
                {
                    continue;
                }

                var offset = point.OffsetTo(point2);
                var antinode = point.Add(offset);
                if (map.Contains(antinode))
                {
                    antinodes.Add(antinode);
                }

                antinode = point2.Add(-offset);
                if (map.Contains(antinode))
                {
                    antinodes.Add(antinode);
                }
            }
        }

        logger.Information("Found {count} unique antinode locations", antinodes.Count);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Searching for antinodes...");

        HashSet<Point> antinodes = [];

        foreach ((Point point, char value) in map)
        {
            if (value == '.')
            {
                continue;
            }

            foreach ((Point point2, char value2) in map)
            {
                if (point == point2 || value2 != value)
                {
                    continue;
                }

                var multiplier = 0;
                while (true)
                {
                    bool valid1 = false;
                    bool valid2 = false;
                    var offset = ++multiplier * point.OffsetTo(point2);

                    var antinode = point.Add(offset);
                    if (map.Contains(antinode))
                    {
                        antinodes.Add(antinode);
                        valid1 = true;
                    }

                    antinode = point.Add(-offset);
                    if (map.Contains(antinode))
                    {
                        antinodes.Add(antinode);
                        valid2 = true;
                    }

                    if (!valid1 && !valid2)
                    {
                        break;
                    }
                }
            }
        }

        logger.Information("Found {count} unique antinode locations", antinodes.Count);

        return Task.CompletedTask;
    }
}