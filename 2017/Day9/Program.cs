using Day9;

var stream = File.ReadAllLines("input.txt")[0];
var util = new StreamUtil(stream);

Console.WriteLine("Solving part 1...");
Console.WriteLine($"Stream total score is {util.CalculateScore()}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
Console.WriteLine($"Stream total amount of garbage is {util.CountGarbage()}");