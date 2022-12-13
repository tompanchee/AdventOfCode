using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Day13;

internal class PacketComparer : IComparer<Packet>
{
    public int Compare(Packet? x, Packet? y) {
        return ComparePackets(x, y);
    }

    public static int ComparePackets(Packet left, Packet right) {
        var i = 0;
        while (true) {
            var lvalue = left[i];
            var rvalue = right[i];

            if (lvalue != null && rvalue == null) return 1;
            if (lvalue == null && rvalue != null) return -1;
            if (lvalue == null && rvalue == null) return 0;

            if (lvalue!.Type == JTokenType.Integer && rvalue!.Type == JTokenType.Integer) {
                var diff = lvalue.Value<int>() - rvalue.Value<int>();
                if (diff == 0) {
                    i++;
                    continue;
                }

                return diff;
            }

            if (lvalue!.Type == JTokenType.Integer) lvalue = JsonConvert.DeserializeObject<JToken>($"[{lvalue.Value<int>()}]");
            if (rvalue!.Type == JTokenType.Integer) rvalue = JsonConvert.DeserializeObject<JToken>($"[{rvalue.Value<int>()}]");

            var result = ComparePackets(new Packet(lvalue!), new Packet(rvalue!));
            if (result == 0) {
                i++;
                continue;
            }

            return result;
        }
    }
}