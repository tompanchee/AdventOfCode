var blockedIps = GetInputData();

Console.WriteLine("Solving part 1...");
var ipToCheck = blockedIps.First().End + 1;
for (var i = 1; i < blockedIps.Length; i++) {
    if (blockedIps[i].End < ipToCheck) {
        continue;
    }

    if (blockedIps[i].Contains(ipToCheck)) {
        ipToCheck = blockedIps[i].End + 1;
        continue;
    }

    if (blockedIps[i].Start > ipToCheck) {
        break;
    }
}

Console.WriteLine($"Smallest allowed IP is {ipToCheck}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
long count = 0;
ipToCheck = blockedIps.First().End + 1;
for (var i = 1; i < blockedIps.Length; i++) {
    if (blockedIps[i].End < ipToCheck) {
        continue;
    }

    if (blockedIps[i].Contains(ipToCheck)) {
        ipToCheck = blockedIps[i].End + 1;
        continue;
    }

    if (blockedIps[i].Start > ipToCheck) {
        count += blockedIps[i].Start - ipToCheck;
        ipToCheck = blockedIps[i].End + 1;

        // Break if exceeded uint.MaxValue
        if (ipToCheck == 0) {
            break;
        }
    }
}

Console.WriteLine($"There are {count} allowed IPs");

Range[] GetInputData() {
    var result = new List<Range>();

    foreach (var row in File.ReadAllLines("input.txt")) {
        if (string.IsNullOrEmpty(row)) {
            continue;
        }

        var split = row.Split('-');
        result.Add(new Range(uint.Parse(split[0]), uint.Parse(split[1])));
    }

    return result.OrderBy(r => r.Start).ToArray();
}

internal class Range
{
    public Range(uint start, uint end) {
        Start = start;
        End = end;
    }

    public uint Start { get; }
    public uint End { get; }

    public bool Contains(uint value) {
        return value >= Start && value <= End;
    }
}