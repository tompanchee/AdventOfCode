using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day01;

[Day(2024, 1, "Historian Hysteria")]
public class Solver : SolverBase
{
    private readonly List<int> left = new();
    private readonly List<int> right = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        foreach (string line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            left.Add(int.Parse(parts[0]));
            right.Add(int.Parse(parts[1]));
        }
    }

    public override Task Solve1()
    {
        logger.Information("Calculating distance difference...");

        List<int> orderedLeft = left.OrderBy(x => x).ToList();
        List<int> orderedRight = right.OrderBy(x => x).ToList();

        int total = orderedLeft.Select((t, i) => Math.Abs(t - orderedRight[i])).Sum();

        logger.Information($"Total distance: {total}");

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Calculating similarity score...");

        long score = left.Aggregate(0L, (current, i) => current + (i * right.Count(x => x == i)));

        logger.Information($"Total similarity score: {score}");

        return Task.CompletedTask;
    }
}