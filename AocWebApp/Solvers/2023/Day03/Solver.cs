using Common;
using Common.Solver;
using Serilog;

namespace Day03;

[Day(2023, 3, "Gear Ratios")]
internal class Solver : SolverBase
{
    private readonly Schematics schematics;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading schematics...");

        schematics = new Schematics(inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray());
    }

    public override Task Solve1()
    {
        logger.Information("Calculating sum of part numbers...");
        var sum = schematics.Numbers.Where(n => schematics.IsPartNumber(n)).Sum(n => n.Value);
        logger.Information("Sum of part numbers is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Calculation gear ratio...");
        var sum = schematics.Symbols.Sum(s => schematics.CalculateGearRatio(s));
        logger.Information("Gear ratio sum is {sum}", sum);

        return Task.CompletedTask;
    }
}