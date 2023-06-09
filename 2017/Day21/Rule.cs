namespace Day21;

internal class Rule
{
    public readonly Grid from;
    public readonly Grid to;

    Rule(Grid from, Grid to) {
        this.from = from;
        this.to = to;
    }

    public static Rule FromRuleBook(string value) {
        var rules = value.Split("=>", StringSplitOptions.TrimEntries);
        var fromRule = rules[0].Split('/');
        var toRule = rules[1].Split('/');

        return new Rule(new Grid(fromRule), new Grid(toRule));
    }

    public Grid? Transform(Grid source) {
        if (source.Size != from.Size) return null;
        for (var f = 0; f < 4; f++) {
            if (from.Equals(source)) return to;
            if (f % 2 == 0) source.FlipVertically();
            else source.FlipHorizontally();
            if (from.Equals(source)) return to;
            source.Rotate();
        }

        return null;
    }
}