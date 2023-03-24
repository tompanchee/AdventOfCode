Console.WriteLine();

Console.WriteLine("Solving part 1...");

var computer = new Assembunny.Assembunny(File.ReadAllLines("input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray());
computer.SetA(7);
computer.Execute();

Console.WriteLine($"Safe key is {computer.A}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

// Reset the original program, no optimizations, brute force
computer = new Assembunny.Assembunny(File.ReadAllLines("input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray());
computer.SetA(12);
computer.Execute();

Console.WriteLine($"Safe key is {computer.A}");