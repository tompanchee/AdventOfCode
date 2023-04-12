using Day18;

var input = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToArray();

Console.WriteLine("Solving puzzle 1...");

var duet = new Duet(input);
duet.Execute(() => true);

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");

var d0 = new Duet(input, 0, true);
var d1 = new Duet(input, 1, true);

d0.SetSender(d1);
d1.SetSender(d0);

var tasks = new[] { Task.Run(() => d0.Execute()), Task.Run(() => d1.Execute()) };

Task.WaitAll(tasks);

Console.WriteLine($"Program 1 sent {d1.SendCount} times before a deadlock");