using Common;
using Common.Solver;
using Serilog;

namespace Day12;

[Day(2023, 12, "Hot Springs")]
internal class Solver : SolverBase
{
    readonly private List<Report> reports = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading condition reports...");

        foreach (string data in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            reports.Add(Report.FromInput(data));
        }
    }

    public override Task Solve1()
    {
        logger.Information("Finding valid arrangements...");
        foreach (var report in reports)
        {
            // Brute force all combinations and calculate sum
            var missingBrokenSprings = report.Groups.Sum() - report.Row.Count(c => c == '#');
            var combinations = new List<string>();
            GenerateSpringCombinations(report.Row, missingBrokenSprings, 0, combinations);

            foreach (var combination in combinations.Where(combination => report.IsValid(combination)))
            {
                report.ValidArrangements.Add(combination);
            }
        }

        var sum = reports.Sum(r => r.ValidArrangements.Count);
        logger.Information("Sum of valid arrangements is {sum}", sum);

        return Task.CompletedTask;

        static void GenerateSpringCombinations(string pattern, int brokenCount, int index, ICollection<string> result)
        {
            while (true)
            {
                // If all '#' symbols are placed, print the combination
                if (brokenCount == 0)
                {
                    result.Add(pattern.Replace('?', '.'));
                    return;
                }

                // Place '#' at the current index and recurse
                if (index < pattern.Length && pattern[index] == '?')
                {
                    pattern = pattern[..index] + "#" + pattern[(index + 1)..];
                    GenerateSpringCombinations(pattern, brokenCount - 1, index + 1, result);
                    pattern = pattern[..index] + "?" + pattern[(index + 1)..]; // Reset string
                }

                // Move to the next index without placing '#'
                if (index < pattern.Length)
                {
                    index++;
                    continue;
                }

                break;
            }
        }
    }

    public override Task Solve2()
    {
        logger.Information("Calculating folded combinations...");
        var sum = reports.Sum(r => r.CalculateFoldedCombinations());

        logger.Information("Sum of all folded valid combinations is {sum}", sum);

        return Task.CompletedTask;
    }
}