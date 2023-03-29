Console.WriteLine("Scanning HVAC system...");

(int dx, int dy)[] offsets = {
    (-1, 0),
    (1, 0),
    (0, -1),
    (0, 1)
};

var freePoints = new HashSet<Point>();
var pointsToClean = new Dictionary<Point, char>();

Console.WriteLine();

Parse();

Console.WriteLine("Solving part 1...");

var startPos = pointsToClean.Single(i => i.Value.Equals('0')).Key;
var shortestPath = GetShortestPath(startPos);
Console.WriteLine($"Shortest path to clean the HVAC system is {shortestPath} steps");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

shortestPath = GetShortestPath(startPos, true);
Console.WriteLine($"Shortest path to clean the HVAC system and return to start is {shortestPath} steps");

int GetShortestPath(Point start, bool returnToStart = false) {
    var queue = new Queue<(Point pos, int count, string toClean)>();
    var visited = new HashSet<(Point pos, int count, string toClean)>();

    queue.Enqueue((start, 0, pointsToClean.Aggregate("", (s, i) => $"{s}{i.Value}")));

    while (queue.Count > 0) {
        var (pos, count, toClean) = queue.Dequeue();

        if (pointsToClean.ContainsKey(pos)) {
            var idx = toClean.IndexOf(pointsToClean[pos]);
            if (idx > -1) toClean = toClean.Remove(idx, 1);
        }

        if (string.IsNullOrWhiteSpace(toClean)) {
            if (!returnToStart) return count;
            if (pos == start) return count;
        }

        foreach (var point in GetNextPoints()) {
            var next = (point, count + 1, toClean);
            if (!visited.Contains(next)) {
                visited.Add(next);
                queue.Enqueue(next);
            }
        }

        IEnumerable<Point> GetNextPoints() {
            foreach (var (dx, dy) in offsets) {
                var next = new Point(pos.X + dx, pos.Y + dy);
                if (freePoints!.Contains(next)) yield return next;
            }
        }
    }

    return -1;
}

void Parse() {
    var input = File.ReadAllLines("input.txt");
    var y = 0;
    foreach (var row in input) {
        if (string.IsNullOrWhiteSpace(row)) continue;
        for (var x = 0; x < row.Length; x++) {
            if (row[x] != '#') freePoints.Add(new Point(x, y));
            if (char.IsDigit(row[x])) pointsToClean.Add(new Point(x, y), row[x]);
        }

        y++;
    }
}

internal record Point(int X, int Y);