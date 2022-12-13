using System.Runtime.CompilerServices;
using AOCCommon;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog.Core;

[assembly: InternalsVisibleTo("Day13.Test")]

namespace Day13;

[Day(13, "Distress Signal")]
internal class Solver : SolverBase
{
    readonly List<(Packet left, Packet right)> packetPairs = new();

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        var sum = 0;
        for (var i = 0; i < packetPairs.Count; i++)
            if (PacketComparer.ComparePackets(packetPairs[i].left, packetPairs[i].right) < 0)
                sum += i + 1;

        logger.Information("Sum of correct pair indices is {sum}", sum);
    }

    public override void Solve2() {
        // Create packet list
        var packets = (from row in data
                where !string.IsNullOrWhiteSpace(row)
                select new Packet(JsonConvert.DeserializeObject<JToken>(row)!))
            .ToList();

        // Adding divider packets
        var dividerPacket1 = new Packet(JsonConvert.DeserializeObject<JToken>("[[2]]")!);
        var dividerPacket2 = new Packet(JsonConvert.DeserializeObject<JToken>("[[6]]")!);
        packets.Add(dividerPacket1);
        packets.Add(dividerPacket2);

        var ordered = packets.ToArray();
        Array.Sort(ordered, new PacketComparer());

        var decoderKey = (Array.IndexOf(ordered, dividerPacket1) + 1) * (Array.IndexOf(ordered, dividerPacket2) + 1);

        logger.Information("Decoder key is {0}", decoderKey);
    }

    protected override void PostConstruct() {
        logger.Information("Reading packets...");

        Packet? left = null;
        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;
            if (left == null) {
                left = new Packet(JsonConvert.DeserializeObject<JToken>(row)!);
                continue;
            }

            var right = new Packet(JsonConvert.DeserializeObject<JToken>(row)!);
            packetPairs.Add((left, right));
            left = null;
        }
    }
}