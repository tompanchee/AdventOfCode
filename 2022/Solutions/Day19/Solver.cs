using AOCCommon;
using Serilog.Core;

namespace Day19;

[Day(19, "Not Enough Minerals")]
internal class Solver : SolverBase
{
    readonly List<Blueprint> blueprints = new();

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Mining with different blueprints...");

        var sum = 0;
        Parallel.ForEach(blueprints, b => {
            logger.Information("Blueprint {id}", b.Id);
            var mine = new Mine(b);
            var geodes = mine.StartMining(24);
            logger.Information("Blueprint {id} produces {geodes} geodes", b.Id, geodes);
            sum += b.Id * geodes;
        });

        logger.Information("Sum of blueprint qualities is {sum}", sum);
    }

    public override void Solve2() {
        logger.Information("Mining with three first blueprints...");

        var product = 1;
        Parallel.ForEach(blueprints.Take(3), b => {
            logger.Information("Blueprint {id}", b.Id);
            var mine = new Mine(b);
            var geodes = mine.StartMining(32);
            logger.Information("Blueprint {id} produces {geodes} geodes", b.Id, geodes);
            product *= geodes;
        });

        logger.Information("Product of geodes mined is {product}", product);
    }

    protected override void PostConstruct() {
        // Test data
        //data = new[] {
        //    "Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.",
        //    "Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian."
        //};

        logger.Information("Reading blueprints...");

        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;

            var id = int.Parse(row[10..row.IndexOf(':')]);
            var instructions = row[(row.IndexOf(':') + 1)..];
            var split = instructions.Split(". ", StringSplitOptions.TrimEntries);
            var oreCost = GetSingleValue(split[0], 21);
            var clayCost = GetSingleValue(split[1], 21);
            var obsidianCost = GetDoubleValue(split[2], 25, 36);
            var geodeCost = GetDoubleValue(split[3], 22, 33);

            blueprints.Add(new Blueprint(id, oreCost, clayCost, obsidianCost, geodeCost));
        }

        int GetSingleValue(string value, int index) {
            return int.Parse(value[index..(index + 2)]);
        }

        (int, int) GetDoubleValue(string value, int index1, int index2) {
            return (int.Parse(value[index1..(index1 + 2)]), int.Parse(value[index2..(index2 + 2)]));
        }
    }
}