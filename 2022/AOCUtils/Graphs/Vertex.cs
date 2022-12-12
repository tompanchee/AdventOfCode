namespace AOCUtils.Graphs;

public class Vertex<T>
{
    public Vertex(T id) {
        Id = id;
    }

    public T Id { get; }

    public IList<(int cost, T vertex)> Neighbours { get; } = new List<(int cost, T vertex)>();
}