using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Day11
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }
    }

    class Floor
    {
        private readonly IList<string> items = new List<string>();

        public void Remove(string item) {
            items.Remove(item);
        }

        public void Add(string item) {
            items.Add(item);
        }

        public IEnumerable<string> Items => items;

        bool Equals(Floor other) {
            return items.OrderBy(i => i).SequenceEqual(other.Items.OrderBy(i => i));
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Floor) obj);
        }

        public override int GetHashCode() {
            return (items != null ? items.GetHashCode() : 0);
        }
    }

    class Building
    {
        private readonly int elevator = 0;

        private readonly Floor[] floors = new Floor[4] {
            new Floor(), new Floor(), new Floor(), new Floor()
        };

        public (int[] elevatorDiff, IEnumerable<IList<string>>) GetAllowedMoves() {
            var moves = floors[elevator].Items.Subsets(2).Where(s => s.Count > 0);
            var allMoves = elevator switch {
                0 => (new[] {1}, moves),
                3 => (new[] {-1}, moves),
                _ => (new[] {-1, 1}, moves)
            };

            // TODO: Filter non allowed moves

            return allMoves;
        }

        protected bool Equals(Building other) {
            return elevator == other.elevator
                   && floors[0].Equals(other.floors[0])
                   && floors[1].Equals(other.floors[1])
                   && floors[2].Equals(other.floors[2])
                   && floors[3].Equals(other.floors[3]);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Building) obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(elevator, floors);
        }
    }
}