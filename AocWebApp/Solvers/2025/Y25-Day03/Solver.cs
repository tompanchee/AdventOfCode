using System.Text;
using Common;
using Common.Solver;
using Serilog;

namespace Y25_Day03;

[Day(2025, 3, "New Solver")]
public class Solver : SolverBase
{
    private readonly List<int[]> banks = [];

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        foreach (var line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            banks.Add(line.Select(c => int.Parse(c.ToString())).ToArray());;
        }
    }

    public override Task Solve1()
    {
        logger.Information("Solving part 1...");
        logger.Information("Total joltage is {totalJoltage}", banks.Sum(b=>CalculateJoltage(b, 2)));
        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Solving part 2...");
        logger.Information("Total joltage is {totalJoltage}", banks.Sum(b=>CalculateJoltage(b, 12)));
        return Task.CompletedTask;
    }

    private long CalculateJoltage(int[] bank, int batteries)
    {
        var start = 0;
        var joltage = new StringBuilder();
        for (var b = batteries - 1; b >= 0; b--)
        {
            var max = bank[start..^b].Max();
            joltage.Append(max);
            start = Array.IndexOf(bank[start..], max) + start + 1;   
        }
        
        return long.Parse(joltage.ToString());
    }
}
