//var input = File.ReadAllLines("test_input.txt");
var input = File.ReadAllLines("input.txt");
(string? enhancer, HashSet<(int x, int)> image) = Parse(input);

var imageEnhacer = new ImageEnhacer(enhancer);

Console.WriteLine("Solving puzzle 1...");
PrintImage(image);
image = imageEnhacer.Enhance(image);
PrintImage(image);
image = imageEnhacer.Enhance(image);
PrintImage(image);
Console.WriteLine($"After two enhancement rounds there are {image.Count()} lit pixels");

(string?, HashSet<(int x, int y)>) Parse(string[] input) {
    string? enhancer =  null;
    var image = new HashSet<(int x, int y)>();
    var y = 0;
    foreach(var line in input) {
        if (string.IsNullOrWhiteSpace(line)) continue;
        if (enhancer == null) {
            enhancer = line;
            continue;
        }
        for(var x = 0; x < line.Length; x++) {
            if (line[x] == '#') image.Add((x, y));
        }
        y++;
    }

    return (enhancer, image);
}

void PrintImage(HashSet<(int x, int y)> image) {
    var ally = image.Select(p => p.y);
    var allx = image.Select(p => p.x);
    for(var y = ally.Min() - 1; y <= ally.Max() + 1; y++) {
        for(var x = allx.Min() - 1; x <= allx.Max() + 1; x++) {
            if (x==0 && y==0) Console.ForegroundColor = ConsoleColor.Red;
            if (image.Contains((x, y))) Console.Write("#");
            else Console.Write(".");
            Console.ResetColor();
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

class ImageEnhacer {
    bool isBackgroundLit = false;
    readonly string enhancer;

    public ImageEnhacer(string enhancer) {
        this.enhancer = enhancer;
    }

    public HashSet<(int x, int y)> Enhance(HashSet<(int x, int y)> image) {
        var offsets = new (int dx, int dy)[] {
            (-1, -1),
            (0, -1),
            (1, -1),
            (-1, 0),
            (0, 0),
            (1, 0),
            (-1, 1),
            (0, 1),
            (1, 1)
        };

        var enhanced = new HashSet<(int x, int y)>();
        var ally = image.Select(p => p.y);
        var allx = image.Select(p => p.x);

        var minx = allx.Min()-1;
        var maxx = allx.Max()+1;
        var miny = ally.Min()-1;
        var maxy = ally.Max()+1;
        

        for(var x = minx - 1; x <= maxx + 1; x++) {
            for(var y = miny - 1; y <= maxy + 1; y++) {
                string binary = "";
                foreach(var offset in offsets) {
                    var (tx, ty) = (x + offset.dx, y + offset.dy);
                    if (x < minx || x > maxx || y < miny || y > maxy) binary += isBackgroundLit ? "1" : "0";                
                    else if (image.Contains((tx, ty))) binary += "1";
                    else binary += "0";
                }                
                var idx = Convert.ToInt32(binary, 2);
                //Console.WriteLine($"({x},{y}) => {binary} == {idx} => {enhancer[idx]}");
                if (enhancer[idx] == '#') enhanced.Add((x, y));
            }
        }

        isBackgroundLit = !isBackgroundLit;
        return enhanced;
    }
}