using Day11;

var input = File.ReadAllLines("input.txt")[0];
var data = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

Console.Write("Solving part 1...");

var child = new Hex(0, 0);
foreach (var direction in data) child.Move(direction);

Console.WriteLine($"Distance to child process is {child.DistanceTo(new Hex(0, 0))}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
var maxDistance = int.MinValue;

child = new Hex(0, 0);
foreach (var direction in data) { 
    var d = child.DistanceTo(new Hex(0, 0));
    if (d > maxDistance) maxDistance = d;
    child.Move(direction); 
}

Console.WriteLine($"Max distance to child process was {maxDistance}");
