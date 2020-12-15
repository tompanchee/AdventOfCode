using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day14.Puzzle1
{
    class Decoder
    {
        private readonly string[] program;
        private Mask mask;

        public Decoder(string[] program) {
            this.program = program;
        }

        public void Execute() {
            foreach (var instruction in program) {
                if (instruction.StartsWith("mask")) {
                    SetMask(instruction);
                }

                if (instruction.StartsWith("mem")) {
                    SetMemory(instruction);
                }
            }
        }

        private void SetMask(string instruction) {
            var regex = new Regex("mask = (.*)");
            var matches = regex.Matches(instruction);
            mask = new Mask(matches[0].Groups[1].Value);
        }

        private void SetMemory(string instruction) {
            var regex = new Regex("mem\\[(.*)\\] = (.*)");
            var matches = regex.Matches(instruction);
            var offset = int.Parse(matches[0].Groups[1].Value);
            var value = long.Parse(matches[0].Groups[2].Value);
            if (mask == null) Memory[offset] = value;
            else Memory[offset] = mask.GetMaskedValue(value);
        }

        public IDictionary<int, long> Memory { get; } = new Dictionary<int, long>();
    }
}