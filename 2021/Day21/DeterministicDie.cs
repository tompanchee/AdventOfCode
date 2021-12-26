namespace Day21
{
    class DeterministicDie
    {
        int current = 1;
        int rollCount = 0;

        public int NumberOfRolls => rollCount;

        public int Roll(int timesToRoll) {
            var sum = 0;
            for (var i = 0; i < timesToRoll; i++) {
                sum += Roll();
            }
            return sum;
        }

        private int Roll() {
            var result = current;
            current++;
            if (current > 100) current = 1;
            rollCount++;
            return result;
        }
    }
}
