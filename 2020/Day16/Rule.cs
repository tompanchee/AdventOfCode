namespace Day16
{
    class Rule
    {
        private readonly ValueRange range1;
        private readonly ValueRange range2;

        public Rule(string name, ValueRange range1, ValueRange range2) {
            Name = name;
            this.range1 = range1;
            this.range2 = range2;
        }

        public bool IsValid(int value) => range1.IsInRange(value) || range2.IsInRange(value);
        public string Name { get; }
    }
}