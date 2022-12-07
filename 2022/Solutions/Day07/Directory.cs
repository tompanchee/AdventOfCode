namespace Day07;

internal class Directory : Node
{
    public Directory(string name, Node? parent = null) : base(name, parent) { }
    
    public override long Size => CalculateSize();

    long CalculateSize() 
    {
        var sum = Files.Sum(f => f.Size);
        sum += Directories.Sum(d => d.Size);

        return sum;
    }
}