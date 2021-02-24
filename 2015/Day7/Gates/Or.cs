using System.Collections.Generic;

namespace Day7.Gates
{
    class Or : Gate
    {
        public Or((Wire a, Wire b) input, Wire output) : base(new List<Wire> {input.a, input.b}, output) { }

        protected override short InputCount => 2;

        protected override ushort? DoActivate() {
            return (ushort) (Input[0].Signal.Value | Input[1].Signal.Value);
        }
    }
}