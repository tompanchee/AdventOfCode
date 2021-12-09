var input = File.ReadAllLines("input.txt");

Console.WriteLine("Solving puzzle 1...");

var sum = 0;
for(var row = 0; row < input.Length; row++) {
    for (var col = 0; col < input[row].Length; col++) {
        if (IsLowPoint(input, row, col)) {
            sum += (1 + int.Parse(input[row][col].ToString()));
        }
    }
}

Console.WriteLine($"Sum of risk levels is {sum}");

bool IsLowPoint(string[] input, int row, int col) {
    var offsets = new (int r, int c)[] {    
        (-1, 0),
        (1, 0),
        (0, -1),
        (0, 1)
    };

    var value = input[row][col];

    foreach(var offset in offsets) {
        var r = row + offset.r;
        var c = col + offset.c;

        if (r < 0 || r > input.Length - 1) continue;
        if (c < 0 || c > input[r].Length - 1) continue;

        if (input[r][c] < value) return false;
    }

    return true;
}