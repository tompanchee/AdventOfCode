namespace Day11;

public class Floor
{
    public IList<Item> Items { get; } = new List<Item>();

    public bool IsAllowed() {
        var microchips = Items.Where(i => i.ToString().Last() == 'M').Select(i => i.ToString()[..^1]).ToList();
        var generators = Items.Where(i => i.ToString().Last() == 'G').Select(i => i.ToString()[..^1]).ToList();

        var result = !generators.Any() || !microchips.Any();

        result = result || microchips.All(microchip => generators.Contains(microchip));

        return result;
    }

    public short GetHash() {
        return Items.Aggregate<Item, short>(0, (current, item) => (short) (current | (short) (1 << (int) item)));
    }
}