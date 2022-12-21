namespace Day20;

internal class Item
{
    public Item(long value, int originalIndex) {
        Value = value;
        OriginalIndex = originalIndex;
    }

    public long Value { get; set; }
    public int OriginalIndex { get; }

    // Use original index for equality to make search work
    protected bool Equals(Item other) {
        return OriginalIndex == other.OriginalIndex;
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Item) obj);
    }

    public override int GetHashCode() {
        return OriginalIndex;
    }
}