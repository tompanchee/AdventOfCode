using System.Linq;

namespace Day11
{
    class WaitingArea
    {
        private readonly string[] seats;

        public WaitingArea(string[] seats) {
            this.seats = seats;
        }

        public WaitingArea GetNextGeneration(bool part2 = false) {
            var newSeats = new string[seats.Length];
            for (var row = 0; row < seats.Length; row++) {
                var newRow = "";
                for (var col = 0; col < seats[row].Length; col++) {
                    newRow += part2 ? GetNewStatus2(row, col) : GetNewStatus(row, col);
                }

                newSeats[row] = newRow;
            }

            return new WaitingArea(newSeats);
        }

        private char GetNewStatus(in int row, in int col) {
            if (seats[row][col] == 'L' && CountNeighbours(row, col, '#') == 0) return '#';
            if (seats[row][col] == '#' && CountNeighbours(row, col, '#') >= 4) return 'L';
            return seats[row][col];
        }

        private char GetNewStatus2(in int row, in int col) {
            if (seats[row][col] == 'L' && SeatsSeen(row, col, '#') == 0) return '#';
            if (seats[row][col] == '#' && SeatsSeen(row, col, '#') >= 5) return 'L';
            return seats[row][col];
        }

        private int SeatsSeen(in int row, in int col, char checkChar) {
            var count = 0;
            for (var dr = -1; dr <= 1; dr++) {
                for (var dc = -1; dc <= 1; dc++) {
                    if (dr == 0 && dc == 0) continue;
                    var r = row + dr;
                    var c = col + dc;
                    while (r >= 0 && r < seats.Length && c >= 0 && c < seats[r].Length) {
                        if (seats[r][c] == 'L' || seats[r][c] == '#') {
                            if (seats[r][c] == checkChar) count++;
                            break;
                        }

                        r += dr;
                        c += dc;
                    }
                }
            }

            return count;
        }

        private int CountNeighbours(in int row, in int col, char checkChar) {
            var count = 0;
            for (var dr = -1; dr <= 1; dr++) {
                for (var dc = -1; dc <= 1; dc++) {
                    if (dr == 0 && dc == 0) continue;
                    var r = row + dr;
                    var c = col + dc;
                    if (r < 0 || r >= seats.Length) continue;
                    if (c < 0 || c >= seats[r].Length) continue;
                    if (seats[r][c] == checkChar) count++;
                }
            }

            return count;
        }

        public int CountWithStatus(char status) {
            return seats.Aggregate(0, (a, b) => a += b.Count(c => c == status));
        }

        public bool Equals(WaitingArea area) {
            return !seats.Where((t, i) => t != area.seats[i]).Any();
        }
    }
}