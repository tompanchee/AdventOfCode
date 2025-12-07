using System.Text;
using Common.Solver;
using Serilog;
using Common;

namespace Y25_Day02;

[Day(2025, 2, "Gift Shop")]
public class Solver : SolverBase
{
    private readonly List<(long min, long max)> ranges = []; 
    
    public Solver(string input, ILogger logger) : base(input, logger)
    {
        var rs = input.Split(',');
        foreach (var r in rs)
        {
            var s = r.Split('-');
            ranges.Add((long.Parse(s[0]), long.Parse(s[1])));
        }       
    }

    // Brute force solution
    public override Task Solve1()
    {
        logger.Information("Solving part 1...");
        logger.Information("Sum of invalid ids is {sum}", CalculateInvalidSum(IsValid1));
        return Task.CompletedTask;
    }
    
    public override Task Solve2()
    {
        logger.Information("Solving part 2...");
        logger.Information("Sum of invalid ids is {sum}", CalculateInvalidSum(IsValid2));
        return Task.CompletedTask;
    }
    
    private static bool IsValid1(long number)
    {
        var s = number.ToString();
        var length = s.Length;
        var sequenceLength =  length/ 2;
        var sequence = s[..sequenceLength];
        var test = new StringBuilder().Insert(0, sequence, 2);
        return !test.Equals(s);
    }
    
    private static bool IsValid2(long number)
    {
        var s = number.ToString();
        var length = s.Length;
        var maxSequenceLength =  length/ 2;
        for (var sl = 1; sl <= maxSequenceLength; sl++)
        {
            var sequence = s[..sl];
            var test = new StringBuilder().Insert(0, sequence, length/sl);
            if (test.Equals(s)) return false;
        }
        
        return true;
    }

    long CalculateInvalidSum(Func<long, bool> isValid)
    {
        var sum = 0L;
        
        foreach ((long min, long max)  in ranges)
        {
            for (var i = min; i <= max; i++)
            {
                if (!isValid(i)) sum += i;
            }
        }

        return sum;
    }
}
