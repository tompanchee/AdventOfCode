using System.Collections.Generic;

namespace Day23
{
    static class CircularExtension
    {
        public static LinkedListNode<int> GetNextNode(this LinkedListNode<int> node, LinkedList<int> list) {
            return node.Next ?? list.First;
        }
    }
}