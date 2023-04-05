using KnotHash;
using System.Text;

var input = "vbqugkhl";
var hashes = new List<string>();

(int dx, int dy)[] offsets = new[]
{
    (-1, 0),
    (1, 0),
    (0, -1),
    (0, 1)
};

Console.WriteLine("Solving part 1...");

for (var i=0; i < 128; i++)
{
    var hasher = Hasher.Init();
    var key = Encoding.ASCII.GetBytes($"{input}-{i}")
        .Select(Convert.ToInt32)
        .ToList();
    key.AddRange(new[] { 17, 31, 73, 47, 23 });

    hasher.Scramble(key.ToArray(), 64);
    var hash = hasher.CalculateHash();

    hashes.Add(GetHashAsBinaryString(hash));
}

var count = hashes.Sum(h => h.Count(c => c == '1'));

Console.WriteLine($"There are {count} squares used");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

var maxx = hashes[0].Length - 1;
var maxy = hashes.Count - 1;

var groupCount = 0;
var visited = new HashSet<(int x, int y)>();

for(var y = 0; y <= maxy; y++)
{
    for(var x =0; x <= maxx; x++)
    {
        if (hashes[y][x] == '0' || visited.Contains((x, y))) continue;

        var queue = new Queue<(int x, int y)>();
        queue.Enqueue((x, y));

        while(queue.Count > 0)
        {
            var current = queue.Dequeue();

            visited.Add(current);
            foreach (var n in GetNeighbours(current.x, current.y)) queue.Enqueue(n);
        }

        groupCount++;
    }
}

Console.WriteLine($"There are {groupCount} regions on the disk.");

IEnumerable<(int x, int y)> GetNeighbours(int x, int y)
{    
    foreach (var (dx, dy) in offsets)
    {
        var nx = x + dx;
        var ny = y + dy;
        if (nx < 0 || nx > maxx || ny < 0 || ny > maxy) continue;
        if (visited.Contains((nx, ny))) continue;
        if (hashes[ny][nx] == '0') continue;

       yield return (nx, ny);
    }
}

static string GetHashAsBinaryString(string hash)
{
    return  string.Join(string.Empty,
        hash.Select(
            c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
        )
    );
}