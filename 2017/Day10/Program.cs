using System.Text;
using Day10;

var data = File.ReadAllLines("input.txt")[0];
var input = data
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    .Select(int.Parse)
    .ToArray();

Console.WriteLine("Solving part 1...");

var hasher = KnotHasher.Init();
hasher.Scramble(input);

Console.WriteLine($"Product of first two numbers is {hasher[0] * hasher[1]}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

var lengths = Encoding.ASCII.GetBytes(data)
    .Select(Convert.ToInt32)
    .ToList();
lengths.AddRange(new[] { 17, 31, 73, 47, 23 });

hasher = KnotHasher.Init(); // reset hasher
hasher.Scramble(lengths.ToArray(), 64);
var hash = hasher.CalculateHash();

Console.WriteLine($"The knot hash is {hash}");