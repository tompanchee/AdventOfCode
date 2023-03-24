Console.WriteLine("Reading program...");
var program = File.ReadAllLines("input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

Console.WriteLine();

Console.WriteLine("Solving part 1...");
var computer = new Assembunny.Assembunny(program);
computer.Execute();
Console.WriteLine($"Register A after execution is {computer.A}");

Console.WriteLine();

Console.WriteLine("Solving part 1...");
computer = new Assembunny.Assembunny(program);
computer.SetC(1);
computer.Execute();
Console.WriteLine($"Register A after execution is {computer.A}");