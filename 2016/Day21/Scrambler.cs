using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Day21Test")]

namespace Day21;

internal class Scrambler
{
    readonly string[] operations;

    public Scrambler(string[] operations) {
        this.operations = operations;
    }

    public string Scramble(string value) {
        var scrambled = value;
        foreach (var operation in operations) {
            if (operation.StartsWith("swap")) {
                scrambled = Swap(scrambled, operation);
                continue;
            }

            if (operation.StartsWith("rotate")) {
                scrambled = Rotate(scrambled, operation);
                continue;
            }

            if (operation.StartsWith("reverse ")) {
                scrambled = Reverse(scrambled, operation);
                continue;
            }

            if (operation.StartsWith("move ")) {
                scrambled = Move(scrambled, operation);
            }
        }

        return scrambled;
    }

    string Swap(string scrambled, string operation) {
        // swap position 2 with position 7
        // swap letter f with letter a

        var s = scrambled.ToCharArray();
        var p1 = operation.Contains("position")
            ? operation[14] - '0'
            : scrambled.IndexOf(operation[12]);
        var p2 = operation.Contains("position")
            ? operation[30] - '0'
            : scrambled.IndexOf(operation[26]);

        s[p1] = scrambled[p2];
        s[p2] = scrambled[p1];

        return new string(s);
    }

    string Rotate(string scrambled, string operation) {
        // rotate right 6 steps
        // rotate based on position of letter f
        // rotate left 1 step

        var rotate = 0;

        if (operation.Contains("right")) {
            rotate = operation[13] - '0';
        }

        if (operation.Contains("left")) {
            rotate = -(operation[12] - '0');
        }

        if (operation.Contains("based")) {
            rotate = scrambled.IndexOf(operation[35]) + 1;
            if (rotate >= 4) {
                rotate++;
            }
        }

        var l = scrambled.ToList();
        for (var i = 0; i < Math.Abs(rotate); i++)
            if (rotate < 0) {
                var c = l.First();
                l.RemoveAt(0);
                l.Add(c);
            } else {
                var c = l.Last();
                l.RemoveAt(l.Count - 1);
                l.Insert(0, c);
            }

        return new string(l.ToArray());
    }

    string Reverse(string scrambled, string operation) {
        // reverse positions 4 through 7

        var p1 = operation[18] - '0';
        var p2 = operation[28] - '0';

        var r = scrambled[p1..(p2 + 1)].ToCharArray().Reverse();
        return $"{scrambled[..p1]}{new string(r.ToArray())}{scrambled[(p2 + 1)..]}";
    }

    string Move(string scrambled, string operation) {
        // move position 0 to position 6

        var p1 = operation[14] - '0';
        var p2 = operation[28] - '0';

        var l = scrambled.ToList();

        var c = l[p1];
        l.RemoveAt(p1);
        l.Insert(p2, c);

        return new string(l.ToArray());
    }
}