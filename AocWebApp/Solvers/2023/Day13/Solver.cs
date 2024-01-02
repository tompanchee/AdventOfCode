using Common;
using Common.Solver;
using Serilog;

namespace Day13;

[Day(2023, 13, "Point of Incidence")]
internal class Solver : SolverBase
{
    private readonly List<Pattern> patterns = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Scanning mirrors...");

        var data = new List<string>();
        foreach (string line in inputLines)
        {
            if (string.IsNullOrEmpty(line))
            {
                if (data.Count > 0) patterns.Add(new Pattern(data));
                data.Clear();
            }
            else
            {
                data.Add(line);
            }
        }

        if (data.Count > 0) patterns.Add(new Pattern(data));
    }

    public override Task Solve1()
    {
        logger.Information("Positioning mirrors...");

        foreach (var pattern in patterns)
        {
            (List<int> rows, List<int> cols) = pattern.GetLinesOfReflection();

            if (rows.Any()) pattern.RowOfReflection = rows[0];
            if (cols.Any()) pattern.ColumnOfReflection = cols[0];
        }

        var sum = patterns.Where(p => p.RowOfReflection != null).Sum(p => p.RowOfReflection) * 100
                  + patterns.Where(p => p.ColumnOfReflection != null).Sum(p => p.ColumnOfReflection);
        logger.Information("Total mirror sum is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Cleaning up smudges...");

        foreach (var pattern in patterns)
        {
            var found = false;
            for (var r = 0; r < pattern.Rows.Count; r++)
            {
                for (var c = 0; c < pattern.Rows[r].Length; c++)
                {
                    pattern.Swap(r, c);
                    (List<int> rows, List<int> cols) = pattern.GetLinesOfReflection();

                    if (rows.Any())
                    {
                        if (rows.Any(row => row != pattern.RowOfReflection))
                        {
                            pattern.RowOfReflection = rows.First(row => row != pattern.RowOfReflection);
                            pattern.ColumnOfReflection = null;
                            found = true;
                            break;
                        }
                    }

                    if (cols.Any())
                    {
                        if (cols.Any(col => col != pattern.ColumnOfReflection))
                        {
                            pattern.ColumnOfReflection = cols.First(col => col != pattern.ColumnOfReflection);
                            pattern.RowOfReflection = null;
                            found = true;
                            break;
                        }
                    }

                    pattern.Swap(r, c); // Reset 
                }

                if (found) break;
            }
        }

        var sum = patterns.Where(p => p.RowOfReflection != null).Sum(p => p.RowOfReflection) * 100
                  + patterns.Where(p => p.ColumnOfReflection != null).Sum(p => p.ColumnOfReflection);
        logger.Information("Total mirror sum is {sum}", sum);

        return Task.CompletedTask;
    }
}