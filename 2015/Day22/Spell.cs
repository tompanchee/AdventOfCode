namespace Day22
{
    class Spell
    {
        public Spell(string name, int cost) {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public int Cost { get; }
        public int Damage { get; set; }
        public int Heal { get; set; }
        public int Armor { get; set; }
        public int Recharge { get; set; }
        public int Duration { get; set; }
    }
}