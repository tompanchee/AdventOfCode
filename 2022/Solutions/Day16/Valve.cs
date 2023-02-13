namespace Day16;

internal class Valve
{
    Valve(string id, int flowRate, string[] neighbours) {
        Id = id;
        FlowRate = flowRate;
        Neighbours = neighbours;
    }

    public string Id { get; }
    public int FlowRate { get; }
    public string[] Neighbours { get; }

    public static Valve Parse(string input) {
        var id = input[6..8];
        var flowRate = int.Parse(input[(input.IndexOf('=') + 1)..input.IndexOf(';')]);
        var valves = input[(input.IndexOf("to", StringComparison.InvariantCulture) + 9)..];
        var neighbours = valves.Split(',', StringSplitOptions.TrimEntries);

        return new Valve(id, flowRate, neighbours);
    }
}