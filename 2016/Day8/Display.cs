using System;
using System.Collections.Generic;

namespace Day8
{
    class Display
    {
        public bool[,] Pixels { get; } = new bool[50, 6];

        public void Execute(string command, bool debug = false) {
            if (command.StartsWith("rect")) {
                var w = int.Parse(command[command.IndexOf(' ')..command.IndexOf('x')]);
                var h = int.Parse(command[(command.IndexOf('x') + 1)..]);
                Rect(w, h);
            } else if (command.StartsWith("rotate row")) {
                var y = int.Parse(command[(command.IndexOf('=') + 1)..command.IndexOf('b')]);
                var amount = int.Parse(command[(command.LastIndexOf('y') + 1)..]);
                RotateRow(y, amount);
            } else if (command.StartsWith("rotate column")) {
                var x = int.Parse(command[(command.IndexOf('=') + 1)..command.IndexOf('b')]);
                var amount = int.Parse(command[(command.IndexOf('y') + 1)..]);
                RotateCol(x, amount);
            } else {
                throw new InvalidOperationException($"Invalid operation {command}");
            }

            if (debug) {
                Console.WriteLine(command);
                Write();
            }
        }

        public void Write() {
            for (var y = 0; y < Pixels.GetLength(1); y++) {
                for (var x = 0; x < Pixels.GetLength(0); x++) {
                    Console.Write(Pixels[x, y] ? "#" : ".");
                }

                Console.WriteLine();
            }
        }

        void Rect(int w, int h) {
            for (var x = 0; x < w; x++) {
                for (var y = 0; y < h; y++) {
                    Pixels[x, y] = true;
                }
            }
        }

        void RotateRow(int y, int amount) {
            var row = new List<bool>();
            for (var x = 0; x < Pixels.GetLength(0); x++) {
                row.Add(Pixels[x, y]);
            }

            var newRow = Rotate(row, amount);
            for (var x = 0; x < Pixels.GetLength(0); x++) {
                Pixels[x, y] = newRow[x];
            }
        }

        void RotateCol(int x, int amount) {
            var col = new List<bool>();
            for (var y = 0; y < Pixels.GetLength(1); y++) {
                col.Add(Pixels[x, y]);
            }

            var newCol = Rotate(col, amount);
            for (var y = 0; y < Pixels.GetLength(1); y++) {
                Pixels[x, y] = newCol[y];
            }
        }

        private static IList<bool> Rotate(List<bool> array, int amount) {
            var result = new List<bool>();
            for (var i = 0; i < array.Count; i++) {
                var idx = i - amount;
                if (idx < 0) idx += array.Count;
                result.Add(array[idx]);
            }

            return result;
        }
    }
}