using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day13;

[Day(2024, 13, "Claw Contraption")]
public class Solver : SolverBase
{
    private readonly List<ClawMachine> machines = [];

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Analyzing claw machines...");
        for (int i = 0; i < inputLines.Length; i += 4)
        {
            List<string> clawData = new() { inputLines[i], inputLines[i + 1], inputLines[i + 2] };
            machines.Add(ClawMachine.Parse(clawData.ToArray()));
        }
    }

    public override Task Solve1()
    {
        logger.Information("Finding minimum token amount...");

        long tokens = machines.Sum(m => CalculatePrizeCost(m));

        logger.Information("Total cost to get all prizes is {tokens} tokens", tokens);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Finding minimum token amount with correct input...");

        long tokens = machines.Sum(m => CalculatePrizeCost(m, null, 10000000000000L));

        logger.Information("Total cost to get all prizes is {tokens} tokens", tokens);

        return Task.CompletedTask;
    }

    private long CalculatePrizeCost(ClawMachine machine, int? maxPresses = 100, long prizeOffset = 0L)
    {
        // Achieving the prize is a pair of linear equations
        // Solved using Cramer's rule
        // x = button A presses
        // y = button B presses

        long prizeX = machine.PriceX + prizeOffset;
        long prizeY = machine.PriceY + prizeOffset;

        long x = ((prizeX * machine.ButtonBy) - (machine.ButtonBx * prizeY)) /
                 ((machine.ButtonAx * machine.ButtonBy) - (machine.ButtonBx * machine.ButtonAy));
        long y = ((machine.ButtonAx * prizeY) - (prizeX * machine.ButtonAy)) /
                 ((machine.ButtonAx * machine.ButtonBy) - (machine.ButtonBx * machine.ButtonAy));

        // Check for max press count
        if (maxPresses.HasValue)
        {
            if (x > maxPresses.Value || y > maxPresses.Value)
            {
                return 0;
            }
        }

        // Don't allow negative presses
        if (x < 0 || y < 0)
        {
            return 0;
        }

        // Don't allow non-integer solutions (check with original integer equation)
        if ((x * machine.ButtonAx) + (y * machine.ButtonBx) != prizeX
            || (x * machine.ButtonAy) + (y * machine.ButtonBy) != prizeY)
        {
            return 0;
        }

        return (3 * x) + y;
    }
}

internal record ClawMachine(long ButtonAx, long ButtonAy, long ButtonBx, long ButtonBy, long PriceX, long PriceY)
{
    public static ClawMachine Parse(string[] input)
    {
        (long x, long y) buttonA = ParseButton(input[0]);
        (long x, long y) buttonB = ParseButton(input[1]);

        string[] priceParts = input[2][7..].Split(',', StringSplitOptions.TrimEntries);
        long px = long.Parse(priceParts[0][2..]);
        long py = long.Parse(priceParts[1][2..]);

        return new ClawMachine(buttonA.x, buttonA.y, buttonB.x, buttonB.y, px, py);
    }

    private static (long x, long y) ParseButton(string input)
    {
        string[] parts = input[10..].Split(',', StringSplitOptions.TrimEntries);
        long x = long.Parse(parts[0][1..]);
        long y = long.Parse(parts[1][1..]);
        return (x, y);
    }
}