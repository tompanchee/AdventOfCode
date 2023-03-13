(int dx, int dy)[] offsets = {
    (-1, 0),
    (1, 0),
    (0, -1),
    (0, 1)
};

var input = int.Parse(File.ReadAllText("input.txt"));
var positionsAt50Steps = 0;

Console.WriteLine("Solving part 1...");
var length = GetPathLength((1, 1), (31, 39));
Console.WriteLine($"Path length from (1, 1) to (31, 39) is {length} steps");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
Console.WriteLine($"{positionsAt50Steps} positions visited in 50 steps");

int GetPathLength((int x, int y) start, (int x, int y) target) {
    var queue = new Queue<((int x, int y)pos, int count)>();
    var visited = new HashSet<(int, int)>();

    queue.Enqueue((start, 0));
    visited.Add(start);

    while (queue.Count > 0) {
        var (pos, count) = queue.Dequeue();

        if (count < 51) {
            positionsAt50Steps = visited.Count;
        }

        if (pos.x == target.x && pos.y == target.y) {
            return count;
        }

        foreach (var neighbour in GetNeighbours(pos.x, pos.y)) {
            if (!visited.Contains(neighbour)) {
                visited.Add(neighbour);
                queue.Enqueue((neighbour, count + 1));
            }
        }
    }

    return -1;
}

bool IsOpenSpace(int x, int y) {
    if (x < 0 || y < 0) {
        return false;
    }

    var value = x * x + 3 * x + 2 * x * y + y + y * y + input;
    var bin = Convert.ToString(value, 2);
    return bin.Count(c => c == '1') % 2 == 0;
}

(int x, int y)[] GetNeighbours(int x, int y) {
    List<(int x, int y)> result = new();

    foreach (var (dx, dy) in offsets) {
        var nx = x + dx;
        var ny = y + dy;

        if (IsOpenSpace(nx, ny)) {
            result.Add((nx, ny));
        }
    }

    return result.ToArray();
}