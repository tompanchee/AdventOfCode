using System.Collections.Generic;
using System.Linq;

namespace Day7.Gates
{
    abstract class Gate
    {
        protected Gate(IList<Wire> input, Wire output) {
            Input = input;
            Output = output;
        }

        public IList<Wire> Input { get; }
        public Wire Output { get; }

        public bool Active => Input.All(w => w.Signal.HasValue);

        protected abstract short InputCount { get; }

        public void Activate() {
            if (!Active) return;
            Output.Signal = DoActivate();
        }

        protected abstract ushort? DoActivate();
    }
}