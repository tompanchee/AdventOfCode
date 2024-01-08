using System.Collections.Specialized;
using System.Text;
using Common;
using Common.Solver;
using Serilog;

namespace Day15;

[Day(2023, 15, "Lens Library")]
internal class Solver : SolverBase
{
    private readonly string[] initializationSequence;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading initialization sequence...");
        initializationSequence = input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    public override Task Solve1()
    {
        logger.Information("Calculating hash sum...");
        logger.Information("Hash sum is {sum}", initializationSequence.Sum(CalculateHash));

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Initializing lenses...");

        var boxes = new List<Box>();
        for (int i = 0; i < 256; i++)
        {
            boxes.Add(new Box(i));
        }

        foreach (string instruction in initializationSequence)
        {
            (int boxId, string label, char operation, int focalLength) = ParseInstruction(instruction);
            var box = boxes[boxId];
            if (operation == '-')
            {
                if (box.Lenses.Contains(label)) box.Lenses.Remove(label);
            }
            else
            {
                box.Lenses[label] = focalLength;
            }
        }

        var power = 0;
        foreach (Box box in boxes)
        {
            var lensValues = new int[box.Lenses.Count];
            box.Lenses.Values.CopyTo(lensValues, 0);
            power += lensValues.Select((t, i) => (box.Id + 1) * (i + 1) * t).Sum();
        }

        logger.Information("Total focusing power is {power}", power);

        return Task.CompletedTask;
    }

    private (int boxId, string label, char operation, int focalLength) ParseInstruction(string instruction)
    {
        if (instruction.Contains('-'))
        {
            var split = instruction.Split('-');
            return (CalculateHash(split[0]), split[0], '-', 0);
        }
        else
        {
            var split = instruction.Split('=');
            return (CalculateHash(split[0]), split[0], '=', int.Parse(split[1]));
        }
    }

    private static int CalculateHash(string str)
    {
        var current = 0;
        var bytes = Encoding.ASCII.GetBytes(str);

        foreach (byte b in bytes)
        {
            current += b;
            current *= 17;
            current %= 256;
        }

        return current;
    }
}

internal class Box
{
    public Box(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public OrderedDictionary Lenses { get; } = new();
}