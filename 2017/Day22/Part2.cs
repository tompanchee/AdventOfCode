using Day22;

internal static class Part2
{
    public static void Solve() {
        var nodes = new Dictionary<(int x, int y), Status>();

        var data = File.ReadAllLines("input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

        var y = data.Length / 2;
        var xmax = data[0].Length / 2;

        foreach (var line in data) {
            for (var x = 0; x < data.Length; x++)
                if (line[x] == '#')
                    nodes.Add((x - xmax, y), Status.Infected);

            y--;
        }

        var carrier = new Carrier();
        Console.WriteLine();
        Console.WriteLine("Solving part 2...");
        var count = 0;
        for (var i = 0; i < 10000000; i++)
            if (Burst())
                count++;
        Console.WriteLine($"{count} bursts caused infection");

        bool Burst() {
            if (!nodes.ContainsKey(carrier.Location) || nodes[carrier.Location] == Status.Clean) {
                carrier.Turn(Carrier.TurnDirection.Left);
                nodes[carrier.Location] = Status.Weaken;
                carrier.Move();
            } else if (nodes[carrier.Location] == Status.Weaken) {
                nodes[carrier.Location] = Status.Infected;
                carrier.Move();
                return true;
            } else if (nodes[carrier.Location] == Status.Infected) {
                carrier.Turn(Carrier.TurnDirection.Right);
                nodes[carrier.Location] = Status.Flagged;
                carrier.Move();
            } else {
                // Flagged
                // reverse
                carrier.Turn(Carrier.TurnDirection.Right);
                carrier.Turn(Carrier.TurnDirection.Right);
                nodes[carrier.Location] = Status.Clean;
                carrier.Move();
            }

            return false;
        }
    }

    enum Status
    {
        Clean,
        Weaken,
        Infected,
        Flagged
    }
}