using Newtonsoft.Json.Linq;

namespace Day13;

internal class Packet
{
    public Packet(JToken content) {
        Content = content;
    }

    public JToken Content { get; }

    public JToken? this[int index] => index >= Content.Count() ? null : Content[index];
}