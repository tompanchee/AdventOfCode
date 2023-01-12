namespace AOCUtils.Geometry._2D;

public class Point
{
    public Point(long x, long y) {
        X = x;
        Y = y;
    }

    public long X { get; set; }

    public long Y { get; set; }

    public long XDiffTo(Point other) {
        return Math.Abs(X - other.X);
    }

    public long YDiffTo(Point other)
    {
        return Math.Abs(Y - other.Y);
    }

    public long ManhattanDistanceTo(Point other) {
        return XDiffTo(other) + YDiffTo(other);
    }

    protected bool Equals(Point other) {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point) obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(X, Y);
    }
}