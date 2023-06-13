using Day22;

internal static class Part1
{
    public static void Solve() {
        var infectedNodes = new HashSet<(int x, int y)>();

        var data = File.ReadAllLines("input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

        var y = data.Length / 2;
        var xmax = data[0].Length / 2;

        foreach (var line in data) {
            for (var x = 0; x < data.Length; x++)
                if (line[x] == '#')
                    infectedNodes.Add((x - xmax, y));

            y--;
        }

        var carrier = new Carrier();

        Console.WriteLine();
        Console.WriteLine("Solving part 1...");
        var count = 0;
        for (var i = 0; i < 10000; i++)
            if (Burst())
                count++;
        Console.WriteLine($"{count} bursts caused infection");

        bool Burst() {
            if (infectedNodes!.Contains(carrier!.Location)) {
                carrier.Turn(Carrier.TurnDirection.Right);
                infectedNodes.Remove(carrier.Location);
                carrier.Move();
                return false;
            }

            carrier.Turn(Carrier.TurnDirection.Left);
            infectedNodes.Add(carrier.Location);
            carrier.Move();
            return true;
        }
    }
}