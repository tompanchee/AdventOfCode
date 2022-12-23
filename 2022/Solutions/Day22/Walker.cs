namespace Day22;

internal class Walker
{
    static readonly List<char> directions = new(new[] {'R', 'D', 'L', 'U'});

    static readonly IDictionary<char, (int dr, int dc)> offsets = new Dictionary<char, (int dr, int dc)> {
        {'R', (0, 1)},
        {'D', (1, 0)},
        {'L', (0, -1)},
        {'U', (-1, 0)}
    };

    readonly int height;

    readonly List<string> map;
    readonly int width;

    (int row, int col) position;

    public Walker((int row, int col) position, char direction, List<string> map) {
        this.position = position;
        Direction = direction;
        this.map = map;
        width = map[0].Length;
        height = map.Count;
    }

    public (int row, int col) Position { get => position; set => position = value; }
    public char Direction { get; set; }

    public void ExecuteInstruction(string instruction) {
        if (int.TryParse(instruction, out var distance)) Move(distance);
        else Turn(instruction);
    }

    public long CalculatePassword() {
        return 1000 * (position.row + 1) + 4 * (position.col + 1) + directions.IndexOf(Direction);
    }

    void Move(int distance) {
        var offset = offsets[Direction];
        var next = position;
        for (var i = 0; i < distance; i++) {
            do {
                next = (next.row + offset.dr, next.col + offset.dc);
                if (next.row < 0) next.row = height - 1;
                if (next.row >= height) next.row = 0;
                if (next.col < 0) next.col = width - 1;
                if (next.col >= width) next.col = 0;
            } while (map[next.row][next.col] == ' ');

            if (map[next.row][next.col] == '#') break; // Stop
            if (map[next.row][next.col] == '.') position = next; // Keep on moving
        }
    }

    void Turn(string instruction) {
        var idx = directions.IndexOf(Direction);
        if (instruction == "L") idx--;
        else idx++;

        if (idx < 0) idx = directions.Count - 1;
        if (idx >= directions.Count) idx = 0;

        Direction = directions[idx];
    }
}