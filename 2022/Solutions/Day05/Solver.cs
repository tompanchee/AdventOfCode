using AOCCommon;
using Serilog.Core;
using System.Data;

namespace Day05;

[Day(5, "Supply Stacks")]
public class Solver : SolverBase
{
    readonly List<Stack<char>> stacks = new();
    readonly List<Instructions> instructions = new();
    
    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1()
    {
        foreach(var instruction in instructions)
        {
            for(var i = 0; i < instruction.Amount; i++)
            {
                var crate = stacks[instruction.Source].Pop();
                stacks[instruction.Destination].Push(crate);
            }
        }

        var topCrates = new string(stacks.Select(s => { 
            if (s.TryPeek(out var c))
            {
                return c;
            }
            return ' ';
            })
            .ToArray());

        logger.Information("Crates at top of stacks are {topCrates}", topCrates);
    }

    public override void Solve2()
    {
        // Reset stacks
        BuildStacks();
        
        foreach (var instruction in instructions)
        {
            // Use temp stack to retain order
            var tempStack = new Stack<char>();
            for (var i = 0; i < instruction.Amount; i++)
            {
                if (stacks[instruction.Source].TryPop(out var crate))
                {
                    tempStack.Push(crate);
                }
            }

            while(tempStack.Count > 0)
            {
                stacks[instruction.Destination].Push(tempStack.Pop());
            }
        }

        var topCrates = new string(stacks.Select(s => {
            if (s.TryPeek(out var c))
            {
                return c;
            }
            return ' ';
        })
            .ToArray());

        logger.Information("Crates at top of stacks are {topCrates}", topCrates);
    }

    protected override void PostConstruct()
    {        
        BuildStacks();        
        ReadInstructions();
    }

    private void BuildStacks()
    {
        logger.Information("Building stacks...");
        stacks.Clear();

        stacks.Add(new Stack<char>()); // Push empty stack at 0 index (to ease indexing)

        var stackData = new List<string>();
        foreach(var row in data)
        {
            if (string.IsNullOrWhiteSpace(row)) break;
            stackData.Add(row);
        }

        var lastRow = stackData.Last();
        for(var col = 0; col < lastRow.Length; col++)
        {
            if (!char.IsNumber(lastRow[col])) continue;

            var newStack = new Stack<char>();            
            for(var row = stackData.Count - 2; row >= 0; row--)
            {
                if (stackData[row][col] == ' ') break;

                newStack.Push(stackData[row][col]);
            }
            
            stacks.Add(newStack);
        }
    }

    private void ReadInstructions()
    {
        logger.Information("Reading instructions...");
        foreach (var row in data)
        {
            // Skip non instruction rows
            if (!row.StartsWith("move")) continue;

            //move 3 from 2 to 9
            var split = row.Split("from");

            var amount = int.Parse(split[0][5..]);
            var source = int.Parse(split[1][1..2]);
            var destination = int.Parse(split[1][6..]);

            instructions.Add(new Instructions(amount, source, destination));
        }
    }

    record Instructions(int Amount, int Source, int Destination);
}
