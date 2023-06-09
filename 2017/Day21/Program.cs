using Day21;

Console.WriteLine("Parsing rules...");

var data = File.ReadAllLines("input.txt");

// Sample data
//data = new[] {
//    "../.# => ##./#../...",
//    ".#./..#/### => #..#/..../..../#..#"
//};

var rules = (from line in data
        where !string.IsNullOrWhiteSpace(line)
        select Rule.FromRuleBook(line))
    .ToList();

Console.WriteLine();
Console.WriteLine("Solving part 1...");

var input = new Grid(".#.", "..#", "###");

for (var i = 0; i < 5; i++) input = Enhancer.Enhance(input, rules);

var count = input.Rows.Sum(r => r.Count(c => c == '#'));
Console.WriteLine($"After 5 iterations there are {count} pixels on");

Console.WriteLine();
Console.WriteLine("Solving part 2...");
input = new Grid(".#.", "..#", "###");
for (var i = 0; i < 18; i++) input = Enhancer.Enhance(input, rules);

count = input.Rows.Sum(r => r.Count(c => c == '#'));
Console.WriteLine($"After 18 iterations there are {count} pixels on");