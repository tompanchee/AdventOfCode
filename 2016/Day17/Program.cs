using System.Security.Cryptography;
using System.Text;

const string OPEN = "bcdef";

var input = File.ReadAllText("input.txt");
var maxPathLength = -1;

Console.WriteLine("Solving part 1...");
var path = FindPath((0, 0), (3, 3), input);
Console.WriteLine($"Shortest path to the vault is {path}");

Console.WriteLine();
Console.WriteLine("Solving part 2...");
_ = FindPath((0, 0), (3, 3), input, false);
Console.WriteLine($"Longest path to the vault is {maxPathLength} steps long");

string FindPath((int, int) start, (int x, int y) target, string code, bool findShortestPath = true) {
    var queue = new Queue<((int x, int y)pos, string code)>();

    queue.Enqueue((start, code));

    while (queue.Count > 0) {
        var current = queue.Dequeue();

        if (current.pos.x == target.x && current.pos.y == target.y) {
            var p = current.code[input!.Length..];
            if (findShortestPath) {
                return p;
            }

            if (p.Length > maxPathLength) {
                maxPathLength = p.Length;
            }

            continue;
        }

        foreach (var nextState in GetNextStates(current.pos, current.code)) queue.Enqueue(nextState);
    }

    return "";
}

IEnumerable<((int x, int y), string code)> GetNextStates((int x, int y) position, string code) {
    var hash = CreateHash(code);
    var result = new List<((int x, int y), string code)>();

    if (IsOpen(hash[0]) && position.y > 0) {
        result.Add(((position.x, position.y - 1), code + "U"));
    }

    if (IsOpen(hash[1]) && position.y < 3) {
        result.Add(((position.x, position.y + 1), code + "D"));
    }

    if (IsOpen(hash[2]) && position.x > 0) {
        result.Add(((position.x - 1, position.y), code + "L"));
    }

    if (IsOpen(hash[3]) && position.x < 3) {
        result.Add(((position.x + 1, position.y), code + "R"));
    }

    return result.ToArray();

    bool IsOpen(char c) {
        return OPEN.Contains(c);
    }
}

string CreateHash(string value) {
    using var md5 = MD5.Create();
    var bytes = Encoding.ASCII.GetBytes(value);
    var hash = md5.ComputeHash(bytes);
    return Convert.ToHexString(hash).ToLower();
}