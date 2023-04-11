var input = 303;

Console.WriteLine("Solving puzzle 1...");

var sl = new Spinlock();

var current = 0;
var pos = 0;
while (sl.Length < 2018) {
    pos = sl.GetIndex(pos + input);
    sl.InsertAfterPos(pos, ++current);
    pos++;
}

Console.WriteLine($"Value after the last value is {sl[pos + 1]}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");

// Keep track on pos 1
current = 0;
pos = 0;
var length = 1;
var valueAt1 = -1;

while (length < 50000001) {
    current++;
    pos = (pos + input) % length;
    if (++pos == 1) valueAt1 = current;
    length++;
}

Console.WriteLine($"Value after zero is {valueAt1}");

internal class Spinlock
{
    readonly List<int> inner = new() { 0 };

    public int Length => inner.Count;

    public int this[int i] => inner[i % inner.Count];

    public int GetIndex(int idx) {
        return idx % inner.Count;
    }

    public void InsertAfterPos(int pos, int value) {
        inner.Insert(pos + 1, value);
    }
}