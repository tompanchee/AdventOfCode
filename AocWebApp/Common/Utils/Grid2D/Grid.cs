using System.Collections;

namespace Common.Utils.Grid2D;

public class Grid : IEnumerable<(Point point, char value)>
{
    public Grid(string[] rows)
    {
        Rows = rows;
    }

    private int MaxX => Rows.Length;
    private int MaxY => Rows[0].Length;

    public char this[int x, int y] => Rows[y][x];
    public char this[Point point] => this[point.X, point.Y];

    public bool Contains(int x, int y)
    {
        return x >= 0 && y >= 0 && x < MaxX && y < MaxY;
    }

    public bool Contains(Point point)
    {
        return Contains(point.X, point.Y);
    }

    public string[] Rows { get; }

    public IEnumerator<(Point point, char value)> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class Enumerator : IEnumerator<(Point point, char value)>
    {
        private int x = -1;
        private int y;
        private readonly Grid grid1;

        public Enumerator(Grid grid)
        {
            grid1 = grid;
        }

        public (Point point, char value) Current => (new Point(x, y), grid1[x, y]);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (++x >= grid1.MaxX)
            {
                x = 0;
                y++;
            }

            return y < grid1.MaxY;
        }

        public void Reset()
        {
            x = -1;
            y = 0;
        }
    }

    public Point[] GetNeighbours(Point head)
    {
        List<Point> neighbours = new List<Point>();

        Point n = head.Move(Orientation.North);
        if (Contains(n))
        {
            neighbours.Add(n);
        }

        n = head.Move(Orientation.East);
        if (Contains(n))
        {
            neighbours.Add(n);
        }

        n = head.Move(Orientation.South);
        if (Contains(n))
        {
            neighbours.Add(n);
        }

        n = head.Move(Orientation.West);
        if (Contains(n))
        {
            neighbours.Add(n);
        }

        return neighbours.ToArray();
    }
}