var program = File.ReadAllLines("input.txt")
    .Where(l => !string.IsNullOrWhiteSpace(l))
    .ToArray();

Console.WriteLine("Solving part 1...");

var bunny = new Assembunny.Assembunny(program);

var a = 0;
while (true) {
    bunny.SetA(a);
    bunny.Execute(20);

    var o = string.Join(',', bunny.Output!);
    Console.WriteLine($"Input {a} produces output {o}...");

    if (o == "0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1") break;
    a++;
}

Console.WriteLine($"Smallest input to produce clock signal is {a}");
