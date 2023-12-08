using Common;
using Common.Solver;
using MathNet.Numerics;
using Serilog;

namespace Day08;

[Day(2023, 8, "Haunted Wasteland")]
internal class Solver : SolverBase
{
    private readonly string instructions;
    private readonly Dictionary<string, Node> network = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading map...");
        instructions = inputLines[0];

        foreach (string line in inputLines[1..].Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            var node = Node.FromInput(line);
            network[node.Name] = node;
        }
    }

    public override Task Solve1()
    {
        logger.Information("Navigating through network...");

        var current = network["AAA"];
        var pos = 0;
        var count = 0;
        while (current.Name != "ZZZ")
        {
            current = instructions[pos] == 'L' ? network[current.Left] : network[current.Right];

            count++;
            pos++;
            if (pos == instructions.Length) pos = 0;
        }

        logger.Information("Getting to ZZZ takes {count} steps", count);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Navigating multiple nodes...");

        var nodes = network.Where(n => n.Key[2] == 'A').Select(n => n.Value).ToList();
        var sequences = nodes.Select(FindSequenceLengthForNode).ToList();

        // Because the start offset and the sequence length are same, a simple LCM is enough
        // Using LCM from https://www.nuget.org/packages/MathNet.Numerics/
        var lcm = sequences.First().sequenceLentgth;
        foreach ((var _, long length) in sequences.Skip(1))
        {
            lcm = Euclid.LeastCommonMultiple(lcm, length);
        }

        logger.Information("Getting all paths to **Z takes {count} steps", lcm);

        return Task.CompletedTask;

        (long offset, long sequenceLentgth) FindSequenceLengthForNode(Node node)
        {
            var current = node;
            var pos = 0;
            var count = 0;

            var start = 0;
            var end = 0;

            while (start == 0 || end == 0)
            {
                current = instructions[pos] == 'L' ? network[current.Left] : network[current.Right];
                count++;

                if (current.Name[2] == 'Z')
                {
                    if (start == 0) start = count;
                    else end = count;
                }

                pos++;
                if (pos == instructions.Length) pos = 0;
            }

            return (start, end - start);
        }
    }
}