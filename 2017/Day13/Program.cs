IDictionary<int, int> scanners = new Dictionary<int, int>();

Console.WriteLine("Scanning scanners...");
var input = File.ReadAllLines("input.txt");

foreach (var line in input)
{
    if (string.IsNullOrWhiteSpace(line)) continue;

    var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    scanners.Add(int.Parse(split[0]), int.Parse(split[1])); 
}

Console.WriteLine("Solving part 1...");
var severity = 0;
var t = 0;

while(t <= scanners.Keys.Max())
{
    if (scanners.ContainsKey(t))
    {
        var isAtZero = t % ((scanners[t] - 1) * 2) == 0;
        if (isAtZero) severity += scanners[t] * t;
    }

    t++;
}

Console.WriteLine($"Total severity is {severity}.");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

// Brute force
var delay = 0;
while(true)
{
    var caught = false;

    t = 0;
    while (t <= scanners.Keys.Max())
    {
        if (scanners.ContainsKey(t))
        {
            var isAtZero = (t + delay) % ((scanners[t] - 1) * 2) == 0;
            if (isAtZero)
            {
                caught = true;
                break;
            }
        }

        t++;
    }

    if (!caught) break;
    delay++;
}

Console.WriteLine($"Package needs to be delayed {delay} ps.");
