namespace Day04
{
    internal class Board
    {
        const int TABLE_SIZE = 5;
        readonly int[,] table = new int[TABLE_SIZE, TABLE_SIZE];

        private Board(int[,] table, int id) {
            this.table = table;
            Id = id;
        }

        public static Board From(string[] data, int id) {
            var table = new int[TABLE_SIZE, TABLE_SIZE];
            for (var row = 0; row < data.Length; row++) {
                var rowData = data[row].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for(var col = 0; col < rowData.Length; col++) {
                    table[row, col] = rowData[col];
                }
            }

            return new Board(table, id);
        }

        public void DrawNumber(int number) {
            for(var row = 0; row < TABLE_SIZE; row++) {
                for (var col = 0; col < TABLE_SIZE; col++) {
                    if(table[row, col] == number) { 
                        table[row, col] = -1; // Mark found number as -1
                    }
                }
            }
        }

        public int GetWinningScore(int number) {
            var sum = table.Cast<int>().Where(n => n > 0).Sum();
            return number * sum;
        }

        public bool IsBingo() {
            for(var i=0; i<TABLE_SIZE; i++) {
                var row = Row(i);
                var col = Col(i);

                if (row.All(n => n < 0) || col.All(n => n < 0)) return true;
            }

            return false;
        }

        public int Id { get; }

        int[] Row(int row) {
            var result = new List<int>();
            for (var i = 0; i < TABLE_SIZE; i++) {
                result.Add(table[row, i]);
            }

            return result.ToArray();
        }

        int[] Col(int col) {
            var result = new List<int>();
            for(var i=0; i < TABLE_SIZE; i++) {
                result.Add(table[i, col]);
            }

            return result.ToArray();
        }
    }
}
