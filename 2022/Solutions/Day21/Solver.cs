using AOCCommon;
using Serilog.Core;

namespace Day21;

[Day(21, "Monkey Math")]
internal class Solver : SolverBase
{
    Dictionary<string, Monkey> monkeys = new();

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        while (monkeys["root"].Value == null)
            foreach (var (_, monkey) in monkeys.Where(m => m.Value.Value == null)) {
                var lMonkey = ResolveLMonkey(monkey);
                var rMonkey = ResolveRMonkey(monkey);

                if (lMonkey?.Value != null) monkey.Expression!.Left = lMonkey.Value.ToString();
                if (rMonkey?.Value != null) monkey.Expression!.Right = rMonkey.Value.ToString();
            }

        logger.Information("Root monkey is yelling value {0}", monkeys["root"].Value!.Value);
    }

    public override void Solve2() {
        // Multiple numbers yelled provide the correct answer. The Binary search gave an incorrect result.
        // Tried with the commented numbers and got the correct answer. Probably some division thingy with big integers.

        //var humanYelling = 3876027196185;
        //long diff = -1;

        long humanYelling = 0;
        var diff = 100000000000;

        while (true) {
            ParseInput();
            var human = monkeys["humn"];
            human.Value = humanYelling;

            var root = monkeys["root"];
            while (!long.TryParse(root.Expression!.Left, out var _) || !long.TryParse(root.Expression.Right, out var _))
                foreach (var (_, monkey) in monkeys.Where(m => m.Value.Value == null)) {
                    var lMonkey = ResolveLMonkey(monkey);
                    var rMonkey = ResolveRMonkey(monkey);

                    if (lMonkey?.Value != null) monkey.Expression!.Left = lMonkey.Value.ToString();
                    if (rMonkey?.Value != null) monkey.Expression!.Right = rMonkey.Value.ToString();
                }

            logger.Debug("Human: {0}, Left: {1}, Right: {2}", humanYelling, root.Expression.Left, root.Expression.Right);

            if (root.Expression!.Left.Equals(root.Expression.Right)) break;

            // Binary search
            if (Math.Sign(long.Parse(root.Expression.Left) - long.Parse(root.Expression.Right)) != Math.Sign(diff)) diff /= -2;

            humanYelling += diff;
        }

        logger.Information("I have to yell {0} to make root values equal", humanYelling);
    }

    Monkey? ResolveLMonkey(Monkey monkey) {
        return !monkeys.ContainsKey(monkey.Expression!.Left) ? null : monkeys[monkey.Expression!.Left];
    }

    Monkey? ResolveRMonkey(Monkey monkey) {
        return !monkeys.ContainsKey(monkey.Expression!.Right) ? null : monkeys[monkey.Expression!.Right];
    }

    protected override void PostConstruct() {
        logger.Information("Listening to monkeys...");

        ParseInput();
    }

    void ParseInput() {
        monkeys.Clear();

        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;
            var monkey = Monkey.Parse(row);
            monkeys.Add(monkey.Id, monkey);
        }
    }
}