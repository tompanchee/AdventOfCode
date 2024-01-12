using Common;
using Common.Solver;
using Serilog;

namespace Day16;

[Day(2023, 16, "The Floor Will Be Lava")]
internal class Solver : SolverBase
{
    private readonly string[] cave;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        logger.Information("Scanning cave...");
        cave = inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
    }

    public override Task Solve1()
    {
        logger.Information("Tracing rays...");
        var count = Trace(new Ray((-1, 0), Ray.EDirection.East));

        logger.Information("Finally there are {count} energized tiles", count);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Finding max number of energized tiles...");

        var max = int.MinValue;

        // Top 
        for (var x = 0; x < cave[0].Length; x++)
        {
            var count = Trace(new Ray((x, -1), Ray.EDirection.South));
            if (count > max) max = count;
        }

        // Bottom 
        for (var x = 0; x < cave[0].Length; x++)
        {
            var count = Trace(new Ray((x, cave.Length), Ray.EDirection.North));
            if (count > max) max = count;
        }

        // Left
        for (var y = 0; y < cave.Length; y++)
        {
            var count = Trace(new Ray((-1, y), Ray.EDirection.East));
            if (count > max) max = count;
        }

        // Right
        for (var y = 0; y < cave.Length; y++)
        {
            var count = Trace(new Ray((cave[0].Length, y), Ray.EDirection.West));
            if (count > max) max = count;
        }

        logger.Information("Max count {count} of energized tiles is", max);

        return Task.CompletedTask;
    }

    private int Trace(Ray start)
    {
        var rays = new List<Ray> { start };

        var energizedTiles = new HashSet<(int x, int y)>();
        var alreadyTraced = new HashSet<((int x, int y), Ray.EDirection)>();

        while (rays.Any())
        {
            var raysToRemove = new List<Ray>();
            var raysToAdd = new List<Ray>();
            foreach (var ray in rays)
            {
                ray.Move();
                if (ray.IsOutOfBounds(cave))
                {
                    raysToRemove.Add(ray);
                    continue;
                }

                if (alreadyTraced.Contains((ray.Position, ray.Direction)))
                {
                    raysToRemove.Add(ray);
                    continue;
                }

                energizedTiles.Add(ray.Position);
                alreadyTraced.Add((ray.Position, ray.Direction));

                if (cave[ray.Position.y][ray.Position.x] == '-' && (ray.Direction is Ray.EDirection.North or Ray.EDirection.South))
                {
                    ray.Direction = Ray.EDirection.East;
                    raysToAdd.Add(new Ray(ray.Position, Ray.EDirection.West));
                }

                if (cave[ray.Position.y][ray.Position.x] == '/' || cave[ray.Position.y][ray.Position.x] == '\\')
                {
                    ray.Turn(cave[ray.Position.y][ray.Position.x]);
                }

                if (cave[ray.Position.y][ray.Position.x] == '-' && (ray.Direction is Ray.EDirection.North or Ray.EDirection.South))
                {
                    ray.Direction = Ray.EDirection.East;
                    raysToAdd.Add(new Ray(ray.Position, Ray.EDirection.West));
                }

                if (cave[ray.Position.y][ray.Position.x] == '|' && (ray.Direction is Ray.EDirection.East or Ray.EDirection.West))
                {
                    ray.Direction = Ray.EDirection.North;
                    raysToAdd.Add(new Ray(ray.Position, Ray.EDirection.South));
                }
            }

            foreach (Ray ray in raysToRemove)
            {
                rays.Remove(ray);
            }

            rays.AddRange(raysToAdd);
        }

        return energizedTiles.Count;
    }
}