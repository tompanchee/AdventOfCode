namespace Day21
{
    class PracticeGame
    {
        int p1Pos;
        int p2Pos;
        long p1Score = 0L;
        long p2Score = 0L;
        readonly long winningScore;
        readonly DeterministicDie die;

        public PracticeGame(int p1Pos, int p2Pos, long winningScore, DeterministicDie die) {
            this.p1Pos = p1Pos;
            this.p2Pos = p2Pos;
            this.winningScore = winningScore;
            this.die = die;
        }

        public Player Play() {
            while (true) {
                var roll = die.Roll(3);
                p1Pos = Move(p1Pos, roll);
                p1Score += p1Pos;
                if (p1Score >= winningScore) return Player.P1;

                roll = die.Roll(3);
                p2Pos = Move(p2Pos, roll);
                p2Score += p2Pos;
                if (p2Score >= winningScore) return Player.P2;
            }
        }

        static int Move(int position, int steps) {
            var p = (position + steps) % 10;
            if (p == 0) p = 10;
            return p;
        }

        public long P1Score => p1Score;
        public long P2Score => p2Score;
    }
}
