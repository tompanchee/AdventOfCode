namespace Day16
{
    // Every packet begins with a standard header: the first three bits encode the packet version, and the next three bits encode the packet type ID.
    // These two values are numbers; all numbers encoded in any packet are represented as binary with the most significant bit first.
    // For example, a version encoded as the binary sequence 100 represents the number 4.
    abstract class Packet {
        public static Packet Parse(string input, ref int pos, bool trim = true) {
            var idx = 0;
            var version = Convert.ToInt32(input[idx..(idx + 3)], 2);
            idx += 3;
            var type = Convert.ToInt32(input[idx..(idx + 3)], 2);
            idx += 3;

            Packet packet;
            if (type == 4) {
                packet = Literal.Parse(input[idx..], ref idx);
            }
            else {
                packet = Operator.Parse(input[idx..], ref idx);
            }

            packet.Version = version;
            packet.Type = type;

            pos += idx;
            if (trim) pos += (idx % 4);
            return packet;
        }

        public int Version { get; set; }
        public int Type { get; set; }

        public abstract long Value { get; }

        public virtual int VersionSum => Version;
    }
}
