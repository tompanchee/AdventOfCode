namespace Day03;

class Schematics
{
    readonly string[] raw;
    private int maxX;
    private int maxY;

    public Schematics(string[] raw)
    {
        this.raw = raw;

        Parse();
    }

    public bool IsPartNumber(Number number)
    {
        (int x1, int x2) = (number.StartPosition.X - 1, number.StartPosition.X + number.Value.ToString().Length);
        (int y1, int y2) = (number.StartPosition.Y - 1, number.StartPosition.Y + 1);

        for (int x = x1; x <= x2; x++)
        {
            if (x < 0 || x > maxX) continue;
            for (int y = y1; y <= y2; y++)
            {
                if (y < 0 || y > maxY) continue;

                if (Symbols.Any(s => s.Position.X == x && s.Position.Y == y)) return true;
            }
        }

        return false;
    }

    public int CalculateGearRatio(Symbol symbol)
    {
        if (symbol.Value != '*')
        {
            return 0;
        }

        var adjacentNumbers = new List<int>();
        foreach (var number in Numbers)
        {
            (int x1, int x2) = (number.StartPosition.X - 1, number.StartPosition.X + number.Value.ToString().Length);
            (int y1, int y2) = (number.StartPosition.Y - 1, number.StartPosition.Y + 1);

            if (symbol.Position.X >= x1
                && symbol.Position.X <= x2
                && symbol.Position.Y >= y1
                && symbol.Position.Y <= y2)
            {
                adjacentNumbers.Add(number.Value);
            }
        }

        return adjacentNumbers.Count == 2 ? adjacentNumbers[0] * adjacentNumbers[1] : 0;
    }

    public List<Symbol> Symbols { get; } = new();
    public List<Number> Numbers { get; } = new();

    private void Parse()
    {
        for (int y = 0; y < raw.Length; y++)
        {
            var number = new List<char>();
            for (int x = 0; x < raw[y].Length; x++)
            {
                if (char.IsDigit(raw[y][x]))
                {
                    number.Add(raw[y][x]);
                }
                else
                {
                    if (raw[y][x] != '.')
                    {
                        Symbols.Add(new Symbol(raw[y][x], new Point(x, y)));
                    }

                    if (number.Any())
                    {
                        var str = new string(number.ToArray());
                        Numbers.Add(new Number(int.Parse(str), new Point(x - str.Length, y)));
                        number.Clear();
                    }
                }
            }

            if (number.Any())
            {
                var str = new string(number.ToArray());
                Numbers.Add(new Number(int.Parse(str), new Point(raw[y].Length - str.Length, y)));
                number.Clear();
            }
        }

        maxY = raw.Length;
        maxX = raw.Max(r => r.Length);
    }

    internal record Symbol(char Value, Point Position);

    internal record Number(int Value, Point StartPosition);
}