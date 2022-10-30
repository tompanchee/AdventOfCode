using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args) {
            var memory = new List<Cell>();
            const int INPUT = 265149;

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine("Fill up memory");
            FillMemory(memory, c=>c.Index == INPUT);
            var cell = memory.Last();
            Console.WriteLine($"{INPUT} distance to port is {cell.Distance}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            Console.WriteLine("Fill up memory");
            memory = new List<Cell>();
            FillMemory(memory, c => c.Value > INPUT,true);
            cell = memory.Last();
            Console.WriteLine($"Memory value is greater than {INPUT} is {cell.Value}");
        }

        static void FillMemory(ICollection<Cell> memory, Func<Cell, bool> abort, bool calculateValue = false) {
            var offsets = new List<(int dr, int dc)> {
                (0, 1), // Right
                (-1, 0), // Up
                (0, -1), // Left
                (1, 0) // Down
            };

            var neighbourOffsets = new List<(int dr, int dc)> {
                (0, 1),
                (1, 1),
                (-1, 0),
                (-1, 1),
                (0, -1),
                (-1, -1),
                (1, 0),
                (1, -1)
            };

            var lookup = new HashSet<(int row, int col)>();

            var direction = 0;
            (int row, int col) current = (0, 0);

            var idx = 0;

            while(true) {
                idx++;
                long value = 0;
                if (calculateValue) {
                    value = idx == 1 ? 1 : CalculateValue();
                }

                var cell = new Cell {
                    Index = idx,
                    Row = current.row,
                    Col = current.col,
                    Value = value
                };
                memory.Add(cell);

                if (abort(cell)) return;

                lookup.Add(current);
                current.row += offsets[direction].dr;
                current.col += offsets[direction].dc;

                direction = GetNextDirection();
            }

            // R => rotate to U when top empty
            // U => rotate to L when left empty
            // L => rotate to D when down empty
            // D => rotate to R when right empty
            int GetNextDirection() {
                return direction switch {
                    0 when !lookup.Contains((current.row - 1, current.col)) => 1,
                    1 when !lookup.Contains((current.row, current.col - 1)) => 2,
                    2 when !lookup.Contains((current.row + 1, current.col)) => 3,
                    3 when !lookup.Contains((current.row, current.col + 1)) => 0,
                    _ => direction
                };
            }

            long CalculateValue() {
                long sum = 0;
                foreach (var (dr, dc) in neighbourOffsets) {
                    (int r, int c) neighbour = (current.row + dr, current.col + dc);
                    if (lookup.Contains(neighbour)) {
                        var nCell = memory.Single(c => c.Row == neighbour.r && c.Col == neighbour.c);
                        sum += nCell.Value;
                    }
                }

                return sum;
            }
        }
    }
    class Cell
    {
        public int Index { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public long Value { get; set; }
        public int Distance => Math.Abs(Row) + Math.Abs(Col);
    }
}
