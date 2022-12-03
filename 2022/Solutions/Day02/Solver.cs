using AOCCommon;
using Serilog.Core;

namespace Day02;

[Day(2, "Rock Paper Scissors")]
public class Solver : SolverBase
{
    readonly List<(Shape p1, Shape p2)> guide = new ();
    
    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1()
    {
        logger.Information("Playing game...");

        var totalPoints = 0;
        foreach(var (p1, p2) in guide)
        {
            totalPoints += CalculatePointsForRound(p1, p2);
        }

        logger.Information("Total points scored is {totalPoints}", totalPoints);
    }

    public override void Solve2()
    {
        logger.Information("Playing game...");

        var totalPoints = 0;
        foreach (var (p1, p2) in guide)
        {
            Shape realP2 = ResolveRealShape(p1, p2);
            totalPoints += CalculatePointsForRound(p1, realP2);
        }

        logger.Information("Total points scored interpreting the guide correctly is {totalPoints}", totalPoints);
    }

    static Shape ResolveRealShape(Shape p1, Shape p2)
    {
        // X Lose (parsed guide Rock)
        // Y Draw (Paper)
        // Z Win (Scissors)

        if (p1 == Shape.Rock)
        {
            if (p2 == Shape.Rock) return Shape.Scissors;
            if (p2 == Shape.Paper) return Shape.Rock;
            return Shape.Paper;
        }

        if (p1 == Shape.Paper) 
        {
            if (p2 == Shape.Rock) return Shape.Rock;
            if (p2 == Shape.Paper) return Shape.Paper;
            else return Shape.Scissors;
        }

        // p1 == Scissors
        if (p2 == Shape.Rock) return Shape.Paper;
        if (p2 == Shape.Paper) return Shape.Scissors;
        return Shape.Rock;
    }

    static int CalculatePointsForRound(Shape p1, Shape p2)
    {
        var shapePoints = new Dictionary<Shape, int>
        {
            { Shape.Rock, 1 },
            { Shape.Paper, 2 },
            { Shape.Scissors, 3 }
        };

        int points = 3; // Draw
        if (p1 == Shape.Rock)
        {
            if (p2 == Shape.Paper) points = 6;
            if (p2 == Shape.Scissors) points = 0;
        }

        if (p1 == Shape.Paper)
        {
            if (p2 == Shape.Scissors) points = 6;
            if (p2 == Shape.Rock) points = 0;
        }

        if (p1 == Shape.Scissors)
        {
            if (p2 == Shape.Rock) points = 6;
            if (p2 == Shape.Paper) points = 0;
        }

        return points + shapePoints[p2];
    }

    protected override void PostConstruct()
    {
        logger.Information("Reading strategy guide...");
        
        var shapes = new Dictionary<string, Shape>
        {
            {"A", Shape.Rock},
            {"B", Shape.Paper},
            {"C", Shape.Scissors},
            {"X", Shape.Rock},
            {"Y", Shape.Paper},
            {"Z", Shape.Scissors},
        };

        foreach(var row in data)
        {
            var split = row.Split(' ');
            guide.Add((shapes[split[0]], shapes[split[1]]));
        }
    }

    enum Shape
    {
        Rock,
        Paper,
        Scissors
    }
}
