using Common;
using Common.Solver;
using Serilog;

namespace Day04;

[Day(2023, 4, "Scratchcards")]
internal class Solver : SolverBase
{
    private readonly List<(int[] winning, int[] numbers)> cards = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading cards...");
        foreach (string line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            var split = line[(line.IndexOf(':') + 1)..].Split('|');
            cards.Add(
                (
                    split[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(),
                    split[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()
                )
            );
        }
    }

    public override Task Solve1()
    {
        logger.Information("Calculating total scores...");
        var sum = cards.Sum(c =>
        {
            var count = c.numbers.Intersect(c.winning).Count();
            return count == 0 ? 0 : 1 << (count - 1);
        });
        logger.Information("Total score of all cards is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Going through card pile...");
        var numberOfCards = Enumerable.Repeat(1, cards.Count).ToArray();

        for (int i = 0; i < cards.Count; i++)
        {
            var count = cards[i].numbers.Intersect(cards[i].winning).Count();
            for (int j = i + 1; j < Math.Min(cards.Count, i + count + 1); j++)
            {
                numberOfCards[j] += numberOfCards[i];
            }
        }

        var sum = numberOfCards.Sum();
        logger.Information("Total number of cards at the end is {sum}", sum);

        return Task.CompletedTask;
    }
}