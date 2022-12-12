using AOCCommon;
using Serilog.Core;

namespace Day11;

[Day(11, "Monkey in the Middle")]
internal class Solver : SolverBase
{
    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Playing game for 20 rounds...");

        var bunch = MonkeyBunch.FromInput(data);

        for (var i = 0; i < 20; i++) {
            bunch.PlayRound();

            logger.Debug("After round {i} the monkeys have the following items:", i + 1);
            logger.Debug(bunch.GetDebugData());
        }

        logger.Information("Monkey business after 20 rounds is {0}", bunch.MonkeyBusiness);
    }

    public override void Solve2() {
        var reducer = data
            .Where(r => r.Contains("divisible"))
            .Select(row => int.Parse(row[row.LastIndexOf(' ')..]))
            .Aggregate(1L, (current, value) => current * value);

        var bunch = MonkeyBunch.FromInput(data, false, reducer);

        for (var i = 0; i < 10000; i++) bunch.PlayRound();

        logger.Information("Monkey business after 10000 rounds is {0}", bunch.MonkeyBusiness);
    }

    protected override void PostConstruct() {
        logger.Information("Observing monkeys...");
    }
}