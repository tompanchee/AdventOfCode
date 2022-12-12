namespace Day11;

internal class Monkey
{
    readonly MonkeyBunch bunch;
    readonly bool divideByThree;
    readonly int divider;
    readonly int falseTarget;
    readonly string operation;
    readonly long? reducer;
    readonly int trueTarget;

    Monkey(MonkeyBunch bunch, string operation, int divider, int trueTarget, int falseTarget, bool divideByThree, long? reducer) {
        this.bunch = bunch;
        this.operation = operation;
        this.divider = divider;
        this.trueTarget = trueTarget;
        this.falseTarget = falseTarget;
        this.divideByThree = divideByThree;
        this.reducer = reducer;
    }

    public long InspectedItems { get; private set; }

    public List<Item> Items { get; } = new();

    public static Monkey Parse(List<string> monkeyData, MonkeyBunch bunch, bool divideByThree, long? reducer) {
        var operation = monkeyData[2].Trim()[17..];
        var divider = int.Parse(monkeyData[3].Trim()[19..]);
        var trueTarget = int.Parse(monkeyData[4].Trim()[25..]);
        var falseTarget = int.Parse(monkeyData[5].Trim()[26..]);
        var monkey = new Monkey(bunch, operation, divider, trueTarget, falseTarget, divideByThree, reducer);

        foreach (var item in monkeyData[1].Trim()[16..].Split(',', StringSplitOptions.TrimEntries)) monkey.AddItem(int.Parse(item));

        return monkey;
    }

    void AddItem(int worryLevel) {
        Items.Add(new Item {WorryLevel = worryLevel});
    }

    public void HandleItems() {
        foreach (var item in Items) HandleItem(item);

        InspectedItems += Items.Count;
        Items.Clear();
    }

    void HandleItem(Item item) {
        var split = operation.Split(new[] {'*', '+'}, StringSplitOptions.TrimEntries);
        var left = split[0];
        var right = split[1];

        var leftValue = left == "old" ? item.WorryLevel : int.Parse(left);
        var rightValue = right == "old" ? item.WorryLevel : int.Parse(right);

        var newValue = operation.Contains('+') ? leftValue + rightValue : leftValue * rightValue;
        if (divideByThree) newValue /= 3;
        if (reducer.HasValue) newValue %= reducer.Value;

        item.WorryLevel = newValue;

        var target = newValue % divider == 0 ? trueTarget : falseTarget;

        bunch.ThrowItemTo(item, target);
    }

    public void ReceiveItem(Item item) {
        Items.Add(item);
    }
}