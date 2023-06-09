namespace Day21;

public class Grid
{
    public Grid(params string[] rows) {
        foreach (var row in rows) Rows.Add(row);
    }

    public List<string> Rows { get; private set; } = new();
    public int Size => Rows.Count;

    public void FlipHorizontally() {
        var newRows = new List<string>();

        for (var i = 0; i < Size; i++) newRows.Add("");

        for (var i = 0; i < Size / 2; i++) {
            newRows[Size - i - 1] = Rows[i];
            newRows[i] = Rows[Size - i - 1];
        }

        if (Size % 2 != 0) newRows[Size / 2] = Rows[Size / 2];

        Rows = newRows;
    }

    public void FlipVertically() {
        var newRows = Rows.Select(row => new string(row.Reverse().ToArray())).ToList();
        Rows = newRows;
    }

    public void Rotate() {
        var newRows = new List<string>();

        for (var i = 0; i < Size; i++) newRows.Add(new string(' ', Size));

        var newRow = 0;
        for (var oldColumn = Size - 1; oldColumn >= 0; oldColumn--) {
            var newColumn = 0;
            for (var oldRow = 0; oldRow < Size; oldRow++) {
                newRows[newRow] = $"{newRows[newRow][..newColumn]}{Rows[oldRow][oldColumn]}{newRows[newRow][(newColumn + 1)..]}";
                newColumn++;
            }

            newRow++;
        }

        Rows = newRows;
    }

    public IEnumerable<Grid> SplitToSize(int size) {
        if (Size % size != 0) throw new ArgumentException("Cannot divide grid evenly");

        var result = new List<Grid>();

        for (var row = 0; row < Size / size; row++) {
            var rows = Rows.Skip(row * size).Take(size).ToList();
            for (var col = 0; col < Size / size; col++) {
                var newGridRows = new List<string>();
                foreach (var r in rows) newGridRows.Add(r[(col * size)..(col * size + size)]);
                result.Add(new Grid(newGridRows.ToArray()));
            }
        }

        return result;
    }

    public override bool Equals(object? obj) {
        if (obj is not Grid other) return false;
        if (other.Size != Size) return false;

        for (var i = 0; i < Size; i++)
            if (Rows[i] != other.Rows[i])
                return false;

        return true;
    }

    public override int GetHashCode() {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return Rows.GetHashCode();
    }
}