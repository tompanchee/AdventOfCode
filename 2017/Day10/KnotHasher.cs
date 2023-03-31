using System.Text;

namespace Day10;

internal class KnotHasher
{
    readonly List<int> inner = new();

    KnotHasher() { }

    public int this[int i] { get => inner[i % inner.Count]; set => inner[i % inner.Count] = value; }

    public static KnotHasher Init() {
        var list = new KnotHasher();
        list.inner.AddRange(Enumerable.Range(0, 256));

        return list;
    }

    public void Scramble(int[] input, int rounds = 1) {
        var pos = 0;
        var skip = 0;

        for (var r = 0; r < rounds; r++)
            foreach (var length in input) {
                PinchAndTwist(pos, length);
                pos += length + skip;
                skip++;

                pos %= inner.Count;
                skip %= inner.Count;
            }
    }

    public string CalculateHash() {
        var sb = new StringBuilder();
        for (var i = 0; i < 16; i++) {
            var hashSet = inner.Skip(i * 16).Take(16);
            var hash = hashSet.Aggregate(0, (current, v) => current ^ v);
            sb.Append(hash.ToString("x2"));
        }

        return sb.ToString();
    }

    void PinchAndTwist(int pos, int length) {
        var subset = GetSubSet(pos, length);
        subset.Reverse();
        ReplaceWith(pos, subset);
    }

    List<int> GetSubSet(int start, int count) {
        var subset = new List<int>();
        if (count <= 0) return subset;

        for (var i = start; i < start + count; i++) subset.Add(this[i]);

        return subset;
    }

    void ReplaceWith(int start, IReadOnlyList<int> replacement) {
        for (var i = 0; i < replacement.Count; i++) this[start + i] = replacement[i];
    }
}