using System.Text;
using Common;
using Common.Solver;
using Common.Utils.Grid2D;
using Serilog;
using Serilog.Events;

namespace Y24_Day06;

[Day(2024, 6, "Guard Gallivant")]
public class Solver:SolverBase
{
    private readonly Grid map;
    private readonly Point start = null!;
    
    IEnumerable<Point> route = null!;
    
    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Reading map...");
        map = new Grid(inputLines.Where(l=>!string.IsNullOrWhiteSpace(l)).ToArray());

        for (var i = 0; i < map.Rows.Length; i++)
        {
            var idx = map.Rows[i].IndexOf('^');
            if (idx > -1)
            {
                start = new Point(idx, i);
                break;
            }
        }
    }

    public override Task Solve1()
    {
        logger.Information("Walking the guard...");

        var guard = new Guard(start, Orientation.North, map);
        var visited = new HashSet<Point> { start };

        bool inMap;
        do
        {
            inMap = guard.Move();
            logger.Debug("Position: ({x},{y})", guard.Pose.Location.X, guard.Pose.Location.Y);
            if (inMap) visited.Add(guard.Pose.Location);
        } while (inMap);

        route = visited.Skip(1); // store for part 2 without start
        
        logger.Information($"Guard visited {visited.Count} locations");

        if (logger.IsEnabled(LogEventLevel.Debug))
        {
            var sb = new StringBuilder().AppendLine();
            for (var y = 0; y < map.Rows.Length; y++)
            {
                for (var x = 0; x < map.Rows[0].Length; x++)
                {
                    if (visited.Contains(new Point(x, y))) sb.Append('X');
                    else sb.Append('.');
                }

                sb.AppendLine();
            }
            logger.Debug(sb.ToString());
        }

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Checking loop positions...");
        var count = 0;

        foreach (var point in route)
        {
            var origRow = map.Rows[point.Y];
            var array = origRow.ToArray();
            array[point.X] = '#';
            map.Rows[point.Y] = new string(array);

            var guard = new Guard(start, Orientation.North, map);
            var visited = new HashSet<Pose> { guard.Pose };
            
            bool inMap;
            do
            {
                inMap = guard.Move();
                logger.Debug("Position: ({x},{y} - Orientation: {orientation})", guard.Pose.Location.X, guard.Pose.Location.Y, guard.Pose.Orientation);
                if (inMap && visited.Contains(guard.Pose))
                {
                    // In loop
                    count++;
                    break;
                }
                if (inMap) visited.Add(guard.Pose);
            } while (inMap);

            map.Rows[point.Y] = origRow;
        }
        
        logger.Information("There are {count} positions for obstacles for the guard to loop", count);
        
        return Task.CompletedTask;
    }
}

class Guard(Point position, Orientation orientation, Grid map)
{
    public Pose Pose{ get; private set; } = new(position, orientation);

    public bool Move()
    {
        var newPos = Pose.Location.Move(Pose.Orientation);
        if (!map.Contains(newPos)) return false;

        if (map[newPos] == '#')
        {
            Pose = Pose.TurnRight();
            Move();
            return true;
        }
        
        Pose = new Pose(newPos, Pose.Orientation);
        
        return true;
    }
}
