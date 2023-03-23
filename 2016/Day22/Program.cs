using System.Text;

var input = File.ReadAllLines("input.txt");

var nodes = Parse(input);

Console.WriteLine("Solving part 1...");

var count = nodes
    .Where(a => a.Used != 0)
    .Sum(a => nodes.Where(b => a != b)
        .Count(b => a.Used <= b.Available));

Console.WriteLine($"There are {count} viable pairs");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

// Building grid
var maxx = nodes.Select(n => n.X).Max();
var maxy = nodes.Select(n => n.Y).Max();

var goal = nodes.Single(n => n.Y == 0 && n.X == maxx);

var grid = new char[maxx + 1, maxy + 1];

foreach (var node in nodes) {
    var c = '.';
    if (node.X == maxx && node.Y == 0) c = 'G';
    else if (node.Used == 0) c = '_';
    else if (node.Used > goal.Size) c = '#';

    grid[node.X, node.Y] = c;
}

// Print grid
var builder = new StringBuilder();
for (var y = 0; y <= maxy; y++) {
    for (var x = 0; x <= maxx; x++) builder.Append(grid[x, y]);

    builder.AppendLine();
}

Console.WriteLine("Grid visualization");
Console.WriteLine(builder.ToString());

// Solve manually from map (Yes I know a boring solution)
// 1. Move empty node to top row
// 2. Move goal to target moving empty node towards target in 5 moves/step

List<Node> Parse(string[] strings) {
    var result = new List<Node>();

    foreach (var row in input) {
        if (string.IsNullOrWhiteSpace(row) || row[0] != '/') continue;
        ;
        result.Add(Node.Parse(row));
    }

    return result;
}

internal class Node
{
    readonly int available;
    readonly int size;
    readonly int used;
    readonly int x;
    readonly int y;

    public Node(int x, int y, int used, int available, int size) {
        this.x = x;
        this.y = y;
        this.used = used;
        this.available = available;
        this.size = size;
    }

    public int X => x;
    public int Y => y;
    public int Used => used;
    public int Available => available;
    public int Size => size;

    public static Node Parse(string row) {
        var pos = row[15..23].Split('-', StringSplitOptions.TrimEntries);
        var x = int.Parse(pos[0][1..]);
        var y = int.Parse(pos[1][1..]);
        var used = int.Parse(row[30..33]);
        var avail = int.Parse(row[37..40]);
        var size = int.Parse(row[24..27]);

        return new Node(x, y, used, avail, size);
    }
}