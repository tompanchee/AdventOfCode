using AOCCommon;
using Serilog.Core;

namespace Day25;

[Day(25, "Full of Hot Air")]
internal class Solver : SolverBase
{
    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Calculating sum...");
        var sum = data.Where(snafu => !string.IsNullOrWhiteSpace(snafu)).Sum(snafu => snafu.SNAFUToLong());

        logger.Information("Resolving sum in SNAFU...");
        var result = sum.LongToSNAFU();

        logger.Information("Number to input to Bob's console is {result}", result);
    }

    public override void Solve2() { }

    protected override void PostConstruct() {
        logger.Information("Reading numbers...");
    }
}