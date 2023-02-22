using AOCUtils.Geometry._2D;

namespace Day24;

internal class Blizzard
{
    public Blizzard(Point position, char direction) {
        Position = position;
        Direction = direction;
    }

    public Point Position { get; init; }
    public char Direction { get; init; }

    public Blizzard Move() {
        var newPos = new Point(Position.X, Position.Y);
        switch (Direction) {
            case 'v':
                newPos.Y++;
                if (newPos.Y == Constants.EDGE_Y_MAX) newPos.Y = Constants.EDGE_Y_MIN + 1;
                break;
            case '^':
                newPos.Y--;
                if (newPos.Y == Constants.EDGE_Y_MIN) newPos.Y = Constants.EDGE_Y_MAX - 1;
                break;
            case '>':
                newPos.X++;
                if (newPos.X == Constants.EDGE_X_MAX) newPos.X = Constants.EDGE_X_MIN + 1;
                break;
            case '<':
                newPos.X--;
                if (newPos.X == Constants.EDGE_X_MIN) newPos.X = Constants.EDGE_X_MAX - 1;
                break;
        }

        return new Blizzard(newPos, Direction);
    }
}