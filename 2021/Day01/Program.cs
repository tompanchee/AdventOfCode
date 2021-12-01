var input = File.ReadAllLines("input.txt").Select(l=>int.Parse(l)).ToArray();

Console.WriteLine("Solving puzzle 1...");

var count = 0;
for(var i=1; i < input.Length; i++) {
    if (input[i] > input[i-1]) count++;
}
Console.WriteLine($"Depth increases {count} times");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
count = 0;
for(var i=0; i < input.Length-3; i++) {
    var current = input.Skip(i).Take(3).Sum();
    var next = input.Skip(i+1).Take(3).Sum();

    if (next > current) count++;
}
Console.WriteLine($"Depth increases {count} times");