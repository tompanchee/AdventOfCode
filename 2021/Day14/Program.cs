using System.Text;

var input = File.ReadAllLines("input.txt");
var (template,  rules) = Parse(input);

Console.WriteLine("Solving puzzle 1...");
// Brute force, keep complete polymer in memory
var polymer = template;
for(var i=0; i<10;i++) {
    polymer = Polymerize(polymer, rules);
}

var minCount = int.MaxValue;
var maxCount = int.MinValue;
var elements = new string(polymer.Distinct().ToArray());
foreach (var ch in elements) {
    var count = polymer.Count(c => c == ch);
    if (count > maxCount) maxCount = count;
    if (count < minCount) minCount = count;
}

Console.WriteLine($"Difference between min and max is {maxCount - minCount}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
// Setup pair counts
var pairCount = new Dictionary<string, long>();
for (var i = 0; i < template.Length - 1; i++) { 
    var pair = template[i..(i+2)];
    if (!pairCount.ContainsKey(pair)) pairCount.Add(pair, 0L);
    pairCount[pair]++;
}

// Run 40 polymerization rounds
// Keep track of pair amount
for(var i = 0; i < 40; i++) {
    var newPairCount = new Dictionary<string, long>();
    foreach (var pair in pairCount) { 
        var insertion = rules[pair.Key];
        
        var key = pair.Key[..1] + insertion;
        if (!newPairCount.ContainsKey(key)) newPairCount.Add(key, 0L);
        newPairCount[key] += pair.Value;

        key = insertion + pair.Key[1..];
        if (!newPairCount.ContainsKey(key)) newPairCount.Add(key, 0L);
        newPairCount[key] += pair.Value;
    }
    pairCount = newPairCount;
}

// Setup element counts
var elementCounts = new Dictionary<char, long>();
foreach (var pair in pairCount) {
    foreach (var element in pair.Key) {
        if (!elementCounts.ContainsKey(element)) elementCounts.Add(element, 0L);
    }
}

// Calculate elements
// Choose max(element as first in pair, element as second in pair)
// Count is sum of chosen pair counts
foreach(var element in elementCounts.Keys) {
    var asFirst = pairCount.Where(p => p.Key[0] == element);
    var asSecond = pairCount.Where(p => p.Key[1] == element);

    var toCount = asFirst.Count() > asSecond.Count() ? asFirst : asSecond;
    elementCounts[element] = toCount.Select(p => p.Value).Sum();
}

Console.WriteLine($"Difference between min and max is {elementCounts.Values.Max() - elementCounts.Values.Min()}");

// Used by puzzle 1
string Polymerize(string polymer, IDictionary<string, string> rules) {
    var newPolymer = new StringBuilder();
    for(var i = 0; i< polymer.Length-1; i++) {
        var key = polymer[i..(i+2)];
        if(rules.ContainsKey(key)) {
            var insertion = rules[key];
            newPolymer.Append(polymer[i]);
            newPolymer.Append(insertion);            
        }
    }
    newPolymer.Append(polymer.Last());

    return newPolymer.ToString();
}

(string, IDictionary<string, string>) Parse(string[] input) {
    string template = string.Empty;
    var rules = new Dictionary<string, string>();

    foreach (var row in input) {
        if (string.IsNullOrWhiteSpace(row)) continue;
        if (template == string.Empty) {
            template = row;
            continue;
        }

        var split = row.Split("->", StringSplitOptions.TrimEntries);
        rules.Add(split[0], split[1]);
    }

    return (template, rules);
}