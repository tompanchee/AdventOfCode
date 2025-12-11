using Common.Solver;
using Common;
using Serilog;

namespace Y25_Day05;

[Day(2025, 5, "Cafeteria")]
public class Solver : SolverBase
{
    private readonly List<(long min, long max)> ranges = [];
    private readonly List<long> ingredients = [];
    
    public Solver(string input, ILogger logger) : base(input, logger)
    {
        foreach (var line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            if (line.Contains('-'))
            {
                var split = line.Split('-');
                var min = long.Parse(split[0]);
                var max = long.Parse(split[1]);
                ranges.Add((min, max));
            }
            else
            {
                var id = long.Parse(line);
                ingredients.Add(id);
            }
        }
    }

    public override Task Solve1()
    {
        logger.Information("Solving part 1...");
        var count = ingredients.Count(ingredient => ranges.Any(range => Contains(range, ingredient)));
        logger.Information("There are {count} fresh ingredients", count);
        return Task.CompletedTask;
        
        static bool Contains((long min, long max) range, long id) => id >= range.min && id <= range.max;
    }

    public override Task Solve2()
    {
        logger.Information("Solving part 2...");

        var ordered = ranges.OrderBy(r => r.min).ToArray();

        for (var i = 0; i < ordered.Length - 1; i++)
        {
            var r1 = ordered[i];
            var r2 = ordered[i + 1];
            if (r1.max >= r2.min)
            {
                var end = Math.Max(r1.max, r2.max);
                ordered[i] = (r1.min, r2.min - 1);
                ordered[i + 1] = (r2.min, end);
            }
        }

        var count = ordered.Sum(r => r.max - r.min + 1);
        
        logger.Information("There are {count} fresh ingredients", count);
        return Task.CompletedTask;
    }
}
