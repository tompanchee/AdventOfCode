using Common;
using Common.Solver;
using Serilog;

namespace Day06;

[Day(2023, 6, "Wait For It")]
internal class Solver : SolverBase
{
    private List<(int time, int distance)> records = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading race records...");

        var times = inputLines[0][10..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        var distances = inputLines[1][10..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        for (int i = 0; i < times.Count; i++)
        {
            records.Add((times[i], distances[i]));
        }
    }

    public override Task Solve1()
    {
        logger.Information("Optimizing races...");

        var product = 1;
        foreach ((int time, int distance) in records)
        {
            var winningCount = 0;
            for (int wait = 1; wait <= time; wait++)
            {
                var result = wait * (time - wait);

                if (result > distance) winningCount++;
                else if (winningCount > 0) break;
            }

            product *= winningCount;
        }

        logger.Information("Multiplying winners give {product}", product);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Running longer race...");

        var time = long.Parse(string.Join("", records.Select(r => r.time)));
        var distance = long.Parse(string.Join("", records.Select(r => r.distance)));

        long lowLimit = 0;
        long highLimit = 0;

        // Binary search for limits
        var low = 0L;
        var high = time;
        while (low < high)
        {
            lowLimit = low + (high - low) / 2;

            var result = lowLimit * (time - lowLimit);
            if (result > distance)
            {
                high -= 1;
            }
            else
            {
                low += 1;
            }
        }

        low = 0L;
        high = time;
        while (low < high)
        {
            highLimit = low + (high - low) / 2;

            var result = highLimit * (time - highLimit);
            if (result < distance)
            {
                high -= 1;
            }
            else
            {
                low += 1;
            }
        }

        logger.Information("There are {number} of times when the race can be won", highLimit - lowLimit);

        return Task.CompletedTask;
    }
}