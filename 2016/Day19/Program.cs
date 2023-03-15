var elfCount = int.Parse(File.ReadAllText("input.txt"));

var elves = new LinkedList<int>();
LinkedListNode<int>? current = null;
LinkedListNode<int>? target = null;

Console.WriteLine("Solving part 1...");

ResetList();
current = elves.First!;
while (elves.Count > 1) {
    elves.Remove(GetNextNodeCircular(current));
    current = GetNextNodeCircular(current);
}

Console.WriteLine($"The elf having all the presents is {elves.First!.Value}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");

// Use two pointers to the same linked list, opposite of the same ring
ResetList();
current = elves.First!;
while (elves.Count > 1) {
    var remove = target;
    target = GetNextNodeCircular(target!);
    elves.Remove(remove!);
    if (elves.Count % 2 == 0) {
        target = GetNextNodeCircular(target);
    }

    current = GetNextNodeCircular(current);
}

Console.WriteLine($"The elf having all the presents is {elves.First!.Value}");

void ResetList() {
    for (var i = 0; i < elfCount; i++) {
        if (i == 0) {
            current = elves.AddFirst(i + 1);
            continue;
        }

        current = elves.AddAfter(current!, i + 1);

        if (i == elfCount / 2) {
            target = current;
        }
    }
}

LinkedListNode<int> GetNextNodeCircular(LinkedListNode<int> node) {
    return node.Next! ?? elves.First!;
}