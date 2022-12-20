namespace AOCUtils.Geometry._3D;

public class Point
{
    public Point(int x, int y, int z) {
        X = x;
        Y = y;
        Z = z;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public Point CreateNewWithOffset((int dx, int dy, int dz) offset) {
        var (dx, dy, dz) = offset;
        return new Point(X + dx, Y + dy, Z + dz);
    }

    protected bool Equals(Point other) {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Point) obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(X, Y, Z);
    }
}