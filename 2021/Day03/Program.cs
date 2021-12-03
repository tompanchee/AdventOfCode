var input = File.ReadAllLines("input.txt");
var bits = input[0].Length;
var max = Convert.ToInt32(new string('1', bits), 2);
var data = input.Select(l=>Convert.ToInt32(l, 2));

Console.WriteLine("Solving puzzle 1...");
var gamma = 0;
for(var b=0; b < bits; b++) {
    var (oneCount, zeroCount) = CountOneAndZeroBits(data, b);
    if (oneCount > zeroCount) gamma += (1 << b);  
}

var epsilon = ~gamma & max;
Console.WriteLine($"Power consumption: {gamma * epsilon}");

Console.WriteLine();

Console.WriteLine("Solving puzzle 2...");
var o2GeneratorRating = data;
var bit = bits - 1;
while(true) {      
    var (oneCount, zeroCount) = CountOneAndZeroBits(o2GeneratorRating, bit); 

    if (oneCount >= zeroCount) o2GeneratorRating = new List<int>(o2GeneratorRating.Where(d=>IsBitSet(d, bit)));
    else o2GeneratorRating = new List<int>(o2GeneratorRating.Where(d=>!IsBitSet(d, bit)));

    if (o2GeneratorRating.Count() == 1) break;
    
    bit--;  
}
Console.WriteLine($"O2 Generator rating: {o2GeneratorRating.Single()}");

var co2ScrubberRating = data;
bit = bits - 1;
while(true) {
    var (oneCount, zeroCount) = CountOneAndZeroBits(co2ScrubberRating, bit); 

    if (oneCount < zeroCount) co2ScrubberRating = new List<int>(co2ScrubberRating.Where(d=>IsBitSet(d, bit)));
    else co2ScrubberRating = new List<int>(co2ScrubberRating.Where(d=>!IsBitSet(d, bit)));

    if (co2ScrubberRating.Count() == 1) break;

    bit--;
}
Console.WriteLine($"CO2 Scrubber rating: {co2ScrubberRating.Single()}");

Console.WriteLine($"Life support rating: {o2GeneratorRating.Single() * co2ScrubberRating.Single()}");

bool IsBitSet(int value, int bit) => ((value >> bit) & 1) > 0;

(int ones, int zeros) CountOneAndZeroBits(IEnumerable<int> values, int bit) {
    var oneMask = 1 << bit;
    var oneCount = values.Count(d=>(d & oneMask) == oneMask);
    return (oneCount, values.Count() - oneCount);
}