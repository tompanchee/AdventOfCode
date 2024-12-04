using System.Text.RegularExpressions;
using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day03;

[Day(2024, 3, "Mull It Over")]
public partial class Solver(string input, ILogger logger) : SolverBase(input, logger)
{
    private readonly Regex mulRegex = MulRegex();
    private readonly Regex disableRegex = DisableRegex();

    private readonly string program = input.Replace("\n", string.Empty);

    public override Task Solve1()
    {
        logger.Information("Multiplying...");
        var sum = Multiply(program);
        logger.Information("Sum of multiplications is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Calculating sum with disabled multiplications...");

        var memory = disableRegex.Replace(program, string.Empty);
        var sum = Multiply(memory);
        logger.Information("Sum of multiplications is {sum}", sum);

        return Task.CompletedTask;
    }

    private int Multiply(string memory)
    {
        var matches = mulRegex.EnumerateMatches(memory);
        var sum = 0;
        foreach (var match in matches)
        {
            var mul = memory[match.Index..(match.Index + match.Length)];
            var numbers = mul[4..^1].Split(',');
            sum += int.Parse(numbers[0]) * int.Parse(numbers[1]);
        }

        return sum;
    }

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"(don't\(\)).*?(do\(\))")]
    private static partial Regex DisableRegex();
}