using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day07;

[Day(2024, 7, "Bridge Repair")]
public class Solver : SolverBase
{
    private readonly List<Calibration> calibrations = [];

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        foreach (var line in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            var split = line.Split(":");
            var result = long.Parse(split[0]);
            var values = split[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            calibrations.Add(new Calibration(result, values));
        }
    }

    public override Task Solve1()
    {
        logger.Information("Catching operators from elephants...");
        var result = calibrations.Where(IsValid).Sum(c => c.Result);
        logger.Information("Calibration result is {Result}", result);

        return Task.CompletedTask;

        static bool IsValid(Calibration calibration)
        {
            if (calibration.Values.Length == 1)
            {
                return calibration.Values[0] == calibration.Result;
            }

            // +
            var values1 = new List<long>(calibration.Values.Skip(2));
            values1.Insert(0, calibration.Values[0] + calibration.Values[1]);

            // *
            var values2 = new List<long>(calibration.Values.Skip(2));
            values2.Insert(0, calibration.Values[0] * calibration.Values[1]);

            return IsValid(calibration with { Values = [.. values1] })
                   || IsValid(calibration with { Values = [.. values2] });
        }
    }

    public override Task Solve2()
    {
        logger.Information("Catching operators from elephants...");
        var result = calibrations.Where(IsValid).Sum(c => c.Result);
        logger.Information("Calibration result is {Result}", result);

        return Task.CompletedTask;

        static bool IsValid(Calibration calibration)
        {
            try
            {
                if (calibration.Values.Length == 1)
                {
                    return calibration.Values[0] == calibration.Result;
                }

                if (calibration.Values[0] > calibration.Result)
                {
                    return false;
                }

                // ||
                var values1 = new List<long>(calibration.Values.Skip(2));
                values1.Insert(0, long.Parse(calibration.Values[0] + calibration.Values[1].ToString()));

                // *
                var values2 = new List<long>(calibration.Values.Skip(2));
                values2.Insert(0, calibration.Values[0] * calibration.Values[1]);

                // +
                var values3 = new List<long>(calibration.Values.Skip(2));
                values3.Insert(0, calibration.Values[0] + calibration.Values[1]);

                return IsValid(calibration with { Values = [.. values1] })
                       || IsValid(calibration with { Values = [.. values2] })
                       || IsValid(calibration with { Values = [.. values3] });
            }
            catch
            {
                return false;
            }
        }
    }
}

internal record Calibration(long Result, long[] Values);