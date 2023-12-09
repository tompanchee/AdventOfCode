using Common;
using Common.Solver;
using Serilog;

namespace Day09;

[Day(2023, 9, "Mirage Maintenance")]
internal class Solver : SolverBase
{
    private readonly List<(int previous, int next)> extrapolatedValues;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading Oasis And Sand Instability Sensor history...");

        List<List<int>> values = inputLines.Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(line => line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList())
            .ToList();

        logger.Information("Extrapolating values...");
        extrapolatedValues = values.Select(Extrapolate).ToList();
    }

    public override Task Solve1()
    {
        logger.Information("Checking future values...");

        var sum = extrapolatedValues.Select(v => v.next).Sum();
        logger.Information("Sum of extrapolated future values is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Checking historical values...");

        var sum = extrapolatedValues.Select(v => v.previous).Sum();
        logger.Information("Sum of extrapolated historical values is {sum}", sum);

        return Task.CompletedTask;
    }

    private static (int previous, int next) Extrapolate(List<int> data)
    {
        List<List<int>> workArray = new() { data };

        while (workArray.Last().Any(i => i != 0))
        {
            var last = workArray.Last();
            var newRow = new List<int>();

            for (int i = 0; i < last.Count - 1; i++)
            {
                newRow.Add(last[i + 1] - last[i]);
            }

            workArray.Add(newRow);
        }

        workArray.Last().Insert(0, 0);

        for (int i = workArray.Count - 2; i >= 0; i--)
        {
            var current = workArray[i];
            current.Add(workArray[i + 1].Last() + workArray[i].Last());
            current.Insert(0, workArray[i][0] - workArray[i + 1][0]);
        }

        return (workArray[0][0], workArray[0].Last());
    }
}