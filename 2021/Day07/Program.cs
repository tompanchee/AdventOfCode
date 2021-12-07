var input = File.ReadAllText("input.txt");

var crabs = input.Split(',').Select(int.Parse).ToArray();

Console.WriteLine("Solving puzzle 1...");
var minFuel = int.MaxValue;

for(var pos = crabs.Min(); pos <= crabs.Max(); pos++) {
    var fuel = 0;
    foreach(var crab in crabs) {
        fuel += Math.Abs(crab -pos);
        if (fuel > minFuel) break;
    }
    if (fuel < minFuel) minFuel = fuel;
}

Console.WriteLine($"Minimum fuel to get aligned is {minFuel}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
minFuel = int.MaxValue;

for(var pos = crabs.Min(); pos <= crabs.Max(); pos++) {
    var fuel = 0;
    foreach(var crab in crabs) {
        fuel += Enumerable.Range(1, Math.Abs(crab -pos)).Sum();        
        if (fuel > minFuel) break;
    }
    if (fuel < minFuel) minFuel = fuel;
}

Console.WriteLine($"Minimum fuel to get aligned is {minFuel}");