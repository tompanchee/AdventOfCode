namespace Day20;

internal class Vector
{
    public Vector(long x, long y, long z) {
        X = x;
        Y = y;
        Z = z;
    }

    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }

    public long ManhattanDistanceTo(Vector other) {
        return Math.Abs(X - other.X)
               + Math.Abs(Y - other.Y)
               + Math.Abs(Z - other.Z);
    }

    public static Vector operator +(Vector a, Vector b) {
        return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    protected bool Equals(Vector other) {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object? obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Vector)obj);
    }

    public override int GetHashCode() {
        // ReSharper disable NonReadonlyMemberInGetHashCode
        return HashCode.Combine(X, Y, Z);
    }
}