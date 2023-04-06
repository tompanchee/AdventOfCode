var input = File.ReadAllLines("input.txt");
var seedA = long.Parse(input[0][24..]);
var seedB = long.Parse(input[1][24..]);

// Sample seeds
//seedA = 65;
//seedB = 8921;

var genA = new Generator(16807, seedA);
var genB = new Generator(48271, seedB);

const int LOW16_BIT_MASK = 65535;

Console.WriteLine("Solving part 1...");

var count = 0;

for (var i = 0; i < 40_000_000; i++) {
    var nextA = genA.Next();
    var nextB = genB.Next();

    if ((nextA & LOW16_BIT_MASK) == (nextB & LOW16_BIT_MASK)) count++;
}

Console.WriteLine($"There are {count} pairs with same low 16 bits");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

var genA2 = new Generator2(16807, 4, seedA);
var genB2 = new Generator2(48271, 8, seedB);

count = 0;

for (var i = 0; i < 5_000_000; i++) {
    var nextA = genA2.Next();
    var nextB = genB2.Next();

    if ((nextA & LOW16_BIT_MASK) == (nextB & LOW16_BIT_MASK)) count++;
}

Console.WriteLine($"There are {count} pairs with same low 16 bits");

internal class Generator
{
    readonly long factor;
    long current;

    public Generator(long factor, long current) {
        this.factor = factor;
        this.current = current;
    }

    public long Next() {
        var next = current * factor;
        next %= 2147483647;
        current = next;

        return current;
    }
}

internal class Generator2
{
    readonly long factor;
    readonly long multipleMask;
    long current;

    public Generator2(long factor, long multiple, long current) {
        this.factor = factor;
        this.current = current;
        multipleMask = multiple - 1;
    }

    public long Next() {
        var next = current;
        do {
            next *= factor;
            next %= 2147483647;
        } while ((next & multipleMask) != 0);

        current = next;

        return current;
    }
}