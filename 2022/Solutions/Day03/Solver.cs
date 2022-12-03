using AOCCommon;
using Serilog.Core;

namespace Day03;

[Day(3, "Rucksack Reorganization")]
public class Solver : SolverBase
{
    const string priority = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    List<(string c1, string c2)> rucksacks = new();
    
    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1()
    {
        var sum = 0;
        foreach(var rucksack in rucksacks)
        {
            var duplicate = rucksack.c1.Intersect(rucksack.c2).First();
            sum += priority.IndexOf(duplicate) + 1;
        }

        logger.Information("Sum of priorities of items in both compartments is {sum}", sum);
    }

    public override void Solve2()
    {
        var sum = 0;
        for(var i = 0; i < data.Length; i += 3) {
            var commonItem = data[i].Intersect(data[i + 1]).Intersect(data[i + 2]).First();
            sum += priority.IndexOf(commonItem) + 1;
        }

        logger.Information("Sum of bagde items of groups is {sum}", sum);
    }

    protected override void PostConstruct()
    {
        logger.Information("Examining rucksacks...");

        foreach(var row in data)
        {
            var half = row.Length/2;
            rucksacks.Add((row[..half], row[half..]));
        }
    }
}
