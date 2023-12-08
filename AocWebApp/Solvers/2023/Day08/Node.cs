namespace Day08;

internal class Node
{
    private Node(string name, string left, string right)
    {
        Name = name;
        Left = left;
        Right = right;
    }

    public static Node FromInput(string input)
    {
        var name = input[..3];
        var split = input[7..^1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return new Node(name, split[0], split[1]);
    }

    public string Name { get; }
    public string Left { get; }
    public string Right { get; }
}