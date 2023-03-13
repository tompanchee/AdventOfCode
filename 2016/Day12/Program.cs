Console.WriteLine("Reading program...");
var program = File.ReadAllLines("input.txt").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

Console.WriteLine();

Console.WriteLine("Solving part 1...");
var computer = new Assembunny(program);
computer.Execute();
Console.WriteLine($"Register A after execution is {computer.A}");

Console.WriteLine();

Console.WriteLine("Solving part 1...");
computer = new Assembunny(program);
computer.SetC(1);
computer.Execute();
Console.WriteLine($"Register A after execution is {computer.A}");

internal class Assembunny
{
    readonly string[] program;

    readonly IDictionary<char, int> registers = new Dictionary<char, int> {
        {'a', 0},
        {'b', 0},
        {'c', 0},
        {'d', 0}
    };

    public Assembunny(string[] program) {
        this.program = program;
    }

    public int A => registers['a'];

    public void Execute() {
        var pc = 0;

        while (pc < program.Length) {
            var operation = program[pc];

            switch (operation[..3]) {
                case "cpy":
                    var args = operation[3..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    registers[args[1][0]] = GetArgumentValue(args[0]);
                    pc++;
                    break;
                case "inc":
                    var arg = operation[3..].Trim();
                    registers[arg[0]]++;
                    pc++;
                    break;
                case "dec":
                    arg = operation[3..].Trim();
                    registers[arg[0]]--;
                    pc++;
                    break;
                case "jnz":
                    args = operation[3..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (GetArgumentValue(args[0]) != 0) {
                        pc += int.Parse(args[1]);
                    } else {
                        pc++;
                    }

                    break;
                default:
                    throw new InvalidOperationException(operation[..3]);
            }
        }
    }

    int GetArgumentValue(string arg) {
        return registers.ContainsKey(arg[0]) ? registers[arg[0]] : int.Parse(arg);
    }

    public void SetC(int value) {
        registers['c'] = value;
    }
}