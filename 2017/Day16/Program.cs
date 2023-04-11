var input = File.ReadAllLines("input.txt").First();
var moves = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

Console.WriteLine("Solving part 1...");

var programs = moves.Aggregate("abcdefghijklmnop", Move);

Console.WriteLine($"Programs after dancing is {programs}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
// Look for sequences while dancing
programs = "abcdefghijklmnop";

var statuses = new List<string>();
int start;
int end;
var count = 0;

while (true) {
    programs = moves.Aggregate(programs, Move);

    if (statuses.Contains(programs)) {
        var statusIdx = statuses.IndexOf(programs);
        start = statusIdx;
        end = count;
        break;
    }

    count++;
    statuses.Add(programs);
}

var wantedIndex = (1_000_000_000 - start) % (end - start) + start - 1;

Console.WriteLine($"After 1000000000 moves the programs is {statuses[wantedIndex]}");

string Move(string value, string instruction) {
    return instruction[0] switch {
        's' => Spin(value),
        'x' => Exchange(value),
        'p' => Partner(value),
        _ => throw new InvalidOperationException()
    };

    string Spin(string s) {
        var spin = int.Parse(instruction[1..]);
        var e = s[^spin..];
        return $"{e}{s[..^spin]}";
    }

    string Exchange(string s) {
        var split = instruction[1..].Split('/');
        var p1 = int.Parse(split[0]);
        var p2 = int.Parse(split[1]);
        var a = s.ToCharArray();
        (a[p1], a[p2]) = (a[p2], a[p1]);
        return new string(a);
    }

    string Partner(string s) {
        var split = instruction[1..].Split('/');
        var p1 = s.IndexOf(split[0], StringComparison.Ordinal);
        var p2 = s.IndexOf(split[1], StringComparison.Ordinal);
        var a = s.ToCharArray();
        (a[p1], a[p2]) = (a[p2], a[p1]);
        return new string(a);
    }
}