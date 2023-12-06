using Common;
using Common.Solver;
using Serilog;

namespace Day05;

[Day(2023, 5, "If You Give A Seed A Fertilizer")]
internal class Solver : SolverBase
{
    private readonly List<Item> seeds = new();
    private readonly Dictionary<string, Map> maps = new();

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading Island Island Almanac...");
        Parse();
    }

    public override Task Solve1()
    {
        logger.Information("Finding lowest location for seed...");
        long minLocation = int.MaxValue;
        foreach (var seed in seeds)
        {
            var current = seed;
            while (true)
            {
                var next = maps.ContainsKey(current!.Category) ? maps[current.Category].GetNext(current) : null;
                if (next == null) break;
                current = next;
            }

            if (current.Value < minLocation)
            {
                minLocation = current.Value;
            }
        }

        logger.Information("Lowest location where a seed is planted is {min}", minLocation);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Calculating seed ranges from Almanac...");
        List<(long start, long length)> seedRanges = new();
        var seedData = inputLines[0][6..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        for (var i = 0; i < seedData.Length; i += 2)
        {
            seedRanges.Add((long.Parse(seedData[i]), long.Parse(seedData[i + 1])));
        }

        // Ugly brute force solution, optimize?
        long minLocation = int.MaxValue;
        foreach ((long start, var length) in seedRanges)
        {
            logger.Debug("Starting analyzing seed pair...");
            for (var i = start; i < start + length; i++)
            {
                var seed = new Item("seed", i);

                var current = seed;
                while (true)
                {
                    var next = maps.ContainsKey(current!.Category) ? maps[current.Category].GetNext(current) : null;
                    if (next == null) break;
                    current = next;
                }

                if (current.Value < minLocation)
                {
                    minLocation = current.Value;
                }
            }
        }

        logger.Information("Lowest location where a seed is planted is {min}", minLocation);

        return Task.CompletedTask;
    }

    private void Parse()
    {
        Map? map = null;
        foreach (var line in inputLines)
        {
            if (line.StartsWith("seeds:"))
            {
                foreach (var seed in line[6..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                {
                    seeds.Add(new Item("seed", long.Parse(seed)));
                }

                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                if (map != null)
                {
                    maps.Add(map.Source, map);
                    map = null;
                }

                continue;
            }

            if (char.IsLetter(line[0]))
            {
                var split = line[..line.IndexOf(' ')].Split("-to-");
                (string source, string destination) = (split[0], split[1]);

                map = new Map(source, destination);
            }
            else
            {
                var split = line.Split(' ');
                (long sourceStart, long destinationStart, long length) = (long.Parse(split[1]), long.Parse(split[0]), long.Parse(split[2]));
                map?.SourceStart.Add(sourceStart);
                map?.DestinationStart.Add(destinationStart);
                map?.Length.Add(length);
            }
        }
    }
}

internal record Item(string Category, long Value);

internal class Map
{
    public Map(string source, string destination)
    {
        Source = source;
        Destination = destination;
    }

    public Item? GetNext(Item current)
    {
        if (Source != current.Category) return null;

        var nextValue = current.Value;
        for (int i = 0; i < SourceStart.Count; i++)
        {
            var start = SourceStart[i];
            var end = start + Length[i] - 1;
            if (current.Value >= start && current.Value <= end)
            {
                nextValue = DestinationStart[i] + (current.Value - start);
                break;
            }
        }

        return new Item(Destination, nextValue);
    }

    public string Source { get; }
    public string Destination { get; }
    public List<long> SourceStart { get; } = new();
    public List<long> DestinationStart { get; } = new();
    public List<long> Length { get; } = new();
}