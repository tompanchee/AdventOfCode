using System.Collections.Generic;

namespace Day7.Gates
{
    class Not : Gate
    {
        public Not(Wire input, Wire output) : base(new List<Wire> {input}, output) { }

        protected override short InputCount => 1;

        protected override ushort? DoActivate() {
            return (ushort) ~Input[0].Signal.Value;
        }
    }
}