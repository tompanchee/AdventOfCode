var input = File.ReadAllLines("input.txt");

Console.WriteLine("Solving puzzle 1...");
var sum = 0;
foreach (var line in input) {
    var result = Validate(line);
    if (result == null) continue;
    sum += Helpers.Points[result.Value];
}

Console.WriteLine($"Total syntax error score is {sum}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
var scores = new List<long>();
foreach (var line in input) { 
    if (!IsValidLine(line)) continue;
    var orphan = GetAutoCompleteOrphans(line);
    var score = orphan.Aggregate(0L, (s,c) => s*5 + Helpers.AutoCompletePoints[c]);
    scores.Add(score);
}
var middleScore = scores.OrderBy(i => i).ToArray()[scores.Count / 2];
Console.WriteLine($"Middle score is {middleScore}");

static char? Validate(string line) {
    var stack = new Stack<char>();
    foreach (var c in line) {
        if (Helpers.End.Contains(c)) {
            var peek = stack.Peek();
            if (Helpers.MatchingChunk[peek] != c) return c;
            _ = stack.Pop();
            continue;
        }
        stack.Push(c);
    }

    return null;
}

static bool IsValidLine(string line) => Validate(line) == null;

static char[] GetAutoCompleteOrphans(string line) {
    var stack = new Stack<char>();
    foreach(var c in line) {
        if (Helpers.End.Contains(c)) _ = stack.Pop();
        else stack.Push(c);
    }
    return stack.ToArray();
}

static class Helpers
{
    public static char[] End => new[] { ')', ']', '}', '>' };
    public static IDictionary<char, int> Points => new Dictionary<char, int> {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
    };
    public static IDictionary<char, char> MatchingChunk => new Dictionary<char, char> {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
        { '<', '>' }
    };
    public static IDictionary<char, long> AutoCompletePoints => new Dictionary<char, long> {
        { '(', 1L },
        { '[', 2L },
        { '{', 3L },
        { '<', 4L }
    };
}