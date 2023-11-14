using Common;
using Common.Solver;
using Serilog;

namespace Day00;

[Day(2023, 0, "Test day")]
public class Class : SolverBase
{
    public Class(string input, ILogger logger) : base(input, logger)
    {
    }

    public override Task Solve1()
    {
        logger.Information("Problem 1 - with {input}", input);
        logger.Debug("This is a debug log");
        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Problem 2 - with {input}", input);
        return Task.CompletedTask;
    }
}
