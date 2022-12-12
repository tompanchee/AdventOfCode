namespace AOCUtils.Graphs;

public class Graph<T> where T : notnull
{
    public Graph(IEnumerable<Vertex<T>> vertices) {
        Vertices = vertices;
    }

    public IEnumerable<Vertex<T>> Vertices { get; }
}