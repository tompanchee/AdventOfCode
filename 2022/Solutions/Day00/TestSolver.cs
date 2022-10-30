using AOCCommon;
using Serilog.Core;

namespace Day00
{
    [Day(0, "Test day")]
    public class TestSolver : SolverBase
    {
        public TestSolver(string path, Logger logger) : base(path, logger)
        {
        }

        public override void Solve1()
        {
            logger.Information("Problem 1 result: {0}", data[0]);
        }

        public override void Solve2()
        {
            logger.Information("Problem 2 result: {0}", data[1]);
        }
    }
}
