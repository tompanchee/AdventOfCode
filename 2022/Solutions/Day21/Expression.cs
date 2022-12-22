namespace Day21;

internal class Expression
{
    Expression(string left, string right, char @operator) {
        Left = left;
        Right = right;
        Operator = @operator;
    }

    public string Left { get; set; }
    public string Right { get; set; }
    public char Operator { get; }

    public long? Result
    {
        get
        {
            if (long.TryParse(Left, out var lValue) && long.TryParse(Right, out var rValue)) return Calculate(lValue, rValue);

            return null;
        }
    }

    public static Expression Parse(string expression) {
        var split = expression.Split(new[] {'+', '-', '*', '/'}, StringSplitOptions.TrimEntries);
        return new Expression(split[0], split[1], expression[5]);
    }

    long Calculate(long lValue, long rValue) {
        return Operator switch {
            '+' => lValue + rValue,
            '-' => lValue - rValue,
            '*' => lValue * rValue,
            '/' => lValue / rValue,
            _ => throw new InvalidOperationException(nameof(Operator))
        };
    }
}