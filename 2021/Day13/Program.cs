var input = File.ReadAllLines("input.txt");

HashSet <(int x, int y)> paper = new HashSet<(int x, int y)>();
IList<(string axis, int value)> instructions = new List<(string axis, int value)>();
Parse(input, paper, instructions);

Console.WriteLine("Solving puzzle 1...");
paper = Fold(paper, instructions[0]);
Console.WriteLine($"After first fold there are {paper.Count} visible dots");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
// First fold done in puzzle 1
for(var i = 1; i < instructions.Count; i++){
    paper = Fold(paper, instructions[i]);
}
Print(paper);

HashSet<(int x,int y)> Fold(HashSet<(int x, int y)> paper, (string axis, int value) fold) {
    if (fold.axis == "x") return FoldX(paper, fold.value);
    return FoldY(paper, fold.value);
}

HashSet<(int x, int y)> FoldX(HashSet<(int x, int y)> paper, int value) {
    var p1 = new HashSet<(int x, int y)>();
    var p2 = new HashSet<(int x, int y)>();

    foreach (var point in paper) {
        if (point.x < value) p1.Add(point);
        else p2.Add(point);
    }

    // Fold vertically
    var p3 = new HashSet<(int x, int y)>();
    foreach(var point in p2) {
        p3.Add((2 * value - point.x, point.y));
    }

    return new HashSet<(int x, int y)>(p1.Union(p3));
}

HashSet<(int x, int y)> FoldY(HashSet<(int x, int y)> paper, int value) {    
    var p1 = new HashSet<(int x, int y)>();
    var p2 = new HashSet<(int x, int y)>();

    foreach (var point in paper) {
        if (point.y < value) p1.Add(point);
        else p2.Add(point);
    }

    // Fold horizontally
    var p3 = new HashSet<(int x, int y)>();
    foreach (var point in p2) {
        p3.Add((point.x, 2* value - point.y));
    }

    return new HashSet<(int x, int y)>(p1.Union(p3));    
}

void Print(HashSet<(int x, int y)> paper) {
    var maxY = paper.Select(p => p.y).Max();

    for (var r = 0; r <= maxY; r++) { 
        var row = paper.Where(p => p.y == r).Select(p => p.x).ToArray();
        var maxX = row.Max();
        for (var c = 0; c <= maxX; c++) {
            if (row.Contains(c)) Console.Write("#");
            else Console.Write(" ");
        }
        Console.WriteLine();
    }
}

void Parse(string[] input, HashSet<(int x, int y)> paper, IList<(string axis, int value)> instructions) {
    int row = 0;
    do {
        var split = input[row].Split(',');
        (int x, int y) = (int.Parse(split[0]), int.Parse(split[1]));
        paper.Add((x, y));
        row++;
    } while (input[row] != string.Empty);
    row++; // Skip empty line

    for (; row < input.Length; row++) {
        var split = input[row]["fold along ".Length..].Split('=', StringSplitOptions.TrimEntries);
        instructions.Add((split[0], int.Parse(split[1])));
    }
}