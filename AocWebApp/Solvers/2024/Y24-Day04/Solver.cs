using System.Collections;
using System.Text;
using Common;
using Common.Solver;
using Serilog;

namespace Y24_Day04;

[Day(2024, 4, "Ceres Search")]
public class Solver : SolverBase
{
    private readonly string[] puzzle;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        puzzle = inputLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
    }

    public override Task Solve1()
    {
        const string xmas = "XMAS";

        logger.Information("Searching for XMAS...");

        List<(int dr, int dc)[]> offsets =
        [
            [(-1, 0), (-2, 0), (-3, 0)], // N
            [(-1, 1), (-2, 2), (-3, 3)], // NE
            [(0, 1), (0, 2), (0, 3)], // E
            [(1, 1), (2, 2), (3, 3)], // SE
            [(1, 0), (2, 0), (3, 0)], // S
            [(1, -1), (2, -2), (3, -3)], // SW
            [(0, -1), (0, -2), (0, -3)], // W
            [(-1, -1), (-2, -2), (-3, -3)] // NW
        ];

        var count = 0;

        var maxRow = puzzle.Length - 1;
        var maxCol = puzzle[0].Length - 1;

        for (var row = 0; row <= maxRow; row++)
        {
            for (var col = 0; col <= maxCol; col++)
            {
                if (puzzle[row][col] != 'X') continue;

                foreach (var offset in offsets)
                {
                    var sb = new StringBuilder("X");
                    foreach (var (dr, dc) in offset)
                    {
                        var newRow = row + dr;
                        var newCol = col + dc;

                        if (newRow < 0 || newRow > maxRow || newCol < 0 || newCol > maxCol)
                        {
                            break;
                        }

                        sb.Append(puzzle[newRow][newCol]);
                    }

                    if (sb.ToString() == xmas)
                    {
                        count++;
                    }
                }
            }
        }

        logger.Information("Found {Count} XMAS", count);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Looking for X-MAS...");

        var count = 0;

        foreach (var subArray in new SubArrays(puzzle, 3, 3))
        {
            var d1 = new StringBuilder()
                .Append(subArray[0][0])
                .Append(subArray[1][1])
                .Append(subArray[2][2])
                .ToString();

            var d2 = new StringBuilder()
                .Append(subArray[0][2])
                .Append(subArray[1][1])
                .Append(subArray[2][0])
                .ToString();

            if (d1 is "MAS" or "SAM" && d2 is "MAS" or "SAM") count++;
        }

        logger.Information("Found {count} X-MAS", count);

        return Task.CompletedTask;
    }
}

internal class SubArrays(IReadOnlyList<string> array, int rows, int cols) : IEnumerable<string[]>
{
    private readonly Enumerator enumerator = new(array, rows, cols);

    public IEnumerator<string[]> GetEnumerator() => enumerator;

    IEnumerator IEnumerable.GetEnumerator() => enumerator;

    private class Enumerator(IReadOnlyList<string> array, int rows, int cols) : IEnumerator<string[]>
    {
        private readonly int arrayRows = array.Count;
        private readonly int arrayCols = array[0].Length;

        private int row;
        private int col = -1;

        public bool MoveNext()
        {
            if (++col > arrayCols - cols)
            {
                col = 0;
                row++;
            }

            return row + rows <= arrayRows;
        }

        public void Reset()
        {
            row = 0;
            col = -1;
        }

        public string[] Current => GetCurrent();

        object IEnumerator.Current => Current;

        private string[] GetCurrent() => array.Skip(row).Take(rows).Select(r => r[col..(col + cols)]).ToArray();

        public void Dispose() { }
    }
}