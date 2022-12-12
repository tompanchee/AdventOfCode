namespace AOCUtils.Graphs;

public class AStar<T> where T : notnull
{
    readonly Func<T, T, int> calculateHeuristicDistance;
    readonly IDictionary<T, Vertex<T>> vertices;
    int visitedNodes;

    AStar(Graph<T> graph, Func<T, T, int> calculateHeuristicDistance) {
        vertices = graph.Vertices.ToDictionary(v => v.Id, v => v);
        this.calculateHeuristicDistance = calculateHeuristicDistance;
    }

    public static AStar<T> Init(Graph<T> graph, Func<T, T, int>? calculateHeuristicDistance = null) {
        return new AStar<T>(graph, calculateHeuristicDistance ?? ((_, _) => 0));
    }

    public (int lenght, IList<T> path) FindShortestPath(T start, T end) {
        var workData = SetupWorkData(start, end);
        var workQueue = new Dictionary<T, WorkVertex> {{start, workData[start]}};
        while (true) {
            var current = workQueue.Values.OrderBy(v => v.Cost).First();
            if (current.Id.Equals(end)) break;
            foreach (var (cost, neighbour) in vertices[current.Id].Neighbours) {
                var work = workData[neighbour];
                if (work.Visited) continue;
                if (work.PathLength == int.MaxValue || work.PathLength > current.PathLength + cost) {
                    work.PathLength = current.PathLength + cost;
                    work.Previous = current;
                }

                if (!workQueue.ContainsKey(work.Id)) workQueue.Add(work.Id, work);
            }

            current.Visited = true;
            workQueue.Remove(current.Id);
            visitedNodes++;
        }

        var path = new List<T>();
        var c = workData[end];
        var length = c.PathLength;
        while (!c.Id.Equals(start)) {
            path.Add(c.Id);
            c = c.Previous;
        }

        path.Add(c.Id);
        path.Reverse();

        return (length, path);
    }

    IDictionary<T, WorkVertex> SetupWorkData(T start, T end) {
        var result = vertices.Keys.ToDictionary(id => id, id => new WorkVertex {
            Id = id,
            HeuristicDistance = calculateHeuristicDistance(end, id)
        });
        result[start].PathLength = 0;
        return result;
    }

    class WorkVertex
    {
        public T Id { get; init; }
        public int PathLength { get; set; } = int.MaxValue;
        public WorkVertex Previous { get; set; }
        public bool Visited { get; set; }
        public int HeuristicDistance { get; set; }
        public int Cost => PathLength == int.MaxValue ? int.MaxValue : PathLength + HeuristicDistance;
    }
}