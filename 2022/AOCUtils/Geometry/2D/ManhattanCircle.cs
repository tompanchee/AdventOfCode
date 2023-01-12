namespace AOCUtils.Geometry._2D;

public class ManhattanCircle
{
    public ManhattanCircle(Point centre, long radius) {
        this.Centre = centre;
        this.Radius = radius;
    }

    public Point Centre { get; }

    public long Radius { get; }

    public bool IsInCircle(Point other) {
        return Centre.ManhattanDistanceTo(other) <= Radius;
    }

    public Point[] CircumferencePoints() {
        var result = new HashSet<Point>();

        for (var r = -Radius; r <= Radius; r++) {
            var x = Centre.X + r;
            var y = Centre.Y - (Math.Abs(x - Centre.X) - Radius);
            var p = new Point(x, y);
            result.Add(p);
            y = Centre.Y + (Math.Abs(x - Centre.X) - Radius);
            p = new Point(x, y);
            result.Add(p);
        }

        return result.ToArray();
    }
}