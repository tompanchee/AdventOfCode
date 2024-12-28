using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day11;

[Day(2024, 11, "Plutonian Pebbles")]
public class Solver : SolverBase
{
    public Solver(string input, ILogger logger) : base(input, logger)
    {
    }

    public override Task Solve1()
    {
        logger.Information("Blinking...");
        var count = Blink(25);
        logger.Information("After 25 blinks count of stones is {count}", count);
        
        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Blinking...");
        var count = Blink(75);
        logger.Information("After 75 blinks count of stones is {count}", count);
        
        return Task.CompletedTask;
    }

    long Blink(int count)
    {
        Dictionary<long, long> current = input
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToDictionary(l=>l,l=>0L);

        var stoneCount = new Dictionary<long, long>();
        
        for (var i = 0; i < count; i++)
        {
            var stones = current.Keys.ToList();
            stoneCount.Clear();
            foreach (long stone in stones)
            {
                if (stone == 0) SetValue(stone, 1);
                else if (stone.ToString().Length % 2 == 0)
                {
                    var str = stone.ToString();
                    SetValue(stone,long.Parse(str[..(str.Length / 2)]));
                    SetValue(stone, long.Parse(str[(str.Length / 2)..]));
                }
                else
                {
                    SetValue(stone, 2024 * stone);
                }
            }
            
            current = new Dictionary<long, long>(stoneCount);
        }
        
        return current.Values.Sum();
        
        void SetValue(long oldStone, long stone)
        {
            _ = current.TryGetValue(oldStone, out var value);
            _ = stoneCount.TryGetValue(stone, out var curCount);
            stoneCount[stone] = curCount + (value == 0 ? 1 : value);
        }
    }
}
