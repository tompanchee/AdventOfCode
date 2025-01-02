using Common.Utils.Grid2D;

namespace Y24_Day14;

internal class Robot
{
    private Robot(Point position, Offset velocity)
    {
        Position = position;
        Velocity = velocity;
    }

    private Offset Velocity { get; }

    public Point Position { get; private set; }

    public static Robot Parse(string data)
    {
        string[] parts = data.Split(' ', StringSplitOptions.TrimEntries);
        string[] pParts = parts[0][2..].Split(',', StringSplitOptions.TrimEntries);
        string[] vParts = parts[1][2..].Split(',', StringSplitOptions.TrimEntries);

        return new Robot(new Point(int.Parse(pParts[0]), int.Parse(pParts[1])),
            new Offset(int.Parse(vParts[0]), int.Parse(vParts[1])));
    }

    public void Move(int t, int xSize, int ySize)
    {
        Point newPosition = Position.Add(t * Velocity);

        // Wrap around
        Position = new Point(Mod(newPosition.X, xSize), Mod(newPosition.Y, ySize));
    }

    private static int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }
}