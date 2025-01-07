using System.Collections;

namespace Common.Utils.Grid2D;

public class SparseGrid : IEnumerable<(Point point, char value)>
{
    private readonly List<(Point position, char value)> points = new();

    public SparseGrid(string[] rows, char? emptyChar = null)
    {
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {
                if (emptyChar.HasValue && rows[y][x] == emptyChar.Value) continue;
                points.Add((new Point(x, y), rows[y][x]));
            }
        }
        
        XLength = rows.First().Length;
        YLength = rows.Length;
    }

    public SparseGrid(int xLength, int yLength)
    {
        XLength = xLength;
        YLength = yLength;
    }

    public int XLength { get; }
    public int YLength { get; }
    
    public char[] this[int x, int y]
    {
        get => this[new Point(x, y)];
        set => this[new Point(x, y)] = value;
    }

    public char[] this[Point point]
    {
        get => points
            .Where(p => p.position.Equals(point))
            .Select(pair => pair.value).ToArray();

        set => SetValue(point, value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerator<(Point point, char value)> GetEnumerator()
    {
        return points.GetEnumerator();
    }

    public bool IsEmpty(Point point)
    {
        return this[point].Length == 0;
    }

    private void SetValue(Point point, char[] value)
    {
        _ = points.RemoveAll(p => p.position.Equals(point));
        foreach (char c in value)
        {
            points.Add((point, c));
        }
    }
}