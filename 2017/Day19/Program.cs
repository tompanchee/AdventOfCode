using System.Text;

var diagram = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToArray();

Console.WriteLine("Solving puzzles 1 and 2...");

// Starting point
(int x, int y) pos = (diagram[0].IndexOf('|'), 0);
var dir = Direction.Down;
var sb = new StringBuilder();
var count = 1;

while (true) {
    if (char.IsLetter(diagram[pos.y][pos.x])) sb.Append(diagram[pos.y][pos.x]);
    var next = Move();
    if (next == null) break;
    pos = next.Value;
    count++;
}

Console.WriteLine($"Part 1: The packet sees letters {sb} on its way.");
Console.WriteLine($"Part 2: The packet goes {count} steps on its way.");

(int x, int y)? Move() {
    var next = NextPosition();

    if (next != null) return next;
    if (dir is Direction.Down or Direction.Up) {
        // Try left or right
        dir = Direction.Left;
        next = NextPosition();
        if (next != null) return next;

        dir = Direction.Right;
        next = NextPosition();
        if (next != null) return next;
    } else {
        // Try up or down
        dir = Direction.Up;
        next = NextPosition();
        if (next != null) return next;

        dir = Direction.Down;
        next = NextPosition();
        if (next != null) return next;
    }

    return null;
}

(int x, int y)? NextPosition() {
    var next = dir switch {
        Direction.Down => (pos.x, pos.y + 1),
        Direction.Left => (pos.x - 1, pos.y),
        Direction.Up => (pos.x, pos.y - 1),
        Direction.Right => (pos.x + 1, pos.y),
        _ => throw new ArgumentOutOfRangeException()
    };

    if (IsAllowed(next)) return next;
    return null;
}

bool IsAllowed((int x, int y) position) {
    if (position.x < 0 || position.x >= diagram![0].Length || position.y < 0 || position.y >= diagram.Length) return false;
    return diagram[position.y][position.x] != ' ';
}

internal enum Direction
{
    Down,
    Left,
    Up,
    Right
}