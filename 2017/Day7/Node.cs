using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Node
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<Node> Children { get; } = new List<Node>();
        public Node Parent { get; set; }

        public long GetTotalWeight() => Weight + Children.Sum(child => child.GetTotalWeight());
    }
}