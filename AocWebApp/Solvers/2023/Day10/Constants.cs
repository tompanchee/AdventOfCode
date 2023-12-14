namespace Day10;

static class Constants
{
    public static readonly Dictionary<char, (int dx, int dy)> Offsets = new() { { 'N', (0, -1) }, { 'E', (1, 0) }, { 'S', (0, 1) }, { 'W', (-1, 0) } };

    public static readonly Dictionary<char, (string d1, string d2)> Pipes = new()
    {
        { '|', ("N", "S") },
        { '-', ("E", "W") },
        { 'L', ("SE", "WN") },
        { 'J', ("EN", "SW") },
        { '7', ("ES", "NW") },
        { 'F', ("NE", "WS") },
    };
}