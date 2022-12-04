using AOCCommon;
using Serilog.Core;

namespace Day04;

[Day(4, "Camp Cleanup")]
public class Solver : SolverBase
{
    readonly List<(List<int> a1, List<int> a2)> sectionAssignments = new();
    
    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1()
    {
        var count = 0;
        foreach(var (a1, a2) in sectionAssignments)
        {
            if ( !a1.Except(a2).Any() 
                || !a2.Except(a1).Any() )
            {
                count++;
            }
        }

        logger.Information("There are {count} assignment pairs fully contiaining the other", count);
    }

    public override void Solve2()
    {
        var count = sectionAssignments.Where(a => a.a1.Intersect(a.a2).Any()).Count();

        logger.Information("There are {count} overlapping assignment pairs", count);
    }

    protected override void PostConstruct()
    {
        logger.Information("Checking assignments...");

        foreach(var row in data)
        {
            var split = row.Split(',');
            sectionAssignments.Add((GetRange(split[0]), GetRange(split[1])));
        }
    }

    private static List<int> GetRange(string range)
    {
        var split=range.Split("-");
        var start = int.Parse(split[0]);
        var end = int.Parse(split[1]);
        return Enumerable.Range(start, end-start+1).ToList();
    }
}
