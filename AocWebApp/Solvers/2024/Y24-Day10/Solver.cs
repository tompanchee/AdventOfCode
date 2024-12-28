using Common;
using Common.Solver;
using Common.Utils.Grid2D;
using Serilog;

namespace Y24_Day10;

[Day(2024, 10, "Hoof It")]
public class Solver : SolverBase
{
    private Grid map;
    
    public Solver(string input, ILogger logger) : base(input, logger)
    {
        map = new Grid(inputLines.Where(l=> !string.IsNullOrWhiteSpace(l)).ToArray());
    }

    public override Task Solve1()
    {
        logger.Information("Counting trailhead scores...");

        var heads = map.Where(p => p.value == '0').ToList();
        var score = heads.Sum(head => CountScore(head.point));

        logger.Information($"Total trailhead score is {score}");
        
        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Counting trailhead ratings...");

        var heads = map.Where(p => p.value == '0').ToList();
        var score = heads.Sum(head => CountRating(head.point));

        logger.Information($"Total trailhead ratings is {score}");
        
        return Task.CompletedTask;
    }

    private int CountScore(Point head)
    {
        var endsReached = new HashSet<Point>();
        FindEnds(head);
        return endsReached.Count;

        void FindEnds(Point pos)
        {
            var neighbours = map.GetNeighbours(pos)
                .Where(n => map[n] == map[pos] + 1
                            && !endsReached.Contains(n))
                .ToList();

            if (neighbours.Count == 0) return;

            foreach (var neighbour in neighbours)
            {
                if (map[neighbour] == '9') endsReached.Add(neighbour);
                FindEnds(neighbour);
            }
        }
        
    }
    
    private int CountRating(Point pos, int rating = 0)
    {
        var neighbours = map.GetNeighbours(pos)
            .Where(n => map[n] == map[pos] + 1)
            .ToList();
        
        return neighbours.Count == 0 
            ? rating 
            : neighbours.Sum(n => CountRating(n, rating + (map[n] == '9' ? 1 : 0)));
    }
}
