using Common;
using Common.Solver;
using Serilog;

namespace Day02;

[Day(2023, 2, "Cube Conundrum")]
internal class Solver : SolverBase
{
    private readonly List<Game> games = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Playing games...");

        foreach (string line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            games.Add(Game.FromInput(line));
        }
    }

    public override Task Solve1()
    {
        var sum = games.Where(g => !g.IsImpossible()).Sum(g => g.ID);
        logger.Information("Sum of possible game Ids is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Calculating power...");
        var sum = games.Sum(g => g.Power());
        logger.Information("Total power is {sum}", sum);

        return Task.CompletedTask;
    }
}