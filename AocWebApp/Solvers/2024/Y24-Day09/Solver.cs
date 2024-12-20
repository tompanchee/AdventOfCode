using Common;
using Common.Solver;
using Serilog;

// ReSharper disable StringLiteralTypo

namespace Y24_Day09;

[Day(2024, 9, "Disk Fragmenter")]
public class Solver(string input, ILogger logger) : SolverBase(input, logger)
{
    public override Task Solve1()
    {
        logger.Information("Defragmenting...");

        List<int> data = [];

        var file = 0;
        for (int i = 0; i < input.Trim().Length; i++)
        {
            var value = input[i] - '0';

            var d = -1;
            if (i % 2 == 0)
            {
                d = file++;
            }

            for (int j = 0; j < value; j++)
            {
                data.Add(d);
            }
        }

        while (true)
        {
            var first = data.IndexOf(-1);
            var lastFile = data.Last(d => d != -1);
            var last = data.LastIndexOf(lastFile);

            if (first > last)
            {
                break;
            }

            data[first] = data[last];
            data[last] = -1;
        }

        var checkSum = 0L;
        for (var i = 0; i < data.Count; i++)
        {
            if (data[i] == -1)
            {
                break;
            }

            checkSum += i * data[i];
        }

        logger.Information("Checksum: {CheckSum}", checkSum);

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Defragmenting...");

        List<Block> blocks = [];

        var file = 0;
        var pos = 0;
        for (int i = 0; i < input.Trim().Length; i++)
        {
            var length = input[i] - '0';

            var fileIndex = -1;
            if (i % 2 == 0)
            {
                fileIndex = file++;
            }

            blocks.Add(new Block(fileIndex, pos, length));
            pos += length;
        }

        for (var fileIndex = blocks.Max(b => b.FileIndex); fileIndex >= 0; fileIndex--)
        {
            var fileBlock = blocks.Single(b => b.FileIndex == fileIndex);
            var freeBlocks = blocks.Where(b => b.FileIndex == -1 && b.Length >= fileBlock.Length);
            var freeBlock = freeBlocks.MinBy(b => b.Start);

            if (freeBlock == null)
            {
                continue;
            }

            if (fileBlock.Start < freeBlock.Start)
            {
                continue;
            }

            if (freeBlock.Length > fileBlock.Length)
            {
                blocks.Add(new Block(-1, freeBlock.Start + fileBlock.Length, freeBlock.Length - fileBlock.Length));
            }

            freeBlock.FileIndex = fileIndex;
            freeBlock.Length = fileBlock.Length;
            fileBlock.FileIndex = -1;
        }

        var checkSum = 0L;
        foreach (var block in blocks.Where(b => b.FileIndex != -1))
        {
            for (var i = block.Start; i < block.Start + block.Length; i++)
            {
                checkSum += i * block.FileIndex;
            }
        }

        logger.Information("Checksum: {CheckSum}", checkSum);

        return Task.CompletedTask;
    }

    private class Block(int fileIndex, int start, int length)
    {
        public int FileIndex { get => fileIndex; set => fileIndex = value; }
        public int Start { get => start; set => start = value; }
        public int Length { get => length; set => length = value; }
    }
}