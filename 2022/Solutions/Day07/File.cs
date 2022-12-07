namespace Day07;

internal class File : Node
{
    public File(string name, long size, Node? parent = null) : base(name, parent) 
    {
        Size = size;
    }
    
    public override long Size { get; }
}