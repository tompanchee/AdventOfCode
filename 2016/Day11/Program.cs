using Day11;

Console.WriteLine("Populating floors...");
var floors = new List<Floor>();
foreach (var line in File.ReadAllLines("input.txt")) {
    if (string.IsNullOrWhiteSpace(line)) {
        break;
    }

    var floor = new Floor();
    // Stupid but easy parsing :)
    if (line.Contains("thulium generator")) {
        floor.Items.Add(Item.TG);
    }

    if (line.Contains("thulium-compatible")) {
        floor.Items.Add(Item.TM);
    }

    if (line.Contains("plutonium generator")) {
        floor.Items.Add(Item.PlG);
    }

    if (line.Contains("plutonium-compatible")) {
        floor.Items.Add(Item.PlM);
    }

    if (line.Contains("strontium generator")) {
        floor.Items.Add(Item.SG);
    }

    if (line.Contains("strontium-compatible")) {
        floor.Items.Add(Item.SM);
    }

    if (line.Contains("promethium generator")) {
        floor.Items.Add(Item.PrG);
    }

    if (line.Contains("promethium-compatible")) {
        floor.Items.Add(Item.PrM);
    }

    if (line.Contains("ruthenium generator")) {
        floor.Items.Add(Item.RG);
    }

    if (line.Contains("ruthenium-compatible")) {
        floor.Items.Add(Item.RM);
    }

    floors.Add(floor);
}

var state = new State(floors.ToArray());

Console.WriteLine("Solving part 1...");
var shortestPath = GetShortestPathLength(state, 10);
Console.WriteLine($"Everything can be moved to floor 4 in {shortestPath} steps");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
state.Floors[0].Items.Add(Item.EG);
state.Floors[0].Items.Add(Item.EM);
state.Floors[0].Items.Add(Item.DG);
state.Floors[0].Items.Add(Item.DM);
shortestPath = GetShortestPathLength(state, 14);
Console.WriteLine($"Everything can be moved to floor 4 in {shortestPath} steps");

int GetShortestPathLength(State start, int finalUpperFloorCount) {
    var queue = new Queue<WorkItem>();
    var visitedStates = new HashSet<long>();

    queue.Enqueue(new WorkItem(start, 0));

    while (queue.Count > 0) {
        var current = queue.Dequeue();

        if (current.State.Floors.Last().Items.Count == finalUpperFloorCount) {
            return current.Count;
        }

        foreach (var allowedState in current.State.GetNextAllowedStates()) {
            var hash = allowedState.GetHash();
            if (!visitedStates.Contains(hash)) {
                visitedStates.Add(hash);
                queue.Enqueue(new WorkItem(allowedState, current.Count + 1));
            }
        }
    }

    return -1;
}

internal class WorkItem
{
    public WorkItem(State state, int count) {
        State = state;
        Count = count;
    }

    public State State { get; set; }
    public int Count { get; set; }
}