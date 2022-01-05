using System.Linq;

namespace Day10
{
    class Bot : Item
    {
        public Bot(int id) : base(id) { }

        public Item LowTarget { get; set; }
        public Item HighTarget { get; set; }

        public void Give() {
            LowTarget.Add(LowChip);
            HighTarget.Add(HighChip);
            chips.Clear();
        }

        public int LowChip => chips.Min();
        public int HighChip => chips.Max();
    }
}