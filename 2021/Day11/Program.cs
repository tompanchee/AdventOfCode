var input = File.ReadAllLines("input.txt");
var octopuses = Parse(input);

Console.WriteLine("Solving puzzle 1...");
var sum = 0;
for(var i = 0; i < 100; i++){
    Step(octopuses);
    sum += octopuses.Cast<int>().Count(e => e == 0);
}
Console.WriteLine($"After 100 steps there has been {sum} flashes");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
octopuses = Parse(input); // Reset input
var step = 0;
while(!octopuses.Cast<int>().All(e => e == 0)) {
    step++;
    Step(octopuses);
}
Console.WriteLine($"First step when all octopuses flash is {step}");

static void Step(int[,] octopuses){
    var height = octopuses.GetLength(0);
    var width = octopuses.GetLength(1);

    var flashing = new HashSet<(int r, int c)>();
    
    // Increment all by one
    for(var i = 0; i < height; i++) {
        for (var j = 0; j < width; j++) {
            octopuses[i, j]++;
        }
    }

    // Spread energy while there are octopuses with enough energy
    while (octopuses.Cast<int>().Any(e=>e>9)) {
        for (var i = 0; i < height; i++) {
            for (var j = 0; j < width; j++) {
                if (octopuses[i, j] > 9 && !flashing.Contains((i, j))) {
                    flashing.Add((i, j)); // Add to flashing
                    foreach(var (r, c) in Helpers.Neighbours) {
                        var nr = i + r;
                        var nc = j + c;
                        if (nr < 0 || nr > height - 1) continue;
                        if (nc < 0 || nc > height - 1) continue;
                        octopuses[nr, nc]++;
                    }
                }
            }
        }

        foreach(var (r, c) in flashing) octopuses[r, c] = 0; // Set flashed octopuses to zero
    }
}

static int[,] Parse(string[] input) {
    var height = input.Length;
    var width = input[0].Length;
    var result = new int[height, width];
    for (var i = 0; i < height; i++) { 
        for(var j = 0; j < width; j++) {
            result[i, j] = int.Parse(input[i][j].ToString());
        }  
    }

    return result;
}

static class Helpers
{
    public static (int r, int c)[] Neighbours => new (int, int)[] {
        (-1, -1),
        (-1, 0),
        (-1, 1),
        (0, -1),
        (0, 1),
        (1, -1),
        (1, 0),
        (1, 1)
    };
}