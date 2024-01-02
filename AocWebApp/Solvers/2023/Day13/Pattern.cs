namespace Day13;

internal class Pattern
{
    public Pattern(IEnumerable<string> input)
    {
        Rows = input.ToList();
    }

    public (List<int> rows, List<int> cols) GetLinesOfReflection()
    {
        var r = new List<int>();
        var c = new List<int>();

        for (var col = 1; col < Rows[0].Length; col++)
        {
            if (IsMirrorAtColumn(col))
            {
                c.Add(col);
            }
        }

        for (var row = 1; row < Rows.Count; row++)
        {
            if (IsMirrorAtRow(row))
            {
                r.Add(row);
            }
        }

        return (r, c);
    }

    public void Swap(int row, int col)
    {
        var c = Rows[row][col];
        var oldRow = Rows[row].ToCharArray();
        oldRow[col] = c == '.' ? '#' : '.';
        var newRow = new string(oldRow);
        Rows[row] = newRow;
    }

    private bool IsMirrorAtColumn(int col)
    {
        var count = Math.Min(col, Rows[0].Length - col);

        foreach (var row in Rows)
        {
            for (var i = 0; i < count; i++)
            {
                if (row[col - i - 1] != row[col + i]) return false;
            }
        }

        return true;
    }

    private bool IsMirrorAtRow(int row)
    {
        var count = Math.Min(row, Rows.Count - row);

        for (var i = 0; i < count; i++)
        {
            for (var c = 0; c < Rows[0].Length; c++)
            {
                if (Rows[row - i - 1][c] != Rows[row + i][c]) return false;
            }
        }

        return true;
    }

    public List<string> Rows { get; }

    public int? ColumnOfReflection { get; set; }
    public int? RowOfReflection { get; set; }
}