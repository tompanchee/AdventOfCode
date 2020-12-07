using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Bag
    {
        public static Bag Parse(string input) {
            var outerEnd = input.IndexOf("bags contain", StringComparison.InvariantCulture);
            var outer = input.Substring(0, outerEnd).Trim();

            var bag = new Bag(outer);

            if (!input.Contains("no other bags")) {
                var inners = input.Substring(outerEnd + "bags contain".Length).Trim().Split(',', StringSplitOptions.TrimEntries);
                foreach (var inner in inners) {
                    var amount = int.Parse(inner.Substring(0, 1));
                    var color = inner.Substring(1)
                        .Replace("bags.", "")
                        .Replace("bags", "")
                        .Replace("bag.", "")
                        .Replace("bag", "")
                        .Trim();
                    bag.InnerBags.Add(new InnerBag {
                        Bag = new Bag(color),
                        Amount = amount
                    });
                }
            }

            return bag;
        }

        private Bag(string color) {
            Color = color;
        }

        public string Color { get; }

        public List<InnerBag> InnerBags { get; } = new List<InnerBag>();

        public bool HasChildWithColor(string color) {
            if (InnerBags.Count == 0) return false;
            if (InnerBags.Any(b => b.Bag.Color == color)) return true;

            return InnerBags.Aggregate(false, (current, innerBag) => current || innerBag.Bag.HasChildWithColor(color));
        }

        public int TotalInnerBagCount
        {
            get
            {
                if (InnerBags.Count == 0) return 1;
                var sum = InnerBags.Sum(innerBag => innerBag.Amount * innerBag.Bag.TotalInnerBagCount);

                return sum + 1; // Add self
            }
        }

        public class InnerBag
        {
            public Bag Bag { get; set; }
            public int Amount { get; set; }
        }
    }
}