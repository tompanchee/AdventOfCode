namespace Day16
{
    class ValueRange
    {
        private readonly int min;
        private readonly int max;

        public static ValueRange FromString(string range) {
            var split = range.Split('-');
            return new ValueRange(int.Parse(split[0]), int.Parse(split[1]));
        }

        public ValueRange(int min, int max) {
            this.min = min;
            this.max = max;
        }

        public bool IsInRange(int value) => value >= min && value <= max;
    }
}