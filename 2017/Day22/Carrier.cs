namespace Day22;

internal class Carrier
{
    public enum TurnDirection
    {
        Left,
        Right
    }

    Direction direction = Direction.Up;
    int x;
    int y;

    public (int x, int y) Location => (x, y);

    public void Move() {
        switch (direction) {
            case Direction.Up:
                y++;
                break;
            case Direction.Down:
                y--;
                break;
            case Direction.Left:
                x--;
                break;
            case Direction.Right:
                x++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Turn(TurnDirection td) {
        if (td == TurnDirection.Left)
            direction = direction switch {
                Direction.Up => Direction.Left,
                Direction.Right => Direction.Up,
                Direction.Down => Direction.Right,
                Direction.Left => Direction.Down,
                _ => throw new ArgumentOutOfRangeException()
            };
        else
            direction = direction switch {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new ArgumentOutOfRangeException()
            };
    }

    enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}