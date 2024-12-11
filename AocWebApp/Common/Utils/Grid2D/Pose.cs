namespace Common.Utils.Grid2D;

public class Pose
{
    public Pose(Point location, Orientation orientation)
    {
        Location = location;
        Orientation = orientation;
    }

    public Point Location { get; }
    public Orientation Orientation { get; }

    public Pose TurnLeft()
    {
        return Orientation switch
        {
            Orientation.Up or Orientation.North => new Pose(Location, Orientation.West),
            Orientation.Left or Orientation.West => new Pose(Location, Orientation.South),
            Orientation.Down or Orientation.South => new Pose(Location, Orientation.East),
            Orientation.Right or Orientation.East => new Pose(Location, Orientation.North),
            _ => throw new ArgumentOutOfRangeException(nameof(Orientation), Orientation, null)
        };
    }

    public Pose TurnRight()
    {
        return Orientation switch
        {
            Orientation.Up or Orientation.North => new Pose(Location, Orientation.East),
            Orientation.Left or Orientation.West => new Pose(Location, Orientation.North),
            Orientation.Down or Orientation.South => new Pose(Location, Orientation.West),
            Orientation.Right or Orientation.East => new Pose(Location, Orientation.South),
            _ => throw new ArgumentOutOfRangeException(nameof(Orientation), Orientation, null)
        };
    }

    protected bool Equals(Pose other)
    {
        return Location.Equals(other.Location) && Orientation == other.Orientation;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Pose)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Location, (int)Orientation);
    }
}