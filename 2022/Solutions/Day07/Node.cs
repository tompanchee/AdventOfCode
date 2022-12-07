namespace Day07;

internal abstract class Node
{
    protected Node(string name, Node? parent = null) {
        Name = name;
        Parent = parent;
    }

    public string Name { get; }

    public List<Node> Children { get; } = new();

    public Node? Parent { get; }

    public List<Directory> Directories => Children.OfType<Directory>().ToList();

    protected List<File> Files => Children.OfType<File>().ToList();

    public abstract long Size { get; }
}