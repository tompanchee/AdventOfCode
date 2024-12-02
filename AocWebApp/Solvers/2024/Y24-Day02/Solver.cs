using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day02;

[Day(2024, 2, "Red-Nosed Reports")]
public class Solver : SolverBase
{
    private List<Report> reports = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading reports...");
        foreach (var line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            reports.Add(Report.Parse(line));
        }
    }

    public override Task Solve1()
    {
        logger.Information("Counting safe reports...");

        var safeReports = reports.Count(r => r.IsSafe());

        logger.Information("Number of safe reports {count}", safeReports);
        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Counting safe reports with dampener...");

        var safeReports = reports.Count(r => r.IsSafeWithDampener());

        logger.Information("Number of safe reports {count}", safeReports);
        return Task.CompletedTask;
    }
}

internal class Report
{
    private Report(List<int> values)
    {
        Values = values;
    }

    public static Report Parse(string input)
    {
        var values = input.Split(' ').Select(int.Parse).ToList();
        return new Report(values);
    }

    public bool IsSafe() => GetSafeStatus(Values);

    public bool IsSafeWithDampener()
    {
        if (IsSafe()) return true;

        for (var i = 0; i < Values.Count; i++)
        {
            var newValues = new List<int>(Values);
            newValues.RemoveAt(i);
            if (GetSafeStatus(newValues))
            {
                return true;
            }
        }

        return false;
    }

    private bool GetSafeStatus(IReadOnlyList<int> values)
    {
        var previous = 0;
        for (var i = 0; i < values.Count - 1; i++)
        {
            var diff = values[i + 1] - values[i];

            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                return false;
            }

            if (i > 0)
            {
                if (Math.Sign(diff) != Math.Sign(previous))
                {
                    return false;
                }
            }

            previous = diff;
        }

        return true;
    }

    private List<int> Values { get; }
}