var input = File.ReadAllLines("input.txt");

var singleLowPoints = new List<(int row, int col)>();

Console.WriteLine("Solving puzzle 1...");

var sum = 0;
for(var row = 0; row < input.Length; row++) {
    for (var col = 0; col < input[row].Length; col++) {
        if (IsLowPoint(input, row, col)) {
            singleLowPoints.Add((row, col)); // Collect low points (for puzzle 2)
            sum += 1 + GetPointValue(input, (row, col));
        }
    }
}

Console.WriteLine($"Sum of risk levels is {sum}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
var maxBasins = new List<int> { int.MinValue, int.MinValue, int.MinValue };

foreach(var lowPoint in singleLowPoints) {
    var size = CalculateBasinSize(input, lowPoint);
    var minSize = maxBasins.Min();
    if (size > minSize) {
        maxBasins.Remove(minSize);
        maxBasins.Add(size);
    }
}
Console.WriteLine($"Largest basin sizes multiplied is {maxBasins[0] * maxBasins[1] * maxBasins[2]}");

bool IsLowPoint(string[] input, int row, int col) {
    var value = input[row][col];

    foreach(var offset in Helper.Offsets) {
        var r = row + offset.r;
        var c = col + offset.c;

        if (r < 0 || r > input.Length - 1) continue;
        if (c < 0 || c > input[r].Length - 1) continue;

        if (input[r][c] <= value) return false;
    }

    return true;
}

int CalculateBasinSize(string[] input, (int row, int col) lowPoint) {
    var basinPoints = GetBasinPoints(input, lowPoint);
    return basinPoints.Length;
}

(int row, int col)[] GetBasinPoints(string[] input, (int row, int col) lowPoint, HashSet<(int, int)> visitedPoints = null) {
    if (visitedPoints == null) visitedPoints = new HashSet<(int, int)>();
    visitedPoints.Add(lowPoint);
    
    var currentValue = GetPointValue(input, lowPoint);
    foreach (var (r, c) in Helper.Offsets) {
        (int row, int col) pointToCheck = (lowPoint.row + r, lowPoint.col + c);
        if (pointToCheck.row < 0 || pointToCheck.row > input.Length - 1) continue;
        if (pointToCheck.col < 0 || pointToCheck.col > input[pointToCheck.row].Length - 1) continue;
        if (visitedPoints.Contains(pointToCheck)) continue;

        var value = GetPointValue(input, pointToCheck);
        if (value == 9 || value < currentValue) continue;

        _ = GetBasinPoints(input, pointToCheck, visitedPoints);
    }

    return visitedPoints.ToArray();
}

int GetPointValue(string[] input, (int r, int c) point) => int.Parse(input[point.r][point.c].ToString());

static class Helper
{
    public static (int r, int c)[] Offsets => new (int r, int c)[] {
        (-1, 0),
        (1, 0),
        (0, -1),
        (0, 1)
    };
}