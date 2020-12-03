using System.IO;

namespace Day3
{
    class Forest
    {
        private readonly string[] rows;

        private Forest(string[] rows) {
            this.rows = rows;
        }

        public static Forest Init(string path) {
            return new Forest(File.ReadAllLines(path));
        }

        public char? this[int top, int left]
        {
            get
            {
                if (top > rows.Length) return null; // Out of bounds
                while (left > rows[top - 1].Length) left -= rows[top - 1].Length; // Wrap
                return rows[top - 1][left - 1]; // 1 to 0 based array
            }
        }
    }
}