using Day21;
using MoreLinq;
using static System.Console;

var input = File.ReadAllLines("input.txt")
    .Where(r => !string.IsNullOrWhiteSpace(r))
    .ToArray();

var scrambler = new Scrambler(input);

const string DATA = "abcdefgh";

WriteLine("Solving part 1...");
var scrambled = scrambler.Scramble(DATA);
WriteLine($"Scrambled string is {scrambled}");

WriteLine();

WriteLine("Solving part 2...");

const string TARGET = "fbgdceah";
//Brute force through all permutations
var password = string.Empty;
foreach (var permutation in DATA.Permutations())
    if (scrambler.Scramble(new string(permutation.ToArray())) == TARGET) {
        password = new string(permutation.ToArray());
        break;
    }

WriteLine($"The password is {password}");