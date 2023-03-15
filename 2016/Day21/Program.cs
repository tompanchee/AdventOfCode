using Day21;
using static System.Console;

var input = File.ReadAllLines("input.txt")
    .Where(r => !string.IsNullOrWhiteSpace(r))
    .ToArray();

var scrambler = new Scrambler(input);

const string DATA = "abcdefgh";

WriteLine("Solving part 1...");
var scrambled = scrambler.Scramble(DATA);
WriteLine($"Scrambled string is {scrambled}");