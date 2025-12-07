using Common;
using Common.Solver;
using Serilog;

namespace Y25_Day01;

[Day(2025, 1, "Secret Entrance")]
public class Solver(string input, ILogger logger) : SolverBase(input, logger)
{
    public override Task Solve1()
    {
        logger.Information("Solving part 1...");

        int zeroCount = 0;
        int dial = 50;
        foreach (string line in inputLines.Where(line => !string.IsNullOrWhiteSpace(line)))
        {
            int turn = int.Parse(line[1..]);
            dial = line[0] switch
            {
                'R' => Mod(dial + turn, 100),
                'L' => Mod(dial - turn, 100),
                _ => dial
            };

            if (dial == 0)
            {
                zeroCount++;
            }
        }

        logger.Information("Password is {ZeroCount}", zeroCount);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Solving part 2...");
        int zeroCount = 0;
        int dial = 50;

        foreach (string line in inputLines.Where(line => !string.IsNullOrWhiteSpace(line)))
        {
            int turn = int.Parse(line[1..]);
            int rounds = turn / 100;
            int leftOver = turn % 100;
            zeroCount += rounds;

            if (line[0] == 'R')
            {
                if (dial != 0 && dial + leftOver >= 100)
                {
                    zeroCount++;
                }

                dial = Mod(dial + turn, 100);
            }
            else
            {
                if (dial != 0 && dial <= leftOver)
                {
                    zeroCount++;
                }

                dial = Mod(dial - turn, 100);
            }
        }

        logger.Information("Password is {ZeroCount}", zeroCount);

        return Task.CompletedTask;
    }

    private static int Mod(int a, int b)
    {
        int m = a % b;
        return m < 0 ? m + b : m;
    }
}