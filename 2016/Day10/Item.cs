using System.Collections.Generic;

namespace Day10
{
    abstract class Item
    {
        protected readonly IList<int> chips = new List<int>();

        protected Item(int id) {
            ID = id;
        }

        public int ID { get; }

        public IList<int> Chips => chips;

        public void Add(int chip) {
            chips.Add(chip);
        }
    }
}