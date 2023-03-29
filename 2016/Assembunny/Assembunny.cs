namespace Assembunny;

public class Assembunny
{
    readonly string[] program;

    readonly IDictionary<char, int> registers = new Dictionary<char, int> {
        { 'a', 0 },
        { 'b', 0 },
        { 'c', 0 },
        { 'd', 0 }
    };

    public Assembunny(string[] program) {
        this.program = program;
    }

    public int A => registers['a'];

    public void Execute(int? maxOutputLength = null) {
        var pc = 0;
        Output = new List<int>();

        while (pc < program.Length) {
            var operation = program[pc];

            switch (operation[..3]) {
                case "cpy":
                    var args = operation[3..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (registers.ContainsKey(args[1][0])) registers[args[1][0]] = GetArgumentValue(args[0]);
                    pc++;
                    break;
                case "inc":
                    var arg = operation[3..].Trim();
                    if (registers.ContainsKey(arg[0])) registers[arg[0]]++;
                    pc++;
                    break;
                case "dec":
                    arg = operation[3..].Trim();
                    if (registers.ContainsKey(arg[0])) registers[arg[0]]--;
                    pc++;
                    break;
                case "jnz":
                    args = operation[3..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (GetArgumentValue(args[0]) != 0)
                        pc += GetArgumentValue(args[1]);
                    else
                        pc++;

                    break;
                case "tgl":
                    arg = operation[3..].Trim();
                    var value = GetArgumentValue(arg);
                    Toggle(value);
                    pc++;
                    break;
                case "out":
                    arg = operation[3..].Trim();
                    Output.Add(GetArgumentValue(arg));
                    pc++;
                    break;

                default:
                    throw new InvalidOperationException(operation[..3]);
            }

            if (maxOutputLength.HasValue && Output.Count >= maxOutputLength.Value) break;
        }

        void Toggle(int offset) {
            var position = pc + offset;
            if (position < 0 || position > program.Length - 1) return;

            var op = program[position][..3];
            program[position] = op switch {
                "inc" => program[position].Replace("inc", "dec"),
                "dec" => program[position].Replace("dec", "inc"),
                "tgl" => program[position].Replace("tgl", "inc"),
                "jnz" => program[position].Replace("jnz", "cpy"),
                "cpy" => program[position].Replace("cpy", "jnz"),
                _ => program[position]
            };
        }
    }

    int GetArgumentValue(string arg) {
        return registers.ContainsKey(arg[0]) ? registers[arg[0]] : int.Parse(arg);
    }

    public void SetA(int value) {
        registers['a'] = value;
    }

    public void SetC(int value) {
        registers['c'] = value;
    }

    public List<int>? Output { get; private set; }
}