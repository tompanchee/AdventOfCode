using System.Collections.Generic;

namespace Day7.Gates
{
    class RShift : Gate
    {
        private readonly short shiftCount;

        public RShift(Wire input, Wire output, short shiftCount) : base(new List<Wire> {input}, output) {
            this.shiftCount = shiftCount;
        }

        protected override short InputCount => 1;

        protected override ushort? DoActivate() {
            return (ushort) (Input[0].Signal.Value >> shiftCount);
        }
    }
}