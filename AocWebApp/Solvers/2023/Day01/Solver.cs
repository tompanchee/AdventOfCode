using Common;
using Common.Solver;
using Serilog;

namespace Day01;

[Day(2023, 1, "")]
internal class Solver : SolverBase
{
    static readonly (string word, string digit)[] Numbers =
    {
        ("zero", "0"), 
        ("one", "1"),
        ("two", "2"),
        ("three", "3"),
        ("four", "4"),
        ("five", "5"),
        ("six", "6"), 
        ("seven", "7"), 
        ("eight", "8"),
        ("nine", "9")
    };

    public Solver(string input, ILogger logger) : base(input, logger) { }

    public override Task Solve1()
    {
        logger.Information("Reading calibration values...");

        var sum = 0;
        foreach (var line in inputLines.Where(l => !string.IsNullOrEmpty(l)))
        {
            var digits = line.Where(char.IsDigit).ToList();
            if (!digits.Any()) continue;
            sum += 10 * (digits.First() - '0') + digits.Last() - '0';
        }

        logger.Information("Calibration sum is {sum}", sum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Reading real calibration values...");

        var sum = 0;
        foreach (var line in inputLines.Where(l => !string.IsNullOrEmpty(l)))
        {
            var firsts = new List<int>();
            var lasts = new List<int>();

            for (int i = 0; i < Numbers.Length; i++)
            {
                var fw = line.IndexOf(Numbers[i].word, StringComparison.InvariantCulture);
                var fd = line.IndexOf(Numbers[i].digit, StringComparison.InvariantCulture);

                if (fd < 0) fd = 999;
                if (fw < 0) fw = 999;
                firsts.Add(Math.Min(fd, fw));

                var lw = line.LastIndexOf(Numbers[i].word, StringComparison.InvariantCulture);
                var ld = line.LastIndexOf(Numbers[i].digit, StringComparison.InvariantCulture);

                if (lw < 0) fd = -999;
                if (ld < 0) fw = -999;
                lasts.Add(Math.Max(lw, ld));
            }

            var first = firsts.IndexOf(firsts.Min());
            var last = lasts.IndexOf(lasts.Max());

            sum += 10 * first + last;
        }

        logger.Information("Real calibration sum is {sum}", sum);

        return Task.CompletedTask;
    }
}