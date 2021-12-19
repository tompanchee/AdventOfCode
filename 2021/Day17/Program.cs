var input = File.ReadAllText("input.txt");
var area = Parse(input);

Console.WriteLine("Solving puzzle 1...");

var maxHeight = int.MinValue;
// With x velocity greater than area right probe is off target
for (var vx = 0; vx <= area.Right; vx++) { 
    var maxy = int.MinValue;
    for (var vy = 0; vy < 100; vy++) { // Brute force with max vy approximation
        var probe = new Probe((0, 0), (vx, vy));
        while (true) {
            Step(probe);
            if (probe.Position.y > maxy) maxy = probe.Position.y;
            if (area.IsInArea(probe.Position)) {
                if (maxy > maxHeight) maxHeight = maxy;
                break;
            }
            if (probe.Velocity.dx < 0 || probe.Position.y < area.Bottom) break;
        }
    }
}

Console.WriteLine($"Maximum trajectory height is {maxHeight}");

Console.WriteLine();
var initialVelocityCount = 0;
for (var vx = 0; vx <= area.Right; vx++) {
    for (var vy = -100; vy < 100; vy++) { // Brute force with vy approximation
        var probe = new Probe((0, 0), (vx, vy));
        while (true) {
            Step(probe);            
            if (area.IsInArea(probe.Position)){
                initialVelocityCount++;
                break;
            }
            if (probe.Velocity.dx < 0 || probe.Position.y < area.Bottom) break;
        }
    }
}

Console.WriteLine($"There are {initialVelocityCount} initial velocites to reach area");

Area Parse(string input) {
    var d = input["target area: ".Length..];
    var s = d.Split(',', StringSplitOptions.TrimEntries);
    var x = s[0];
    var y = s[1];
    var xs = x["x=".Length..].Split("..");
    var ys = y["y=".Length..].Split("..");

    var topLeft = (int.Parse(xs[0]), int.Parse(ys[1]));
    var bottomRight = (int.Parse(xs[1]), int.Parse(ys[0]));

    return new Area(topLeft, bottomRight);
}

void Step(Probe probe) {
    probe.Position = (probe.Position.x + probe.Velocity.dx, probe.Position.y + probe.Velocity.dy);
    probe.Velocity = (probe.Velocity.dx == 0 ? 0 : probe.Velocity.dx - 1, probe.Velocity.dy-1);
}

class Probe 
{
    public Probe((int x, int y) position, (int dx, int dy) velocity) {
        Position = position;
        Velocity = velocity;
    }

    public (int x, int y) Position {get; set;}
    public(int dx, int dy) Velocity { get; set; }
}

class Area
{
    readonly (int x, int y) topLeft;
    readonly (int x, int y) bottomRight;

    public Area((int x, int y) topLeft, (int x, int y) bottomRight) {
        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
    }

    public int Top => topLeft.y;
    public int Bottom => bottomRight.y;
    public int Left => topLeft.x;
    public int Right => bottomRight.x;

    public bool IsInXRange((int x, int y) point) => (point.x >= Left && point.x <= Right);
    public bool IsInYRange((int x, int y) point) => (point.y <= Top && point.y >= Bottom);
    public bool IsInArea((int x, int y) point) => IsInXRange(point) && IsInYRange(point); 
}
