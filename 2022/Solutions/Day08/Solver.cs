using AOCCommon;
using Serilog.Core;

namespace Day08;

[Day(8, "Treetop Tree House")]
internal class Solver : SolverBase
{
    const int ZERO = '0';

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        var visibleTrees = new HashSet<(int row, int col)>();

        for (var row = 0; row < data.Length; row++)
        for (var col = 0; col < data[row].Length; col++)
            if (IsEdge(row, col)
                || IsVisibleLeft(row, col)
                || IsVisibleRight(row, col)
                || IsVisibleTop(row, col)
                || IsVisibleBottom(row, col))
                visibleTrees.Add((row, col));

        logger.Information("A total of {0} trees are visible", visibleTrees.Count);

        bool IsVisibleLeft(int row, int col) {
            var height = data[row][col] - ZERO;
            for (var c = col - 1; c >= 0; c--)
                if (data[row][c] - ZERO >= height)
                    return false;

            return true;
        }

        bool IsVisibleRight(int row, int col) {
            var height = data[row][col] - ZERO;
            for (var c = col + 1; c < data[row].Length; c++)
                if (data[row][c] - ZERO >= height)
                    return false;

            return true;
        }

        bool IsVisibleTop(int row, int col) {
            var height = data[row][col] - ZERO;
            for (var r = row - 1; r >= 0; r--)
                if (data[r][col] - ZERO >= height)
                    return false;

            return true;
        }

        bool IsVisibleBottom(int row, int col) {
            var height = data[row][col] - ZERO;
            for (var r = row + 1; r < data.Length; r++)
                if (data[r][col] - ZERO >= height)
                    return false;

            return true;
        }
    }

    public override void Solve2() {
        var maxScenicScore = -1;

        for (var row = 0; row < data.Length; row++)
        for (var col = 0; col < data[row].Length; col++) {
            var scenicScore = CalculateScenicScore(row, col);
            if (scenicScore > maxScenicScore) maxScenicScore = scenicScore;
        }

        logger.Information("Max scenic score for any tree is {0}", maxScenicScore);

        int CalculateScenicScore(int row, int col) {
            var deltas = new (int dr, int dc)[] {
                (0, -1),
                (0, 1),
                (-1, 0),
                (1, 0)
            };

            if (IsEdge(row, col)) return 0;

            var height = data[row][col] - ZERO;
            var score = 1;
            foreach (var (dr, dc) in deltas) {
                var r = row;
                var c = col;
                var visibleCount = 1;
                while (true) {
                    r += dr;
                    c += dc;

                    if (data[r][c] - ZERO >= height || IsEdge(r, c)) break;
                    visibleCount++;
                }

                score *= visibleCount;
            }

            return score;
        }
    }

    bool IsEdge(int row, int col) {
        return row == 0 || col == 0 || row == data.Length - 1 || col == data[0].Length - 1;
    }

    protected override void PostConstruct() {
        logger.Information("Looking at trees...");

        //data = new[] {
        //    "30373",
        //    "25512",
        //    "65332",
        //    "33549",
        //    "35390"
        //};
    }
}