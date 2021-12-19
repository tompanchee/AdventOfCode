using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Graph<T>
    {
        private readonly IDictionary<T, Vertex<T>> vertices;
        private readonly int? logInterval;
        private int visitedNodes;

        public Graph(IList<Vertex<T>> vertices, int? logInterval = null) {
            this.logInterval = logInterval;
            this.vertices = vertices.ToDictionary(v => v.Id, v => v);
        }

        // A* algorithm
        public (int lenght, List<T> path) FindShortestPath(T start, T end, Func<T,T,int> calculateHeuristicDistance) {
            var workData = SetupWorkData(start, end, calculateHeuristicDistance);
            var workQueue = new Dictionary<T, WorkVertex> {{start, workData[start] }};
            while (true) {
                if (logInterval.HasValue && visitedNodes % logInterval == 0) Console.WriteLine($"Visited {visitedNodes} nodes");
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

        private IDictionary<T, WorkVertex> SetupWorkData(T start, T end, Func<T, T, int> calculateHeuristicDistance) {
            var result = vertices.Keys.ToDictionary(id=>id, id => new WorkVertex {
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

    class Vertex<T>
    {
        public Vertex(T id) {
            Id = id;
        }

        public T Id { get; }

        public IList<(int cost, T vertex)> Neighbours { get; } = new List<(int cost, T vertex)>();
    }
}