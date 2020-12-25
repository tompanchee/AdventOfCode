using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    class CrabCups
    {
        private readonly int numberOfCups;
        private int max;
        private LinkedListNode<int> current;

        readonly LinkedList<int> cups = new LinkedList<int>();
        private readonly IDictionary<int, LinkedListNode<int>> lookup = new Dictionary<int, LinkedListNode<int>>();

        public CrabCups(int numberOfCups, string input) {
            this.numberOfCups = numberOfCups;

            Init(input);
        }

        private void Init(string input) {
            LinkedListNode<int> node = null;

            var values = input.Select(c => int.Parse(new string(c, 1))).ToArray();
            var maxValue = values.Max();

            for (var i = 0; i < numberOfCups; i++) {
                var value = i < input.Length ? values[i] : maxValue + i - input.Length + 1;
                node = Add(node, value);
            }

            max = Math.Max(maxValue, maxValue + numberOfCups - input.Length);
        }

        public void Play(int rounds) {
            current = cups.First;

            for (var i = 0; i < rounds; i++) {
                var pickup = new[] {
                    current.GetNextNode(cups).Value,
                    current.GetNextNode(cups).GetNextNode(cups).Value,
                    current.GetNextNode(cups).GetNextNode(cups).GetNextNode(cups).Value
                };

                var target = GetTarget(current.Value, pickup);
                var targetNode = Find(target);

                // Remove pickup
                Remove(current.GetNextNode(cups).GetNextNode(cups).GetNextNode(cups));
                Remove(current.GetNextNode(cups).GetNextNode(cups));
                Remove(current.GetNextNode(cups));

                var node = Add(targetNode, pickup[0]);
                node = Add(node, pickup[1]);
                Add(node, pickup[2]);

                current = current.GetNextNode(cups);
            }
        }

        int GetTarget(int value, int[] forbidden) {
            var target = value - 1;
            while (!IsValid()) {
                target -= 1;
                if (target < 1) target = max;
            }

            return target;

            bool IsValid() => target > 0 && !forbidden.Contains(target);
        }

        LinkedListNode<int> Add(LinkedListNode<int> node, int value) {
            var newNode = node == null
                ? cups.AddFirst(value)
                : cups.AddAfter(node, value);

            lookup[value] = newNode;
            return newNode;
        }

        void Remove(LinkedListNode<int> node) {
            cups.Remove(node);
            lookup.Remove(node.Value);
        }

        LinkedListNode<int> Find(int value) {
            return lookup[value];
        }

        public LinkedList<int> Cups => cups;
    }
}