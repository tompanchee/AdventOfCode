namespace Day16;

internal class Ray
{
    private (int x, int y) position;
    private EDirection direction;

    public Ray((int x, int y) position, EDirection direction)
    {
        this.position = position;
        this.direction = direction;
    }

    public void Move()
    {
        switch (direction)
        {
            case EDirection.North:
                position.y--;
                break;
            case EDirection.South:
                position.y++;
                break;
            case EDirection.West:
                position.x--;
                break;
            case EDirection.East:
                position.x++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool IsOutOfBounds(string[] cave)
    {
        if (position.x < 0 || position.y < 0) return true;
        if (position.x > cave[0].Length - 1) return true;
        if (position.y > cave.Length - 1) return true;

        return false;
    }

    public void Turn(char mirror)
    {
        direction = direction switch
        {
            EDirection.East => mirror switch
            {
                '/' => EDirection.North,
                '\\' => EDirection.South,
                _ => direction
            },
            EDirection.North => mirror switch
            {
                '/' => EDirection.East,
                '\\' => EDirection.West,
                _ => direction
            },
            EDirection.West => mirror switch
            {
                '/' => EDirection.South,
                '\\' => EDirection.North,
                _ => direction
            },
            EDirection.South => mirror switch
            {
                '/' => EDirection.West,
                '\\' => EDirection.East,
                _ => direction
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public (int x, int y) Position { get => position; set => position = value; }

    public EDirection Direction { get => direction; set => direction = value; }

    public enum EDirection
    {
        North,
        East,
        South,
        West
    }
}