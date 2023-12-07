using Common;
using Common.Solver;
using Serilog;

namespace Day07;

[Day(2023, 7, "Camel Cards")]
internal class Solver : SolverBase
{
    private readonly List<Hand> hands;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading hands and bids...");
        hands = inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).Select(Hand.FromInput).ToList();
    }

    public override Task Solve1()
    {
        logger.Information("Comparing hands...");
        var comparer = new HandComparer();
        var ordered = hands.OrderByDescending(h => h, comparer).ToList();

        logger.Information("Calculating total winnings...");
        var sum = ordered.Select((t, i) => t.CalculateWinning(i + 1)).Sum();

        logger.Information("Total winnings is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Comparing hands with Jokers...");
        var comparer = new HandComparer(true);
        var ordered = hands.OrderByDescending(h => h, comparer).ToList();

        logger.Information("Calculating total winnings...");
        var sum = ordered.Select((t, i) => t.CalculateWinning(i + 1)).Sum();

        logger.Information("Total winnings is {sum}", sum);

        return Task.CompletedTask;
    }
}