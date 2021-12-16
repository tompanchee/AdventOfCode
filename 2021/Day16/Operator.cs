namespace Day16
{
    // An operator packet contains one or more packets. To indicate which subsequent binary data represents its sub-packets,
    // an operator packet can use one of two modes indicated by the bit immediately after the packet header;
    // this is called the length type ID:

    //If the length type ID is 0, then the next 15 bits are a number that represents the total length in bits of the sub-packets contained by this packet.

    //If the length type ID is 1, then the next 11 bits are a number that represents the number of sub-packets immediately contained by this packet.
    class Operator : Packet
    {        
        public static Operator Parse(string input, ref int pos) {
            var idx = 0;
            var lengthType = input[idx];
            idx++;

            var subPackets = new List<Packet>();
            if (lengthType == '0') {
                var subPacketLength = Convert.ToInt32(input[idx..(idx + 15)], 2);
                idx += 15;
                var subPacketData = input[idx..(idx + subPacketLength)];
                idx += subPacketLength;

                var subPos = 0;
                while (subPos < subPacketData.Length) {
                    subPackets.Add(Packet.Parse(subPacketData[subPos..], ref subPos, false));
                }
            }
            else {
                var subPacketCount = Convert.ToInt32(input[idx..(idx + 11)], 2);
                idx += 11;
                for (var i = 0; i < subPacketCount; i++) {
                    subPackets.Add(Packet.Parse(input[idx..], ref idx, false));
                }
            }

            pos += idx;
            return new Operator { SubPackets = subPackets };
        }

        public List<Packet> SubPackets { get; set; } = new List<Packet>();

        public override int VersionSum => base.VersionSum + SubPackets.Select(p => p.VersionSum).Sum();

        public override long Value => CalculateValue();

        private long CalculateValue() {
            return Type switch {
                0 => SubPackets.Sum(p => p.Value),
                1 => SubPackets.Aggregate(1L, (t, p) => t * p.Value),
                2 => SubPackets.Min(p => p.Value),
                3 => SubPackets.Max(p => p.Value),
                5 => SubPackets[0].Value > SubPackets[1].Value ? 1 : 0,
                6 => SubPackets[0].Value < SubPackets[1].Value ? 1 : 0,
                7 => SubPackets[0].Value == SubPackets[1].Value ? 1 : 0,
                _ => throw new InvalidOperationException($"Invalid operation: {Type}"),
            };
        }
    }
}
