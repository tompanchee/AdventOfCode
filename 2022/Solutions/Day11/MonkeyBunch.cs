using System.Text;

namespace Day11;

internal class MonkeyBunch
{
    readonly List<Monkey> monkeys = new();

    MonkeyBunch() { }

    public long MonkeyBusiness
    {
        get
        {
            var orderedMonkeys = monkeys.OrderByDescending(m => m.InspectedItems).ToArray();
            return orderedMonkeys[0].InspectedItems * orderedMonkeys[1].InspectedItems;
        }
    }

    public static MonkeyBunch FromInput(string[] input, bool divideByThree = true, long? reducer = null) {
        var bunch = new MonkeyBunch();

        var monkeyData = new List<string>();
        foreach (var row in input) {
            if (string.IsNullOrEmpty(row)) {
                bunch.AddMonkey(monkeyData, divideByThree, reducer);
                monkeyData.Clear();
                continue;
            }

            monkeyData.Add(row);
        }

        return bunch;
    }

    void AddMonkey(List<string> monkeyData, bool divideByThree, long? reducer) {
        monkeys.Add(Monkey.Parse(monkeyData, this, divideByThree, reducer));
    }

    public void ThrowItemTo(Item item, int monkeyId) {
        monkeys[monkeyId].ReceiveItem(item);
    }

    public void PlayRound() {
        foreach (var monkey in monkeys) monkey.HandleItems();
    }

    public string GetDebugData() {
        var sb = new StringBuilder();
        sb.AppendLine();
        for (var i = 0; i < monkeys.Count; i++) {
            sb.Append($"Monkey {i}: ");
            sb.AppendLine(string.Join(", ", monkeys[i].Items.Select(item => item.WorryLevel)));
        }

        return sb.ToString();
    }
}