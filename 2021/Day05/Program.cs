var input = File.ReadAllLines("input.txt");
var lines = ParseInput(input);
var grid = new Dictionary<(int x, int y), int>();

Console.WriteLine("Solving puzzle 1...");

foreach(var line in lines) {
    Plot(grid, line.Item1, line.Item2);
}
Console.WriteLine($"{grid.Values.Where(p=>p>1).Count()} points have at least two vents");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");

grid.Clear();
foreach (var line in lines) {
    Plot(grid, line.Item1, line.Item2, true);
}
Console.WriteLine($"{grid.Values.Where(p => p > 1).Count()} points have at least two vents");

IList<Tuple<(int x, int y), (int x, int y)>> ParseInput(string[] input) {
    var result = new List<Tuple<(int x, int y), (int x, int y)>>();

    foreach(var item in input) {
        var split = item.Split("->", StringSplitOptions.TrimEntries);
        var start = split[0].Split(',').Select(int.Parse).ToArray();
        var end = split[1].Split(',').Select(int.Parse).ToArray();
        result.Add(Tuple.Create((start[0], start[1]), (end[0], end[1])));
    }

    return result;
}

void Plot(Dictionary<(int x, int y), int> grid, (int x, int y) start, (int x, int y) end, bool alsoDiagonal = false) {
    var dx = 0;
    var dy = 0;

    if (IsHorizontal(start, end)) {
        dy = 0;
        dx = end.x > start.x ? 1 : -1;
    } else if (IsVertical(start, end)) {
        dx = 0;
        dy = end.y > start.y ? 1 : -1; }
    else {
        if (!alsoDiagonal) return;
        dx = end.x > start.x ? 1 : -1;
        dy = end.y > start.y ? 1 : -1;
    }

    var current = start;
    var loopEnd = (end.x + dx, end.y + dy);
    while (current != loopEnd) {
        if (!grid.ContainsKey(current)) {
            grid.Add(current, 0);
        }

        grid[current] += 1;

        current.x += dx;
        current.y += dy;
    }
}

bool IsHorizontal((int x, int y) p1, (int x, int y) p2) => p1.y == p2.y;
bool IsVertical((int x, int y) p1, (int x, int y) p2) => p1.x == p2.x;
