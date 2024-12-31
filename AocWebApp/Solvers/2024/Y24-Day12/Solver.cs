using System.Security.Cryptography;
using Common;
using Common.Solver;
using Common.Utils.Grid2D;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Y24_Day12;

[Day(2024, 12, "Garden Groups")]
public class Solver : SolverBase
{
    private readonly Grid garden;
    readonly List<Group> groups = [];
    
    public Solver(string input, ILogger logger) : base(input, logger)
    {
        garden = new Grid(inputLines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray());
        ResolveGroups();
    }

    private void ResolveGroups()
    {
        logger.Information("Scanning Garden Groups...");
        
        // Keep track of handled positions
        HashSet<Point> handledItems = [];

        foreach ((Point point, char value) pos in garden)
        {
            if(handledItems.Contains(pos.point)) continue;
            if (pos.value == '.') continue; // Skip . for troubleshooting
            
            var visited = new HashSet<Point>();
            var queue = new Queue<Point>();
            queue.Enqueue(pos.point);

            while (queue.Count > 0)
            {       
                var point = queue.Dequeue();
                // ReSharper disable once CanSimplifySetAddingWithSingleCall
                if (visited.Contains(point)) continue;
                visited.Add(point);
                
                var neighbours = garden.GetNeighbours(point)
                    .Where(p=> !visited.Contains(p) && garden[p] == pos.value);

                foreach (var neighbour in neighbours)
                {
                    queue.Enqueue(neighbour);
                }
            }
            
            groups.Add(new Group( visited.ToList(), garden));
            foreach (Point v in visited)
            {
                handledItems.Add(v);
            }
        }

        if (logger.IsEnabled(LogEventLevel.Debug))
        {
            foreach (Group group in groups)
            {
                logger.Debug("Plant: '{plant}', Area: {area}, Perimeter: {perimeter}, Sides: {sides}", 
                    group.Plant, 
                    group.Area,
                    group.PerimeterLength,
                    group.Sides);
            }
        }
    }

    public override Task Solve1()
    {
        logger.Information("Calculating total fence price...");
        logger.Information("Total fencing price is {sum}", groups.Select(g=>g.Price).Sum());

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Calculating total discounted fence price...");
        logger.Information("Total discounted fence price is {sum}", groups.Select(g=>g.DiscountedPrice).Sum());
        
        return Task.CompletedTask;
    }
}

class Group
{
    readonly List<Point> points;

    public Group(List<Point> points, Grid garden)
    {
        this.points = points;
        Plant = garden[points.First()];
        
        Perimeter = CalculatePerimeter(garden);
    }
    
    public char Plant { get; }
    public int Price => Area * PerimeterLength;
    public int DiscountedPrice => Area * Sides;
    public int Area => points.Count;
    public int PerimeterLength => Perimeter.Count;
    public int Sides => CalculateNumberOfSides();
    private List<Point> Perimeter { get; }
    
    private List<Point> CalculatePerimeter(Grid garden)
    {
        List<Point> perimeter = [];
        
        var visited = new HashSet<Point>();
        var queue = new Queue<Point>();
        queue.Enqueue(points[0]);

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();
            // ReSharper disable once CanSimplifySetAddingWithSingleCall
            if (visited.Contains(point)) continue;
            visited.Add(point);
            
            var neighbours = garden.GetNeighbours(point, true)
                .Where(n=> !visited.Contains(n));

            foreach (Point neighbour in neighbours)
            {
                if (!points.Contains(neighbour))
                {
                    perimeter.Add(neighbour);
                    continue;
                }
                
                queue.Enqueue(neighbour);
            }
        }
        
        return perimeter;
    }

    int CalculateNumberOfSides()
    {
        var sides = 0;
        
        // Make copy because points are removed from the copy
        var perimeterPoints = new List<Point>(Perimeter);

        while (perimeterPoints.Count > 0)
        {
            var point = perimeterPoints[0];
            var toRemove = new List<Point> { point };
            
            // Check each direction and remove all found points
            toRemove.AddRange(Remove(point, Orientation.North));
            toRemove.AddRange(Remove(point, Orientation.East));
            toRemove.AddRange(Remove(point, Orientation.South));
            toRemove.AddRange(Remove(point, Orientation.West));

            //var max = toRemove
            //    .Select(p => Perimeter.Count(pos => pos.Equals(p)))
            //    .Max();

            //if (max == 1 || max == 2) sides++;
            //else 
                //sides += max;

                sides++;

            foreach (var p in toRemove)
            {
                //while (perimeterPoints.Any(pos => pos.Equals(p)))
                //{
                    perimeterPoints.Remove(p);
                //}
            }
        }

        return sides;

        List<Point> Remove(Point p, Orientation direction)
        {
            var removed = new List<Point>();
            Point? current = p;

            while (current != null)
            {
                current = current.Move(direction);
                if (perimeterPoints.Contains(current))
                {
                    removed.Add(current);
                }
                else
                {
                    current = null;
                }
            }

            return removed;
        }
    }
}


