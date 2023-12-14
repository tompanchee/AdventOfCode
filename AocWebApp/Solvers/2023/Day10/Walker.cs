namespace Day10;

internal class Walker
{


    private readonly string[] map;

    private (int x, int y) position;
    private char? previousDirection;
    private char nextDirection;

    public Walker(string[] map, (int x, int y) initialPosition)
    {
        this.map = map;
        position = initialPosition;
    }

    public void WalkStep(char? direction = null)
    {
        if (direction != null) nextDirection = direction.Value;

        var d = nextDirection;
        (int dx, int dy) = Constants.Offsets[d];
        position.x += dx;
        position.y += dy;
        previousDirection = d;

        if (map[position.y][position.x] == 'S') return;
        var pipe = Constants.Pipes[map[position.y][position.x]];
        var next = pipe.d1.Contains(previousDirection.Value) ? pipe.d1 : pipe.d2;

        nextDirection = next.Length == 1
            ? next[0]
            : next.Single(c => c != previousDirection.Value);
    }

    public (int x, int y) Position => position;
}