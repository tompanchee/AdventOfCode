using System.Text;

var input = File.ReadAllLines("input.txt").First();

Console.WriteLine("Solving part 1...");
var floor = new List<string> {input};
var current = input;
while (floor.Count < 40) {
    var @new = GetNextRow(current);
    floor.Add(@new);
    current = @new;
}

Console.WriteLine($"There are total {floor.Sum(f => f.Count(c => c == '.'))} safe tiles.");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
var sum = input.Count(c => c == '.');
var iteration = 0;
current = input;
while (++iteration < 400000) {
    var @new = GetNextRow(current);
    sum += @new.Count(c => c == '.');
    current = @new;
}

Console.WriteLine($"There are total {sum} safe tiles.");

string GetNextRow(string row) {
    var next = new StringBuilder();
    for (var i = 0; i < row.Length; i++) {
        var left = i == 0 ? '.' : row[i - 1];
        var right = i == row.Length - 1 ? '.' : row[i + 1];

        if ((left == '^' || right == '^') && !(left == '^' && right == '^')) {
            next.Append('^');
        } else {
            next.Append('.');
        }
    }

    return next.ToString();
}