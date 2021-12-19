var input = File.ReadAllLines("input.txt");

var caves = Parse(input);

Console.WriteLine("Solving puzzle 1...");
var paths = FindPaths(caves);
Console.WriteLine($"There are {paths.Count} ways through the cave system");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");

IList<Path> FindPaths(IList<Cave> caves){
    var start = caves.Single(c => c.Id == "start");

    var paths = new List<Path>();
    var p = new Path(paths);
    p.FindPath(start);

    return paths;
}

IList<Cave> Parse(string[] input) {
    var result = new List<Cave>();
    foreach (var line in input) {
        var split = line.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var (start, end) = (GetOrCreateCave(split[0]), GetOrCreateCave(split[1]));
        start.Neighbours.Add(end);
        end.Neighbours.Add(start);
    }

    return result;

    Cave GetOrCreateCave(string id) {
        var cave = result.FirstOrDefault(x => x.Id == id);
        if (cave == null) {
            cave = new Cave(id);
            result.Add(cave);
        }
        return cave;
    }
}

class Cave
{
    public Cave(string id) {
        Id = id;
    }
    
    public string Id { get; }
    public List<Cave> Neighbours { get; } = new List<Cave>();

    public bool IsBig => Id.All(c => char.IsUpper(c));
    public bool IsSmall => !IsBig;
}

class Path
{
    readonly List<Path> paths;
    HashSet<Cave> path;

    public Path(List<Path> paths, IEnumerable<Cave>? path = null)
    {
        this.path = new HashSet<Cave>(path ?? Array.Empty<Cave>());
        this.paths = paths;
    }

    public void FindPath(Cave cave) {
        var current = new HashSet<Cave>(path);
        foreach(var n in cave.Neighbours) {
            if (n.IsSmall && path.Contains(n)) continue;
            if (n.Id == "end") { 
                path.Add(n);
                paths.Add(this);
                path = new HashSet<Cave>(current);
                continue;
            }
            path.Add(cave);
            new Path(paths, path).FindPath(n);
        }
    }
}