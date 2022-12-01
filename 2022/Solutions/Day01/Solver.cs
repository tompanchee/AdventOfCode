using AOCCommon;
using Serilog.Core;

namespace Day01;

[Day(1, "Calorie Counting")]
public class Solver : SolverBase
{
    public Solver(string path, Logger logger) : base(path, logger) { }

    readonly List<int> caloriesCarriedByElf = new();

    public override void Solve1()
    {
        var max = caloriesCarriedByElf.Max();

        logger.Information("Max total calories carried by an elf is {max} calories", max);
    }

    public override void Solve2()
    {
        var sum = caloriesCarriedByElf.OrderByDescending(x => x).Take(3).Sum();

        logger.Information("Total calories carried by top three elves is {sum} calories", sum);
    }

    protected override void PostConstruct()
    {
        logger.Information("Analyzing snacks...");

        var totalCalories = 0;
        foreach (var row in data)
        {
            if (string.IsNullOrEmpty(row))
            {
                caloriesCarriedByElf.Add(totalCalories);
                totalCalories = 0;
                continue;
            }

            totalCalories += int.Parse(row);
        }
    }
}
