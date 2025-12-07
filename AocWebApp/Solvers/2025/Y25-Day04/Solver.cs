using Common.Solver;
using Common;
using Common.Utils.Grid2D;
using Serilog;

namespace Y25_Day04;

[Day(2025, 4, "Printing Department")]
public class Solver : SolverBase
{
    private readonly Grid grid;
    
    public Solver(string input, ILogger logger) : base(input, logger)
    {
        grid = new Grid(inputLines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray());
    }

    public override Task Solve1()
    {
        logger.Information("Solving part 1...");
        var accessibleRolls = GetAccessibleRolls();

        logger.Information("Number of accessible rolls is {count}", accessibleRolls.Count);
        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Solving part 2...");

        var sum = 0;
        var accessibleRolls = GetAccessibleRolls();
        while (accessibleRolls.Count != 0)
        {
            sum += accessibleRolls.Count;
            foreach (var accessibleRoll in accessibleRolls)
            {
                grid.SetPoint(accessibleRoll, '.');
            }
            accessibleRolls = GetAccessibleRolls();
        }
        
        logger.Information("Number of accessible rolls is {sum}", sum);
        
        return Task.CompletedTask;
    }
    
    private HashSet<Point> GetAccessibleRolls()
    {
        var accessibleRolls = new HashSet<Point>();
        foreach (var (p, v) in grid)
        {
            if (v == '@')
            {
                var neighbours = grid.GetNeighbours(p, allowInterCardinal: true);
                var count = neighbours.Count(neighbour => grid[neighbour] == '@');
                if (count < 4)
                {
                    accessibleRolls.Add(p);
                }
            }
        }

        return accessibleRolls;
    }
}
