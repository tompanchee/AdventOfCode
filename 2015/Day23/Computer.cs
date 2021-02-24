using System;

namespace Day23
{
    class Computer
    {
        private readonly string[] program;
        private int pc;

        public Computer(string[] program) {
            this.program = program;
        }

        public void Execute() {
            while (pc < program.Length) {
                var instruction = program[pc];
                var opCode = instruction[..3].Trim();
                var args = instruction[3..].Split(',', StringSplitOptions.TrimEntries);

                switch (opCode) {
                    case "hlf":
                        Half(args);
                        break;
                    case "tpl":
                        Triple(args);
                        break;
                    case "inc":
                        Increment(args);
                        break;
                    case "jmp":
                        Jump(args);
                        break;
                    case "jie":
                        JumpIfEven(args);
                        break;
                    case "jio":
                        JumpIfOne(args);
                        break;
                    default:
                        throw new InvalidOperationException(instruction);
                }
            }
        }

        void Half(string[] args) {
            SetRegister(args[0], GetRegister(args[0]) / 2);
            pc++;
        }

        void Triple(string[] args) {
            SetRegister(args[0], GetRegister(args[0]) * 3);
            pc++;
        }

        void Increment(string[] args) {
            SetRegister(args[0], GetRegister(args[0]) + 1);
            pc++;
        }

        void Jump(string[] args) {
            Jump(args[0]);
        }

        void JumpIfEven(string[] args) {
            if (GetRegister(args[0]) % 2 == 0) Jump(args[1]);
            else pc++;
        }

        void JumpIfOne(string[] args) {
            if (GetRegister(args[0]) == 1) Jump(args[1]);
            else pc++;
        }

        uint GetRegister(string register) {
            return register.ToLowerInvariant() == "a" ? A : B;
        }

        void SetRegister(string register, uint value) {
            if (register.ToLowerInvariant() == "a") A = value;
            else B = value;
        }

        void Jump(string offset) {
            pc += int.Parse(offset);
        }

        public uint A { get; set; }

        public uint B { get; set; }
    }
}