using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day05;

[Day(2024, 5, "Print Queue")]
public class Solver : SolverBase
{
    private readonly List<(int left, int right)> rules = [];
    private readonly List<int[]> pages = [];
    private readonly List<int[]> incorrect = [];

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading print rules...");

        bool inPages = false;

        foreach (string line in inputLines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (!inPages)
                {
                    inPages = true;
                }
                else
                {
                    break;
                }

                continue;
            }

            if (!inPages)
            {
                string[] split = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                rules.Add((int.Parse(split[0]), int.Parse(split[1])));
            }
            else
            {
                string[] split = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                pages.Add(Array.ConvertAll(split, int.Parse));
            }
        }
    }

    public override Task Solve1()
    {
        logger.Information("Validating print rules...");

        int middlePageSum = 0;
        foreach (int[] page in pages)
        {
            bool isValid = true;
            for (int i = 0; i < page.Length - 1; i++)
            {
                IEnumerable<(int left, int right)> leftRules = rules.Where(x => x.left == page[i]);
                foreach ((int left, int right) leftRule in leftRules)
                {
                    if (page[0..i].Any(x => x == leftRule.right))
                    {
                        isValid = false;
                        break;
                    }
                }

                IEnumerable<(int left, int right)> rightRules = rules.Where(x => x.right == page[i]);
                foreach ((int left, int right) rightRule in rightRules)
                {
                    if (page[(i + 1)..].Any(x => x == rightRule.left))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            if (isValid)
            {
                middlePageSum += page[page.Length / 2];
            }
            else
            {
                incorrect.Add(page); // collect incorrect pages for second part
            }
        }

        logger.Information("Middle page sum is {sum}", middlePageSum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Correcting invalid updates...");

        RuleComparer comparer = new RuleComparer(rules);

        int middlePageSum = incorrect
            .Select(i => i.OrderBy(x => x, comparer)
                .ToArray())
            .Select(ordered => ordered[ordered.Length / 2])
            .Sum();

        logger.Information("Middle page sum is {sum}", middlePageSum);

        return Task.CompletedTask;
    }
}

internal class RuleComparer(List<(int left, int right)> rules) : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (rules.Any(r => r.left == x && r.right == y))
        {
            return -1;
        }

        if (rules.Any(r => r.left == y && r.right == x))
        {
            return 1;
        }

        return 0;
    }
}