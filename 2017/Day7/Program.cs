using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("Building input tree");
            var tree = BuildTree();

            Console.WriteLine("Solving puzzle 1...");
            Console.WriteLine($"Name of bottom program is {tree.Name}");

            Console.WriteLine();

            Console.WriteLine("Solving puzzle 2...");
            var unbalancedChild = tree;
            Node next;
            do {
                next = FindUnbalancedChild(unbalancedChild);
                if (next != null) unbalancedChild = next;
            } while (next != null);

            var sibling = unbalancedChild.Parent.Children.First(c => c != unbalancedChild);
            var diff = sibling.GetTotalWeight() - unbalancedChild.GetTotalWeight();

            Console.WriteLine($"Balanced weight is {unbalancedChild.Weight + diff}");
        }

        static Node FindUnbalancedChild(Node node) {
            var totalWeights = node.Children.Select<Node, (Node node, long weight)>(c => (c, c.GetTotalWeight())).ToList();
            var (unbalanced, weight) = totalWeights.FirstOrDefault(w => totalWeights.Count(tw => tw.weight == w.weight) == 1);
            return weight == 0 ? null : unbalanced;
        }

        static Node BuildTree() {
            var input = File.ReadAllLines("input.txt");
            var allNodes = new HashSet<Node>();
            foreach (var line in input) {
                var name = line.Substring(0, line.IndexOf(' '));
                var weight = int.Parse(line[(line.IndexOf('(') + 1)..line.IndexOf(')')]);
                var children = new List<string>();
                if (line.Contains("->")) {
                    children = line[(line.IndexOf('>') + 1)..].Split(',', StringSplitOptions.TrimEntries).ToList();
                }

                var node = CreateOrFindNode(name, weight);

                foreach (var cNode in children.Select(child => CreateOrFindNode(child))) {
                    node.Children.Add(cNode);
                    cNode.Parent = node;
                }
            }

            var root = allNodes.FirstOrDefault(n => n.Parent == null);
            return root;

            Node CreateOrFindNode(string name, int? weight = null) {
                var node = allNodes.SingleOrDefault(n => n.Name.Equals(name));
                if (node != null) {
                    if (weight.HasValue) node.Weight = weight.Value;
                    return node;
                }

                node = new Node {
                    Name = name
                };
                if (weight.HasValue) node.Weight = weight.Value;
                allNodes.Add(node);

                return node;
            }
        }
    }
}