var input = File.ReadAllText("input.txt");

Console.WriteLine("Solving puzzle 1...");
// Brute force (keep all the fish in the array)
var fish = input.Split(',').Select(int.Parse).ToList();
var day = 0;

while(day < 80) {
    day++;
    var nextGeneration = new List<int>();
    var fishToAdd = 0;
    foreach (var f in fish) {
        var next = f - 1;
        nextGeneration.Add(next == -1 ? 6 : next);
        if (next == -1) fishToAdd++;
    }

    for (var i = 0; i < fishToAdd; i++) {
        nextGeneration.Add(8);
    }

    fish = nextGeneration;
}

Console.WriteLine($"After 80 days there are {fish.Count} fish");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
// Keep track on number of fish creating a new fish in amount of days (days, number of fish)
fish = input.Split(',').Select(int.Parse).ToList();
day = 0;

var createNewInDays = new Dictionary<int, long>();
var days = fish.Max();
while (days > 0) {
    createNewInDays.Add(days, fish.Count(f => f == days));
    days--;
}

while(day < 256) {
    day++;

    var next = new Dictionary<int, long>();
    var max = createNewInDays.Keys.Max();
    for (var i = 0; i <= max; i++) {
        if (!createNewInDays.ContainsKey(i)) continue;
        if (i == 0) {            
            var createNew = createNewInDays[0];
            next.Add(6, createNew);
            next.Add(8, createNew);
        } else {
            if (next.ContainsKey(i - 1)) next[i - 1] = next[i - 1] + createNewInDays[i];
            else next.Add(i - 1, createNewInDays[i]);
        }
    }

    createNewInDays = next;
}

Console.WriteLine($"After 256 days there are {createNewInDays.Values.Sum()} fish");