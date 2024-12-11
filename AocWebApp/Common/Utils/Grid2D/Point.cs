namespace Common.Utils.Grid2D;

public record Point(int X, int Y)
{
    private static readonly Offset Up = new(0, -1);
    private static readonly Offset Down = new(0, 1);
    private static readonly Offset Left = new(-1, 0);
    private static readonly Offset Right = new(1, 0);

    public Point Add(int x, int y)
    {
        return new Point(X + x, Y + y);
    }

    public Point Add(Offset offset)
    {
        return Add(offset.Dx, offset.Dy);
    }

    public Offset OffsetTo(Point point)
    {
        return new Offset(X - point.X, Y - point.Y);
    }

    public Point Move(Orientation orientation)
    {
        return orientation switch
        {
            Orientation.Up or Orientation.North => Add(Up),
            Orientation.Left or Orientation.West => Add(Left),
            Orientation.Down or Orientation.South => Add(Down),
            Orientation.Right or Orientation.East => Add(Right),
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
        };
    }
}