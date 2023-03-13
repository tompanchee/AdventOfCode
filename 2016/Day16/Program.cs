using System.Text;

var input = File.ReadAllText("input.txt");

Console.WriteLine("Solving part 1...");
//var data = DragonCurve("10000", 20);
var data = DragonCurve(input, 272);
var check = CalculateCheckSum(data);
Console.WriteLine($"Checksum is  {check}");

Console.WriteLine();

Console.WriteLine("Solving part 2...");
data = DragonCurve(input, 35651584);
check = CalculateCheckSum(data);
Console.WriteLine($"Checksum is  {check}");

static string DragonCurve(string value, int size) {
    var result = value;

    while (result.Length < size) {
        var aReverse = new string(result.Reverse().ToArray());
        var b = new string(aReverse.Select(c => c == '1' ? '0' : '1').ToArray());
        result = $"{result}0{b}";
    }

    return result[..size];
}

static string CalculateCheckSum(string value) {
    while (true) {
        var checkSum = new StringBuilder();
        for (var i = 0; i < value.Length; i += 2) checkSum.Append(value[i] == value[i + 1] ? '1' : '0');

        if (checkSum.Length % 2 == 1) {
            return checkSum.ToString();
        }

        value = checkSum.ToString();
    }
}