var input = File.ReadAllLines("input.txt");
var data = ParseInput(input);

Console.WriteLine("Solving puzzle 1...");
int[] uniqueLengths = {2,3,4,7};
var countOfUniques = 0;
foreach(var r in data.Select(d=>d.results)) {
    countOfUniques += r.Where(p=>uniqueLengths.Contains(p.Length)).Count();
}
Console.WriteLine($"Number of unique digits in results is {countOfUniques}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
/* Segment numbering:       
 000
5   1
5   1
 666
4   2
4   2
 333

 Allowed combinations:
 0: 0 1 2 3 4 5
 1:   1 2
 2: 0 1   3 4   6
 3: 0 1 2 3     6
 4:   1 2     5 6
 5: 0   2 3   5 6
 6: 0   2 3 4 5 6
 7: 0 1 2
 8: 0 1 2 3 4 5 6
 9: 0 1 2 3   5 6
*/

Dictionary<int, int[]> numberSegments = new Dictionary<int, int[]>{
    {0, new[]{0,1,2,3,4,5}},
    {1, new[]{1,2}},
    {2, new[]{0,1,3,4,6}},
    {3, new[]{0,1,2,3,6}},
    {4, new[]{1,2,5,6}},
    {5, new[]{0,2,3,5,6}},
    {6, new[]{0,2,3,4,5,6}},
    {7, new[]{0,1,2}},
    {8, new[]{0,1,2,3,4,5,6}},
    {9, new[]{0,1,2,3,5,6}}
};

var sum = 0;
foreach(var row in input) {
    var segments = new string[7];

    var one = string.Empty; // length: 2
    var seven = string.Empty; // 3
    var four = string.Empty; // 4
    var eight = string.Empty; // 7
    var twoThreeFive = new List<string>(); // 5
    var zeroSixOrNine = new List<string>(); // 6
    var zero = string.Empty;
    var two = string.Empty;
    var three = string.Empty;
    var five = string.Empty;
    var six = string.Empty;
    var nine = string.Empty;
    
    foreach(var signal in row.signals.Select(s=>s.OrderBy(c=>c))) {
        switch(signal.Lenght) {
            case 2:
                one = signal;
                break;
            case 3:
                seven = signal;
                break;
            case 4:
                four = signal;
                break;
            case 5:
                twoThreeFive.Add(signal);
                break;
            case 6:
                zeroSixOrNine.Add(signal);
                break;
            case 7:
                eight = signal;
                break;
            default:
                throw new InvalidDataException("Unknown signal combination");
        }

        if (one != string.Empty && seven != string.Empty) {
            segments[0] = seven.Except(one).ToString();
        }

        // if(seven != string.Empty && sixOrNine.Any()) {
        //     nine = sixOrNine.SingleOrDefault(sn => sn.Contains(seven));
        //     six = sixOrNine.SingleOrDefault(sn => !sn.Contains(seven));
        // }

        // if (one != string.Empty && twoThreeFive.Any()) {
        //     three = twoThreeFive.SingleOrDefault(ttf => ttf.Contains(one));
        // }

    }
}


Data[] ParseInput(string[] input) {
    var result = new List<Data>();

    foreach(var line in input) {
        var split = line.Split('|', StringSplitOptions.TrimEntries);
        result.Add(new Data(split[0].Split(' '), split[1].Split(' ')));
    }

    return result.ToArray();
}

readonly record struct Data(string[] signals, string[] results);