using System.Security.Cryptography;
using System.Text;

var salt = File.ReadAllText("input.txt");
//var salt = "abc";
List<string> hashCache = new();

Console.WriteLine("Solving part 1...");
Console.WriteLine($"64th pad key is at index {FindPadKey(GetMd5ForIndex)}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
Console.WriteLine($"64th pad key is at index {FindPadKey(GetStretchedHasForIndex)}");

string GetMd5ForIndex(int idx) {
    if (idx < hashCache.Count) {
        return hashCache[idx];
    }

    var output = CreateHash($"{salt}{idx}");
    hashCache.Add(output);

    return output;
}

string CreateHash(string value) {
    using var md5 = MD5.Create();
    var input = Encoding.ASCII.GetBytes(value);
    var hash = md5.ComputeHash(input);
    return Convert.ToHexString(hash).ToLower();
}

string GetStretchedHasForIndex(int idx) {
    if (idx < hashCache.Count) {
        return hashCache[idx];
    }

    var hash = CreateHash($"{salt}{idx}");
    for (var i = 0; i < 2016; i++) hash = CreateHash(hash);

    hashCache.Add(hash);

    return hash;
}

int FindPadKey(Func<int, string> hashFunction) {
    List<int> keyIndices = new();
    hashCache.Clear();

    var index = 0;
    while (keyIndices.Count < 64) {
        var md5 = hashFunction(index);
        var charToCheck = HasThreeSameCharsInRow(md5);
        if (charToCheck != null) {
            var check = new string(charToCheck.Value, 5);
            for (var offset = 1; offset <= 1000; offset++) {
                var md52 = hashFunction(index + offset);
                if (md52.Contains(check)) {
                    keyIndices.Add(index);
                    break;
                }
            }
        }

        index++;

        char? HasThreeSameCharsInRow(string s) {
            for (var i = 0; i < s.Length - 2; i++)
                if (s[i] == s[i + 1] && s[i] == s[i + 2]) {
                    return s[i];
                }

            return null;
        }
    }

    return keyIndices.Last();
}