namespace Day12;

internal class Node
{
    public Node(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public IList<Node> Neighbours { get; } = new List<Node>();
}
