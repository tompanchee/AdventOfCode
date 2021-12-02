var input = ParseInput();

Console.WriteLine("Solving puzzle 1...");
(int x, int depth) position = (0, 0);

// Traverse
foreach(var command in input) {
    switch (command.direction) {
        case "forward":
            position.x += command.amount;
            break;
        
        case "down":
            position.depth += command.amount;
            break;

        case "up":
            position.depth -= command.amount;
            break;

        default:
            throw new InvalidOperationException($"Invalid command {command.direction}");
    }
}

Console.WriteLine($"Product of current position {position.x * position.depth}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
position = (0, 0);
var aim = 0;

// Traverse
foreach(var command in input) {
    switch (command.direction) {
        case "forward":
            position.x += command.amount;
            position.depth += aim * command.amount;
            break;
        
        case "down":
            aim += command.amount;
            break;

        case "up":
            aim -= command.amount;
            break;

        default:
            throw new InvalidOperationException($"Invalid command {command.direction}");
    }
}

Console.WriteLine($"Product of current position {position.x * position.depth}");


List<(string direction, int amount)> ParseInput() {
    var data = File.ReadAllLines("input.txt");
    var result = new List<(string, int)>();
    foreach(var line in data) {
        var split = line.Split(' ');
        result.Add((split[0], int.Parse(split[1])));
    }

    return result;
}

