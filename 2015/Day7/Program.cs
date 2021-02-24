using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Day7.Gates;

namespace Day7
{
    class Program
    {
        static void Main(string[] args) {
            var input = File.ReadAllLines("input.txt");

            var (wires, gates) = ParseInput(input);

            Console.WriteLine("Solving puzzle 1");
            var signalA = GetSignalA(wires, gates);
            Console.WriteLine($"Signal at wire a is {signalA}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            (wires, gates) = ParseInput(input); // Reset input

            // Set wire B signal to signal from puzzle 1
            var g = gates.Single(g => g.Output.Id == "b");
            g.Input[0].Signal = signalA;

            signalA = GetSignalA(wires, gates);
            Console.WriteLine($"Signal at wire a is {signalA}");
        }

        private static ushort GetSignalA(IList<Wire> wires, IList<Gate> gates) {
            var remaining = new List<Gate>(gates);
            var wireA = wires.Single(w => w.Id == "a");

            while (!wireA.Signal.HasValue) {
                var active = remaining.Where(g => g.Active).ToList();
                foreach (var gate in active) {
                    gate.Activate();
                    remaining.Remove(gate);
                }
            }

            return wireA.Signal.Value;
        }

        private static (IList<Wire>, IList<Gate>) ParseInput(string[] input) {
            var currentNonNamedWire = 1;
            var gates = new List<Gate>();
            var wires = new List<Wire>();
            foreach (var line in input) {
                if (line.Contains("NOT")) ParseNot();
                else if (line.Contains("LSHIFT")) ParseLShift();
                else if (line.Contains("RSHIFT")) ParseRShift();
                else if (line.Contains("AND")) ParseAnd();
                else if (line.Contains("OR")) ParseOr();
                else ParseNop();

                void ParseNot() {
                    var split = line[3..].Split("->", StringSplitOptions.TrimEntries);
                    var i = AddWire(split[0]);
                    var o = AddWire(split[1]);
                    gates.Add(new Not(i, o));
                }

                void ParseLShift() {
                    var split1 = line.Split("LSHIFT", StringSplitOptions.TrimEntries);
                    var split2 = split1[1].Split("->", StringSplitOptions.TrimEntries);
                    var i = AddWire(split1[0]);
                    var o = AddWire(split2[1]);
                    var shift = short.Parse(split2[0]);
                    gates.Add(new LShift(i, o, shift));
                }

                void ParseRShift() {
                    var split1 = line.Split("RSHIFT", StringSplitOptions.TrimEntries);
                    var split2 = split1[1].Split("->", StringSplitOptions.TrimEntries);
                    var i = AddWire(split1[0]);
                    var o = AddWire(split2[1]);
                    var shift = short.Parse(split2[0]);
                    gates.Add(new RShift(i, o, shift));
                }

                void ParseAnd() {
                    var split1 = line.Split("AND", StringSplitOptions.TrimEntries);
                    var split2 = split1[1].Split("->", StringSplitOptions.TrimEntries);
                    var i1 = AddWire(split1[0]);
                    var i2 = AddWire(split2[0]);
                    var o = AddWire(split2[1]);
                    gates.Add(new And((i1, i2), o));
                }

                void ParseOr() {
                    var split1 = line.Split("OR", StringSplitOptions.TrimEntries);
                    var split2 = split1[1].Split("->", StringSplitOptions.TrimEntries);
                    var i1 = AddWire(split1[0]);
                    var i2 = AddWire(split2[0]);
                    var o = AddWire(split2[1]);
                    gates.Add(new Or((i1, i2), o));
                }

                void ParseNop() {
                    var split = line.Split("->", StringSplitOptions.TrimEntries);
                    var i = AddWire(split[0]);
                    var o = AddWire(split[1]);
                    gates.Add(new Nop(i, o));
                }
            }

            return (wires, gates);

            Wire AddWire(string id) {
                var wire = wires.SingleOrDefault(w => w.Id == id);
                if (wire == null) {
                    var name = id;
                    var signal = (ushort?) null;
                    if (id.All(char.IsDigit)) {
                        name = currentNonNamedWire++.ToString(CultureInfo.InvariantCulture);
                        signal = ushort.Parse(id);
                    }

                    wire = new Wire(name) {Signal = signal};
                    wires.Add(wire);
                }

                return wire;
            }
        }
    }
}