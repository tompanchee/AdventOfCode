namespace Day21;

internal class Monkey
{
    long? value;

    Monkey(string id, Expression? expression = null, long? value = null) {
        Id = id;
        Expression = expression;
        this.value = value;
    }

    public string Id { get; }

    public long? Value
    {
        get
        {
            if (value.HasValue) return value;
            if (Expression?.Result == null) return null;
            value = Expression.Result;
            return value;
        }
        set => this.value = value;
    }

    public Expression? Expression { get; }

    public static Monkey Parse(string input) {
        var split = input.Split(':', StringSplitOptions.TrimEntries);
        var id = split[0];
        return long.TryParse(split[1], out var value)
            ? new Monkey(id, value: value)
            : new Monkey(id, Expression.Parse(split[1]));
    }
}