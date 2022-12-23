using AOCCommon;
using Serilog.Core;

namespace Day22;

[Day(22, "Monkey Map")]
internal class Solver : SolverBase
{
    readonly List<string> map = new();
    Instructions? instructions;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Walking through the force field...");
        (int row, int col) pos = (0, map[0].IndexOf('.'));

        var walker = new Walker(pos, 'R', map);

        string? instruction;
        while ((instruction = instructions!.GetNext()) != null) walker.ExecuteInstruction(instruction);

        logger.Information("Password is {0}", walker.CalculatePassword());
    }

    public override void Solve2() { }

    protected override void PostConstruct() {
        logger.Information("Reading map...");

        //data = new[] {
        //    "        ...#",
        //    "        .#..",
        //    "        #...",
        //    "        ....",
        //    "...#.......#",
        //    "........#...",
        //    "..#....#....",
        //    "..........#.",
        //    "        ...#....",
        //    "        .....#..",
        //    "        .#......",
        //    "        ......#.",
        //    "",
        //    "10R5L5R10L4R5L5"
        //};

        var row = 0;
        while (!string.IsNullOrWhiteSpace(data[row])) {
            map.Add(data[row]);

            row++;
        }

        var maxLength = map.Select(r => r.Length).Max();
        for (var i = 0; i < map.Count; i++)
            if (map[i].Length < maxLength)
                map[i] += new string(Enumerable.Repeat(' ', maxLength - map[i].Length).ToArray());

        logger.Information("Reading path instructions...");
        instructions = new Instructions(data[row + 1]);
    }
}