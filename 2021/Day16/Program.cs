using Day16;

var input = File.ReadAllText("input.txt");
var packets = Parse(input);

Console.WriteLine("Solving puzzle 1...");
Console.WriteLine($"Version sum is {packets.Sum(p => p.VersionSum)}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
Console.WriteLine($"BITS transmission evaluated: {packets.Sum(p => p.Value)}");


List<Packet> Parse(string input) {
    var result = new List<Packet>();
    var binary = string.Join(string.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

    var pos = 0;
    while (pos < input.Length) {
        result.Add(Packet.Parse(binary[pos..], ref pos));
    }

    return result;
}