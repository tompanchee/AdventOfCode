using System.Collections.Generic;
using System.Linq;

namespace Day20
{
    class Tile
    {
        private readonly string[] rows;

        public Tile(int id, string[] rows) {
            Id = id;
            this.rows = rows;

            CalculateEdges();
        }

        public int Id { get; }

        public string[] Edges { get; private set; }

        private void CalculateEdges() {
            var edges = new List<string> {
                // Top
                rows[0],
                // Right
                new string(rows.Select(r => r.Last()).ToArray()),
                // Bottom
                rows.Last(),
                // Left
                new string(rows.Select(r => r[0]).Reverse().ToArray())
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

        public List<Tile> PossibleNeighbours { get; } = new List<Tile>();
    }
}