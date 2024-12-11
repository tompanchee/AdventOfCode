namespace Common.Utils.Grid2D;

public class Grid
{
    private readonly string[] rows;

    public Grid(string[] rows)
    {
        this.rows = rows;
    }

    private int MaxX => rows.Length;
    private int MaxY => rows[0].Length;

    public char this[int x, int y] => rows[y][x];
    public char this[Point point] => this[point.X, point.Y];

    public bool Contains(int x, int y)
    {
        return x >= 0 && y >= 0 && x < MaxX && y < MaxY;
    }

    public bool Contains(Point point)
    {
        return Contains(point.X, point.Y);
    }

    public string[] Rows => rows;
}