using Day12;

IDictionary<int, Node> nodes = new Dictionary<int, Node>();
List<List<int>> nodeGroups = new List<List<int>>();

Console.WriteLine("Scanning programs...");
ParseInput();

Console.WriteLine("Grouping graphs...");
GroupGraphs();

Console.WriteLine("Solving part 1...");
var zeroGroup = nodeGroups.Single(g=>g.Contains(0));
Console.WriteLine($"There are {zeroGroup.Count} programs in the group having 0.");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
Console.WriteLine($"There {nodeGroups.Count} groups in total.");

void ParseInput()
{
    var input = File.ReadAllLines("input.txt");

    foreach (var line in input)
    {
        if (string.IsNullOrWhiteSpace(line)) continue;

        var split = line.Split("<->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var nodeId = int.Parse(split[0]);

        var node = GetOrCreateNode(nodeId);
        var neighbourIds = split[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);

        foreach (var neighbourId in neighbourIds)
        {
            var neighbour = GetOrCreateNode(neighbourId);
            node.Neighbours.Add(neighbour);
        }
    }

    Node GetOrCreateNode(int nodeId)
    {
        if (nodes.ContainsKey(nodeId)) return nodes[nodeId];

        var n = new Node(nodeId);
        nodes.Add(nodeId, n);

        return n;
    }
}

void GroupGraphs()
{
    foreach (var node in nodes.Values)
    {
        if (IsInGroup(node.Id)) continue;
        
        var group = CreateNodeGroup(node.Id);

        PopulateGroup(group, node.Id);
    }

    List<int> CreateNodeGroup(int nodeId)
    {
        var g = new List<int>() { nodeId };
        nodeGroups.Add(g);

        return g;
    }

    bool IsInGroup(int nodeId) => nodeGroups.Any(g=>g.Contains(nodeId));

    void PopulateGroup(List<int> group, int id)
    {
        // BFS to fill node group
        var node = nodes[id];

        var visited = new HashSet<int>();
        var queue = new Queue<Node>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visited.Add(current.Id);

            if (!group.Contains(current.Id)) { 
                group.Add(current.Id);
            }

            foreach(var n in current.Neighbours)
            {
                if (visited.Contains(n.Id)) continue;
                queue.Enqueue(n);
            }
        }
    }
}
