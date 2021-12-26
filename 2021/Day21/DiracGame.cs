namespace Day21
{
    internal class DiracGame
    {
        readonly int winningPoints;
        long p1Count = 0;
        long p2Count = 0;

        public DiracGame(int winningPoints){
            this.winningPoints = winningPoints;
        }

        // Distribution of rolling three dice
        static readonly IDictionary<int, int> distribution = new Dictionary<int, int> {
            {3, 1},
            {4, 3},
            {5, 6},
            {6, 7},
            {7, 6},
            {8, 3},
            {9, 1}
        };

        public void PlayAll(int p1Pos, int p2Pos, long occurrences = 1, Player player = Player.P1, int p1Score = 0, int p2Score = 0) {
            foreach((var roll, var amount) in distribution) {                
                if (player == Player.P1) {
                    var position = Move(p1Pos, roll);
                    var score = p1Score + position;
                    var occ = occurrences * amount;
                    if (score >= winningPoints) {
                        p1Count += occ;
                    } else {
                        PlayAll(position, p2Pos, occ, Player.P2, score, p2Score);
                    }

                } else {
                    var position = Move(p2Pos, roll);
                    var score = p2Score + position;
                    var occ = occurrences * amount;
                    if (score >= winningPoints) {
                        p2Count += occ;
                    }
                    else {
                        PlayAll(p1Pos, position, occ, Player.P1, p1Score, score);
                    }
                }
            }
        }

        private static int Move(int pos, int roll){
            var p = (pos + roll) % 10;
            if (p == 0) p = 10;
            return p;
        }

        public string MostWins => p1Count > p2Count ? "P1" : "P2";
        public long MostWinsCount => p1Count > p2Count ? p1Count : p2Count;        
    }
}

