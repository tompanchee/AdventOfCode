using AOCCommon;
using Serilog.Core;

namespace Day17;

[Day(17, "Pyroclastic Flow")]
internal class Solver : SolverBase
{
    readonly List<byte> chamber = new() {
        0b11111111 // Floor
    };

    int instructionPos;

    string? jetPattern;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Dropping rocks...");

        for (var i = 0; i < 2022; i++) {
            var shape = Shapes.GetShape(i);
            DropRock(shape);
        }

        logger.Information("The tower is {0} units tall", chamber.Count - 1);
    }

    public override void Solve2() {
        logger.Information("Dropping even more rocks...");

        //Reset state
        chamber.Clear();
        chamber.Add(0b11111111);
        instructionPos = 0;

        List<(int shapeIdx, int pos, int height)> statuses = new();

        var count = 0;
        var found = false;
        (int shapeIdx, int pos, int height) start = default;
        var startIndex = 0;
        while (!found) {
            var shape = Shapes.GetShape(count);
            DropRock(shape);

            var shapeIdx = count % Shapes.SHAPE_COUNT;
            var status = (shapeIdx, pos: instructionPos, height: chamber.Count - 1);
            start = statuses.LastOrDefault(s => s.pos == status.pos && s.shapeIdx == shapeIdx);
            if (start != default) {
                startIndex = statuses.FindLastIndex(s => s.pos == status.pos && s.shapeIdx == shapeIdx);
                // Check if sequence is found
                var r1 = status.height;
                var r2 = start.height;

                var c = Math.Min(r2 - 1, r1 - r2);

                found = true;

                for (var i = 0; i < c; i++)
                    if (chamber[r1 - i] != chamber[r2 - i]) {
                        found = false;
                        break;
                    }
            }

            statuses.Add(status);
            count++;
        }

        var endIndex = statuses.Count - 1;
        var end = statuses.Last();

        var beforeSequenceHeight = statuses[startIndex - 1].height;

        var sequenceLength = endIndex - startIndex;
        var sequenceHeight = end.height - start.height;
        var rocksToDrop = 1000000000000 - (startIndex + 1);
        var numberOfSequences = rocksToDrop / sequenceLength;

        var remainingRocks = rocksToDrop % sequenceLength;
        var remainingHeight = statuses[startIndex + (int) remainingRocks].height - statuses[startIndex].height;

        var total = beforeSequenceHeight + numberOfSequences * sequenceHeight + remainingHeight;

        logger.Information("The tower is {0} units tall", total);
    }

    protected override void PostConstruct() {
        logger.Information("Reading jet pattern...");

        jetPattern = data[0];
        //jetPattern = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>"; // Test data
    }

    void DropRock(byte[] shape) {
        var topRock = chamber.Count - 1;
        var rockBottom = topRock + 4;
        var settled = false;
        while (!settled) {
            var instruction = GetNextInstruction();
            if (instruction == '<') MoveLeft();
            else MoveRight();
            MoveDown();
        }

        void MoveLeft() {
            // Check for edges
            if (shape.Any(s => (s & 64) == 64)) return; // Already at edge

            // Shift to new position
            var newShape = new byte[shape.Length];
            for (var i = 0; i < shape.Length; i++) newShape[i] = (byte) (shape[i] << 1);

            // Check for overlaps with existing rows
            if (IsOverlapping(newShape, rockBottom)) return;

            shape = newShape;
        }

        void MoveRight() {
            // Check for edges
            if (shape.Any(s => (s & 1) == 1)) return; // Already at edge
            //if (rockBottom > chamberHeight) return; // Not yet at bottom

            // Shift to new position
            var newShape = new byte[shape.Length];
            for (var i = 0; i < shape.Length; i++) newShape[i] = (byte) (shape[i] >> 1);

            // Check for overlaps with existing rows
            if (IsOverlapping(newShape, rockBottom)) return;

            shape = newShape;
        }

        void MoveDown() {
            var newBottom = rockBottom - 1;
            // Test for overlaps
            if (IsOverlapping(shape, newBottom)) {
                settled = true;
                for (var i = 0; i < shape.Length; i++)
                    if (rockBottom + i <= topRock) chamber[rockBottom + i] = (byte) (chamber[rockBottom + i] | shape[i]);
                    else chamber.Add(shape[i]);
                return;
            }

            rockBottom = newBottom;
        }

        bool IsOverlapping(IReadOnlyList<byte> s, int bottom) {
            for (var i = 0; i < topRock - bottom + 1; i++) {
                if (i >= shape.Length) return false;
                if ((chamber[bottom + i] & s[i]) != 0) return true;
            }

            return false;
        }
    }

    char GetNextInstruction() {
        var i = jetPattern![instructionPos++];
        if (instructionPos >= jetPattern.Length) instructionPos = 0;
        return i;
    }
}