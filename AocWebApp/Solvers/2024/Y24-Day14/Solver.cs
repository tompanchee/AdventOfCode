using System.Text;
using Common;
using Common.Solver;
using Common.Utils.Grid2D;
using Serilog;

namespace Y24_Day14;

[Day(2024, 14, "Restroom Redoubt")]
public class Solver : SolverBase
{
    // Sample
    //private const int XSize = 11;
    //private const int YSize = 7;

    // Real input
    private const int XSize = 101;
    private const int YSize = 103;

    private const int XHalf = XSize / 2;
    private const int YHalf = YSize / 2;

    private readonly List<Robot> robots = [];

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        GetInitialState();
    }

    private void GetInitialState()
    {
        robots.Clear();

        foreach (string data in inputLines.Where(l => !string.IsNullOrWhiteSpace(l)))
        {
            robots.Add(Robot.Parse(data));
        }
    }

    public override Task Solve1()
    {
        logger.Information("Moving robots for 100s...");

        foreach (Robot robot in robots)
        {
            robot.Move(100, XSize, YSize);
        }

        int[] quadrantCount = QuadrantCount();
        int safetyFactor = quadrantCount[0] * quadrantCount[1] * quadrantCount[2] * quadrantCount[3];

        logger.Information("Safety factor is {sf}", safetyFactor);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Looking for easter egg...");

        GetInitialState();

        // Max number of iterations until back in initial state
        for (int i = 0; i < XSize * YSize; i++)
        {
            foreach (Robot robot in robots)
            {
                robot.Move(1, XSize, YSize);
            }

            Print(i + 1);
        }

        // Brute force manually when a Christmas tree is found
        logger.Information("Easter egg found after the number of iterations checked by a human from the output");

        return Task.CompletedTask;
    }

    private void Print(int iteration)
    {
        HashSet<Point> robotPos = new(robots.Select(r => r.Position).Distinct());

        StringBuilder sb = new();
        for (int y = 0; y < YSize; y++)
        {
            sb.AppendLine();
            for (int x = 0; x < XSize; x++)
            {
                if (robotPos.Contains(new Point(x, y)))
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }
            }
        }

        logger.Information("--------");
        logger.Information("At {iteration} iterations", iteration);
        logger.Information(sb.ToString());
        logger.Information("--------");
    }

    private int[] QuadrantCount()
    {
        int[] quadrantCount = [0, 0, 0, 0];

        foreach (Point pos in robots.Select(r => r.Position))
        {
            switch (pos)
            {
                case { X: < XHalf, Y: < YHalf }:
                    quadrantCount[0]++;
                    break;
                case { X: > XHalf, Y: < YHalf }:
                    quadrantCount[1]++;
                    break;
                case { X: < XHalf, Y: > YHalf }:
                    quadrantCount[2]++;
                    break;
                case { X: > XHalf, Y: > YHalf }:
                    quadrantCount[3]++;
                    break;
            }
        }

        return quadrantCount;
    }
}