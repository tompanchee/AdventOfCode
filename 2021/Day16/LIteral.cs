namespace Day16
{
    // Packets with type ID 4 represent a literal value. Literal value packets encode a single binary number.
    // To do this, the binary number is padded with leading zeroes until its length is a multiple of four bits,
    // and then it is broken into groups of four bits. Each group is prefixed by a 1 bit except the last group,
    // which is prefixed by a 0 bit. These groups of five bits immediately follow the packet header.
    class Literal : Packet
    {
        long value;

        public static Literal Parse(string input, ref int pos) {
            var idx = 0;
            var value = string.Empty;
            bool @continue = true;
            while (@continue) {
                var group = input[idx..(idx + 5)];
                idx += 5;
                if (group[0] == '0') @continue = false;
                value += group[1..];
            }

            value += new string('0', value.Length % 4);

            pos += idx;
            return new Literal { value = Convert.ToInt64(value, 2) };
        }

        public override long Value => value;
    }
}
