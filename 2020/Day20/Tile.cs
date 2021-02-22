using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day20
{
    class Tile
    {
        private IList<Tile> neighbours = null;

        public Tile(int id, string[] rows) {
            Id = id;
            this.Rows = rows;

            CalculateEdges();
        }

        public int Id { get; }

        public string[] Rows { get; }

        public string[] Edges { get; private set; }

        private void CalculateEdges() {
            var edges = new List<string> {
                // Top
                Rows[0],
                // Right
                new string(Rows.Select(r => r.Last()).ToArray()),
                // Bottom
                Rows.Last(),
                // Left
                new string(Rows.Select(r => r[0]).ToArray())
            };

            // Flipped

            // Top
            edges.Add(Reverse(edges[0]));
            // Right
            edges.Add(Reverse(edges[1]));
            // Bottom
            edges.Add(Reverse(edges[2]));
            // Left
            edges.Add(Reverse(edges[3]));

            Edges = edges.ToArray();
        }

        static string Reverse(string s) {
            return new string(s.Reverse().ToArray());
        }

        public IList<Tile> GetNeighbours(Tile[] tiles) {
            return neighbours ??= CalculateNeighbours(tiles);
        }

        public Tile FindNeighbourWithEdge(string edge, Tile[] tiles) {
            return GetNeighbours(tiles).SingleOrDefault(n => n.Edges.Contains(edge));
        }

        private IList<Tile> CalculateNeighbours(Tile[] tiles) {
            var n = new List<Tile>();
            foreach (var edge in Edges) {
                var candidates = tiles.Where(t => t.Id != Id && t.Edges.Contains(edge));
                foreach (var candidate in candidates) {
                    if (!n.Contains(candidate)) n.Add(candidate);
                }
            }

            return n;
        }

        public int Size => Rows.Length; // Square tile

        public string Top => Edges[0];
        public string Right => Edges[1];
        public string Bottom => Edges[2];
        public string Left => Edges[3];

        public void Rotate() {
            for (var i = 0; i < Size / 2; i++) {
                for (var j = i; j < Size - i - 1; j++) {
                    // Swap elements of each cycle
                    // in clockwise direction
                    var temp = this[i, j];
                    this[i, j] = this[Size - 1 - j, i];
                    this[Size - 1 - j, i] = this[Size - 1 - i, Size - 1 - j];
                    this[Size - 1 - i, Size - 1 - j] = this[j, Size - 1 - i];
                    this[j, Size - 1 - i] = temp;
                }
            }

            CalculateEdges();
        }

        public void FlipHorizontally() {
            for (var i = 0; i < Size / 2; i++) {
                var temp = Rows[i];
                Rows[i] = Rows[Size - i - 1];
                Rows[Size - i - 1] = temp;
            }

            CalculateEdges();
        }

        public void FlipVertically() {
            for (var i = 0; i < Size; i++) {
                Rows[i] = Reverse(Rows[i]);
            }

            CalculateEdges();
        }

        public Tile RemoveEdges() {
            var withoutEdges = new List<string>();
            for (var i = 1; i < Size - 1; i++) {
                withoutEdges.Add(Rows[i][1..^1]);
            }

            return new Tile(Id, withoutEdges.ToArray());
        }

        public string[] GetSubArray((int row, int col) point, (int height, int width) size) {
            var result = new List<string>();
            for (var row = point.row; row < point.row + size.height; row++) {
                result.Add(Rows[row].Substring(point.col, size.width));
            }

            return result.ToArray();
        }

        private char this[int row, int col] { get => Rows[row][col]; set => Rows[row] = new StringBuilder(Rows[row]) {[col] = value}.ToString(); }
    }
}