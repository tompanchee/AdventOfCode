namespace Day22;

internal class Instructions
{
    readonly string data;
    int pos = 0;

    public Instructions(string data) {
        this.data = data;
    }

    public string? GetNext() {
        if (pos >= data.Length) return null;

        if (char.IsLetter(data[pos])) return data[pos++].ToString();

        var offset = 0;
        while (pos + offset < data.Length && !char.IsLetter(data[pos + offset])) offset++;

        var result = data[pos..(pos + offset)];
        pos += offset;

        return result;
    }

    public void Reset()
    {
        pos = 0;
    }
}