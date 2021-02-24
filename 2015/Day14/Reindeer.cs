namespace Day14
{
    class Reindeer
    {
        private readonly int speed;
        private readonly int flyTime;
        private readonly int sequenceTime;

        public Reindeer(string name, int speed, int flyTime, int restTime) {
            Name = name;
            this.speed = speed;
            this.flyTime = flyTime;
            sequenceTime = flyTime + restTime;
        }

        public int PositionAt(int moment) {
            var totalSequences = moment / sequenceTime;
            var sequenceMoment = moment % sequenceTime;

            var position = totalSequences * speed * flyTime;
            if (sequenceMoment > flyTime) position += speed * flyTime;
            else position += sequenceMoment * speed;

            return position;
        }

        public string Name { get; }
    }
}