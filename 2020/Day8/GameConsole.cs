using System;
using System.Collections.Generic;

namespace Day8
{
    class GameConsole
    {
        private readonly string[] program;
        private int pc;

        public GameConsole(string[] program) {
            this.program = program;
        }

        public int Execute(bool haltOnLoop = false) {
            pc = -1;
            var executedInstructions = new List<int>();

            while (true) {
                pc++;
                if (haltOnLoop && executedInstructions.Contains(pc)) {
                    Console.WriteLine("Loop found halted");
                    return -1;
                }

                executedInstructions.Add(pc);

                var (opCode, argument) = ParseInstruction(program[pc]);

                switch (opCode) {
                    case "acc":
                        Acc += int.Parse(argument);
                        break;
                    case "jmp":
                        pc += int.Parse(argument) - 1; // subtract one as pc is any way increased in the loop
                        break;
                    case "nop":
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown operation code {opCode}");
                }

                if (pc == program.Length - 1) {
                    Console.WriteLine("Executed last instruction");
                    return 0;
                }
            }
        }

        private static (string, string) ParseInstruction(string instruction) {
            var split = instruction.Split(' ');
            return (split[0], split[1]);
        }

        public int Acc { get; private set; }
    }
}