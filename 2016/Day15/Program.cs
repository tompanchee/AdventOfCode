Console.WriteLine("Checking disc positions...");
var discs = Parse();

Console.WriteLine();

// Brute force all the way...
Console.WriteLine("Solving part 1...");
var t = 0;
while (discs.Any(d => d.GetPositionAt(t) != 0)) t++;

Console.WriteLine($"To get a capsule the button needs to pressed at {t}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
discs.Add(new Disc(discs.Count + 1, 11, 0));
t = 0;
while (discs.Any(d => d.GetPositionAt(t) != 0)) t++;

Console.WriteLine($"To get a capsule the button needs to pressed at {t}");

IList<Disc> Parse() {
    var result = new List<Disc>();

    foreach (var line in File.ReadAllLines("input.txt")) {
        if (string.IsNullOrWhiteSpace(line)) {
            continue;
        }

        var number = int.Parse(line[(line.IndexOf('#') + 1)..(line.IndexOf('#') + 2)]);
        var cycle = int.Parse(line[12..14]);
        var offset = int.Parse(line[(line.LastIndexOf(' ') + 1)..line.IndexOf('.')]);

        result.Add(new Disc(number, cycle, offset));
    }

    return result;
}

internal class Disc
{
    public Disc(int number, int cycleLength, int offset) {
        Number = number;
        CycleLength = cycleLength;
        Offset = offset;
    }

    public int Number { get; }
    public int CycleLength { get; }
    public int Offset { get; }

    public int GetPositionAt(int time) {
        return (time + Number + Offset) % CycleLength;
    }
}