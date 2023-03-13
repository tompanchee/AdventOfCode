var elfCount = int.Parse(File.ReadAllText("input.txt"));

var elves = new LinkedList<(int, int)>();
LinkedListNode<(int, int)>? current = null;
for (var i = 0; i < elfCount; i++) {
    if (i == 0) {
        current = elves.AddFirst((i + 1, 1));
        continue;
    }

    current = elves.AddAfter(current!, (i + 1, 1));
}

Console.WriteLine("Solving part 1...");
current = elves.First!;
while (true) {
    if (elves.Count == 1) break;
    if (current == elves.Last) {
        current!.Value = (current.Value.Item1, current.Value.Item2 + elves.First!.Value.Item2);
        elves.Remove(elves.First);
        current = elves.First;
    } else {
        current!.Value = (current.Value.Item1, current.Value.Item2 + current.Next!.Value.Item2);
        elves.Remove(current.Next);
        current = current.Next ?? elves.First;
    }
}

Console.WriteLine($"The elf having all the presents is {elves.First!.Value.Item1}");