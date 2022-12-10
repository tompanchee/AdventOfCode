using System.Text;
using AOCCommon;
using Serilog.Core;

namespace Day10;

[Day(10, "Cathode-Ray Tube")]
internal class Solver : SolverBase
{
    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        var cycleValues = new Dictionary<int, int>();
        var device = new Device(logger);

        device.Subscribe(20, RegisterValues);
        device.Subscribe(60, RegisterValues);
        device.Subscribe(100, RegisterValues);
        device.Subscribe(140, RegisterValues);
        device.Subscribe(180, RegisterValues);
        device.Subscribe(220, RegisterValues);

        device.Run(data);

        var sum = cycleValues.Sum(v => v.Key * v.Value);
        logger.Information("Sum of required signal strengths is {sum}", sum);

        void RegisterValues(int cycle, int x) {
            cycleValues.Add(cycle, x);
        }
    }

    public override void Solve2() {
        var sb = new StringBuilder();
        var currentRow = -1;

        var device = new Device(logger, RegisterValue);

        device.Run(data);

        logger.Information(sb.ToString());

        void RegisterValue(int cycle, int x) {
            var row = (cycle - 1) / 40;
            var col = (cycle - 1) % 40;

            if (row != currentRow) {
                currentRow = row;
                sb.AppendLine();
            }

            sb.Append(Math.Abs(col - x) < 2 ? '#' : '.');
        }
    }

    protected override void PostConstruct() {
        logger.Information("Reading program...");
    }
}