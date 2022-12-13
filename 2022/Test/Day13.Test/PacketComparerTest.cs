using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Day13.Test;

public class PacketComparerTest
{
    [Theory]
    [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]", -1)]
    [InlineData("[[1],[2,3,4]]", "[[1],4]", -1)]
    [InlineData("[9]", "[[8,7,6]]", 1)]
    [InlineData("[[4,4],4,4]", "[[4,4],4,4,4]", -1)]
    [InlineData("[7,7,7,7]", "[7,7,7]", 1)]
    [InlineData("[[[]]]", "[[]]", 1)]
    [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", 1)]
    public void ShouldComparePackets(string lPacket, string rPacket, int sign) {
        var left = new Packet(JsonConvert.DeserializeObject<JToken>(lPacket)!);
        var right = new Packet(JsonConvert.DeserializeObject<JToken>(rPacket)!);

        Assert.Equal(Math.Sign(sign), Math.Sign(PacketComparer.ComparePackets(left, right)));
    }
}