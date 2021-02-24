using System.Collections.Generic;

namespace Day7.Gates
{
    class Nop : Gate
    {
        public Nop(Wire input, Wire output) : base(new List<Wire> {input}, output) { }

        protected override short InputCount => 1;

        protected override ushort? DoActivate() {
            return Input[0].Signal;
        }
    }
}