using System.Security.Cryptography;
using System.Text;
using Common;
using Common.Solver;
using Serilog;
using Serilog.Events;

namespace Day14;

[Day(2023, 14, "Parabolic Reflector Dish")]
internal class Solver : SolverBase
{
    private char[,] dish = null!;

    public Solver(string input, ILogger logger) : base(input, logger)
    {
        ReadInput();
    }

    public override Task Solve1()
    {
        logger.Information("Tilting once north...");
        TiltNorth();
        logger.Information("Total load after tilt is {load}", CalculateLoad());

        return Task.CompletedTask;
    }

    public override Task Solve2()
    {
        logger.Information("Doing tilt cycles...");

        var hashes = new Dictionary<string, int> { { GetHash(), 0 } };
        var loads = new List<int>();
        var cycles = 0;

        string? hash;
        while (true)
        {
            Cycle();
            cycles++;
            hash = GetHash();
            if (hashes.ContainsKey(hash))
                break;
            hashes.Add(hash, cycles);
            loads.Add(CalculateLoad());
        }

        var offset = hashes[hash];
        var length = cycles - offset;

        var sequence = loads.Skip(offset - 1).Take(length).ToArray();

        var count = (1000000000 - offset) % length;

        logger.Information("Total load after 1000000000 tilt cycles is {load}", sequence[count]);

        return Task.CompletedTask;
    }

    private void ReadInput()
    {
        logger.Information("Scanning dish...");
        dish = new char[inputLines[0].Length, inputLines.Count(l => !string.IsNullOrWhiteSpace(l))];

        for (var y = 0; y < dish.GetLength(1); y++)
        {
            for (var x = 0; x < dish.GetLength(0); x++)
            {
                dish[x, y] = inputLines[y][x];
            }
        }

        LogDish();
    }

    private void Cycle()
    {
        logger.Debug("Doing tilt cycle...");
        TiltNorth();
        TiltWest();
        TiltSouth();
        TiltEast();
    }

    #region Tilting

    private void TiltNorth()
    {
        logger.Debug("Tilting north...");

        for (var x = 0; x < dish.GetLength(0); x++)
        {
            var limit = 0;
            for (var y = 0; y < dish.GetLength(1); y++)
            {
                if (dish[x, y] == 'O')
                {
                    if (y != limit)
                    {
                        dish[x, limit] = 'O';
                        dish[x, y] = '.';
                    }

                    limit++;
                }

                if (dish[x, y] == '#')
                {
                    limit = y + 1;
                }
            }
        }

        LogDish();
    }

    private void TiltSouth()
    {
        logger.Debug("Tilting south...");

        for (var x = dish.GetLength(0) - 1; x >= 0; x--)
        {
            var limit = dish.GetLength(1) - 1;
            for (var y = dish.GetLength(1) - 1; y >= 0; y--)
            {
                if (dish[x, y] == 'O')
                {
                    if (y != limit)
                    {
                        dish[x, limit] = 'O';
                        dish[x, y] = '.';
                    }

                    limit--;
                }

                if (dish[x, y] == '#')
                {
                    limit = y - 1;
                }
            }
        }

        LogDish();
    }

    private void TiltWest()
    {
        logger.Debug("Tilting west...");

        for (var y = 0; y < dish.GetLength(1); y++)
        {
            var limit = 0;
            for (var x = 0; x < dish.GetLength(0); x++)
            {
                if (dish[x, y] == 'O')
                {
                    if (x != limit)
                    {
                        dish[limit, y] = 'O';
                        dish[x, y] = '.';
                    }

                    limit++;
                }

                if (dish[x, y] == '#')
                {
                    limit = x + 1;
                }
            }
        }

        LogDish();
    }

    private void TiltEast()
    {
        logger.Debug("Tilting east...");

        for (var y = dish.GetLength(1) - 1; y >= 0; y--)
        {
            var limit = dish.GetLength(0) - 1;
            for (var x = dish.GetLength(1) - 1; x >= 0; x--)
            {
                if (dish[x, y] == 'O')
                {
                    if (x != limit)
                    {
                        dish[limit, y] = 'O';
                        dish[x, y] = '.';
                    }

                    limit--;
                }

                if (dish[x, y] == '#')
                {
                    limit = x - 1;
                }
            }
        }

        LogDish();
    }

    #endregion

    private int CalculateLoad()
    {
        var load = 0;
        var height = dish.GetLength(1);

        for (var y = 0; y < dish.GetLength(1); y++)
        {
            for (var x = 0; x < dish.GetLength(0); x++)
            {
                if (dish[x, y] == 'O') load += height - y;
            }
        }

        return load;
    }

    private string GetHash()
    {
        var concat = new StringBuilder();
        for (var y = 0; y < dish.GetLength(1); y++)
        {
            for (var x = 0; x < dish.GetLength(0); x++)
            {
                concat.Append(dish[x, y]);
            }
        }

        return Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(concat.ToString())));
    }

    private void LogDish()
    {
        if (!logger.IsEnabled(LogEventLevel.Debug)) return;

        var sb = new StringBuilder().AppendLine();
        for (var y = 0; y < dish.GetLength(1); y++)
        {
            for (var x = 0; x < dish.GetLength(0); x++)
            {
                sb.Append(dish[x, y]);
            }

            sb.AppendLine();
        }

        logger.Debug(sb.ToString());
    }
}