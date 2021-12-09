var input = File.ReadAllLines("input.txt");
var data = ParseInput(input);

Console.WriteLine("Solving puzzle 1...");
int[] uniqueLengths = {2,3,4,7};
var countOfUniques = 0;
foreach(var r in data.Select(d=>d.results)) {
    countOfUniques += r.Where(p=>uniqueLengths.Contains(p.Length)).Count();
}
Console.WriteLine($"Number of unique digits in results is {countOfUniques}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
var sum = 0;
foreach (var row in data) {
    var zero = string.Empty;
    var one = string.Empty; // length: 2
    var two = string.Empty;
    var three = string.Empty;
    var four = string.Empty; // 4
    var five = string.Empty;
    var six = string.Empty;
    var seven = string.Empty; // 3
    var eight = string.Empty; // 7
    var nine = string.Empty;
    var twoThreeFive = new List<string>(); // 5
    var zeroSixNine = new List<string>(); // 6

    foreach (var signal in row.signals.Select(s => string.Concat(s.OrderBy(c => c)))) {
        switch (signal.Length)
        {
            case 2:
                one = signal;
                break;
            case 3:
                seven = signal;
                break;
            case 4:
                four = signal;
                break;
            case 5:
                twoThreeFive.Add(signal);
                break;
            case 6:
                zeroSixNine.Add(signal);
                break;
            case 7:
                eight = signal;
                break;
            default:
                throw new InvalidDataException("Unknown signal combination");
        }
    }

    six = zeroSixNine.Single(zsn => zsn.Intersect(one).Count() != one.Length);
    zeroSixNine.Remove(six);

    three = twoThreeFive.Single(ttf => ttf.Intersect(one).Count() == one.Length);
    twoThreeFive.Remove(three);

    nine = zeroSixNine.Single(zsn => zsn.Intersect(three).Count() == three.Length);
    zeroSixNine.Remove(nine);

    zero = zeroSixNine.Single();

    if (twoThreeFive[0].Intersect(four).Count() == 2) {
        two = twoThreeFive[0];
        five = twoThreeFive[1];
    } else {
        two = twoThreeFive[1];
        five = twoThreeFive[0];
    }

    var map = new List<string> { zero, one, two, three, four, five, six, seven, eight, nine };

    var value = 0;
    for (var i = 0; i < 4; i++) {
        var number = string.Concat(row.results[i].OrderBy(c => c));
        value += (int)Math.Pow(10, 3 - i) * map.IndexOf(number);
    }

    sum += value;
}
Console.WriteLine($"Sum of ouput values {sum}");


Data[] ParseInput(string[] input) {
    var result = new List<Data>();

    foreach(var line in input) {
        var split = line.Split('|', StringSplitOptions.TrimEntries);
        result.Add(new Data(split[0].Split(' '), split[1].Split(' ')));
    }

    return result.ToArray();
}

readonly record struct Data(string[] signals, string[] results);