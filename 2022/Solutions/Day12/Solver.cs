using AOCCommon;
using AOCUtils.Graphs;
using Serilog.Core;

namespace Day12;

[Day(12, "Hill Climbing Algorithm")]
internal class Solver : SolverBase
{
    Graph<(int row, int col)>? hills;
    (int row, int col)? start;
    (int row, int col)? end;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        var algorithm = AStar<(int, int)>.Init(hills!);
        var (length, _) = algorithm.FindShortestPath(start!.Value, end!.Value);

        logger.Information("Shortest path to best signal is {0} steps long", length);
    }

    public override void Solve2() {
        var algorithm = AStar<(int, int)>.Init(hills!);

        var minDistance = int.MaxValue;
        for (var row = 0; row < data.Length; row++) {
            if (string.IsNullOrWhiteSpace(data[row])) continue;
            for (var col = 0; col < data[row].Length; col++) {
                if (data[row][col] == 'a') {
                    start = (row, col);
                    try {
                        var (length, _) = algorithm.FindShortestPath(start!.Value, end!.Value);
                        if (length < minDistance) minDistance = length;
                    } catch {
                        // ignored, no path found
                    }
                }
            }
        }

        logger.Information("Shortest path from any low point to best signal is {0} steps long", minDistance);
    }

    protected override void PostConstruct() {
        logger.Information("Mapping the hills...");

        (int dr, int dc)[] offsets = {
            (-1, 0),
            (0, 1),
            (1, 0),
            (0, -1)
        };
        var vertices = new List<Vertex<(int row, int col)>>();

        for (var row = 0; row < data.Length; row++) {
            if (string.IsNullOrWhiteSpace(data[row])) continue;
            for (var col = 0; col < data[row].Length; col++) vertices.Add(new Vertex<(int row, int col)>((row, col)));
        }

        foreach (var vertex in vertices)
        {
            foreach (var (dr, dc) in offsets)
            {
                var nr = vertex.Id.row + dr;
                var nc = vertex.Id.col + dc;

                if (nr < 0 || nr > data.Length - 1 || nc < 0 || nc > data[0].Length - 1) continue;
                if (string.IsNullOrWhiteSpace(data[nr])) continue;

                var height = data[vertex.Id.row][vertex.Id.col];
                var nheight = data[nr][nc];

                if (height == 'S') {
                    start = (vertex.Id.row, vertex.Id.col);
                    height = 'a';
                }

                if (height == 'E')
                {
                    end = (vertex.Id.row, vertex.Id.col);
                    height = 'z';
                }

                var cost = nheight - height;
                if (cost > 1) continue;

                vertex.Neighbours.Add((1, (nr, nc))); // Use same cost for all available neighbours
            }
        }

        hills = new Graph<(int row, int col)>(vertices);
    }
}