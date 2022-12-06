using AOCCommon;
using Serilog.Core;

namespace Day06;

[Day(6, "Tuning Trouble")]
public class Solver : SolverBase
{
    string? signal;
    
    public Solver(string path, Logger logger) : base(path, logger) {}
    
    public override void Solve1()
    {
        int startOfPacket = ResolveCharactersToReadToFindDistinctMarker(4);

        logger.Information("Start of packet is found when {0} caharacters are read", startOfPacket);
    }

    public override void Solve2()
    {
        int startOfMessage = ResolveCharactersToReadToFindDistinctMarker(14);

        logger.Information("Start of message is found when {0} caharacters are read", startOfMessage);
    }

    int ResolveCharactersToReadToFindDistinctMarker(int markerLength)
    {
        for (var i = 0; i < signal!.Length - markerLength; i++)
        {
            var test = signal[i..(i + markerLength)];
            if (test.Distinct().Count() == markerLength)
            {
                return i + markerLength;
            }
        }

        return -1; // No marker found
    }

    protected override void PostConstruct()
    {
        logger.Information("Analyzing signal...");
        signal = data[0];
    }
}
