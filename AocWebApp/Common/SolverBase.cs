using Serilog;

namespace Common;

public abstract class SolverBase : ISolver
{
    protected readonly string input;
    protected readonly ILogger logger;

    protected SolverBase(string input, ILogger logger)
    {
        this.input = input;
        this.logger = logger;
    }

    public abstract Task Solve1();
    public abstract Task Solve2();
}