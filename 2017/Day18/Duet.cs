using System.Collections.Concurrent;

namespace Day18;

internal class Duet
{
    public enum Status
    {
        Initialized,
        Running,
        Waiting,
        Completed
    }

    readonly string[] program;

    readonly IDictionary<char, long> registers = new Dictionary<char, long>();
    readonly bool useSendAndReceive;
    long lastFrequencyPlayed;
    Duet? sender;

    public Duet(string[] program, long? id = null, bool useSendAndReceive = false) {
        this.program = program;
        this.useSendAndReceive = useSendAndReceive;

        if (id.HasValue) registers.Add('p', id.Value);
    }

    public ConcurrentQueue<long> SendQueue { get; } = new();

    public int SendCount { get; private set; }

    public Status ProgramStatus { get; private set; } = Status.Initialized;

    public void SetSender(Duet s) {
        sender = s;
    }

    public void Execute(Func<bool>? recoverCallback = null) {
        var pc = 0L;
        var exit = false;

        while (!exit && pc < program.Length) {
            ProgramStatus = Status.Running;
            var operation = program[pc];

            pc += operation[..3] switch {
                "snd" => PlaySoundOrSend(operation[3..]),
                "set" => Set(operation[3..]),
                "add" => Add(operation[3..]),
                "mul" => Multiply(operation[3..]),
                "mod" => Modulo(operation[3..]),
                "rcv" => RecoverOrReceive(operation[3..], recoverCallback, ref exit),
                "jgz" => JumpIfGreaterThanZero(operation[3..]),
                _ => throw new InvalidOperationException(operation)
            };
        }

        ProgramStatus = Status.Completed;
    }

    // snd X plays a sound with a frequency equal to the value of X.
    int PlaySoundOrSend(string args) {
        if (useSendAndReceive) return Send(args);
        lastFrequencyPlayed = GetValue(args);
        return 1;
    }

    // snd X sends the value of X to the other program.
    int Send(string args) {
        SendQueue.Enqueue(GetValue(args));
        SendCount++;
        return 1;
    }

    // set X Y sets register X to the value of Y.
    int Set(string args) {
        var split = args.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var x = split[0][0];
        var y = GetValue(split[1]);

        AddRegisterIfNotExists(x);
        registers[x] = y;
        return 1;
    }

    // add X Y increases register X by the value of Y.
    int Add(string args) {
        var split = args.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var x = split[0][0];
        var y = GetValue(split[1]);

        AddRegisterIfNotExists(x);
        registers[x] += y;
        return 1;
    }

    // mul X Y sets register X to the result of multiplying the value contained in register X by the value of Y.
    int Multiply(string args) {
        var split = args.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var x = split[0][0];
        var y = GetValue(split[1]);

        AddRegisterIfNotExists(x);
        registers[x] *= y;
        return 1;
    }

    // mod X Y sets register X to the remainder of dividing the value contained in register X by the value of Y.
    int Modulo(string args) {
        var split = args.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var x = split[0][0];
        var y = GetValue(split[1]);

        AddRegisterIfNotExists(x);
        registers[x] %= y;
        return 1;
    }

    // rcv X recovers the frequency of the last sound played, but only when the value of X is not zero.
    int RecoverOrReceive(string args, Func<bool>? recoverCallback, ref bool exit) {
        if (useSendAndReceive) return Receive(args, ref exit);
        var value = GetValue(args);
        if (value != 0) {
            Console.WriteLine($"Recovered frequency {lastFrequencyPlayed}");
            if (recoverCallback != null) exit = recoverCallback();
        }

        return 1;
    }

    // rcv X receives the next value and stores it in register X. If no values are in the queue, the program waits for a value to be sent to it.
    int Receive(string args, ref bool exit) {
        if (sender!.SendQueue.IsEmpty) {
            ProgramStatus = Status.Waiting;
            if (sender.ProgramStatus is Status.Waiting or Status.Completed) exit = true;

            return 0;
        }

        var reg = args.Trim()[0];
        AddRegisterIfNotExists(reg);
        while (true)
            if (sender!.SendQueue.TryDequeue(out var value)) {
                registers[reg] = value;
                break;
            }

        ProgramStatus = Status.Running;

        return 1;
    }

    // jgz X Y jumps with an offset of the value of Y, but only if the value of X is greater than zero.
    long JumpIfGreaterThanZero(string args) {
        var split = args.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var x = GetValue(split[0]);
        var y = GetValue(split[1]);

        return x > 0 ? y : 1;
    }

    long GetValue(string arg) {
        if (int.TryParse(arg, out var result)) return result;

        var reg = arg.Trim()[0];
        AddRegisterIfNotExists(reg);
        return registers[reg];
    }

    void AddRegisterIfNotExists(char reg) {
        if (!registers.ContainsKey(reg)) registers.Add(reg, 0);
    }
}