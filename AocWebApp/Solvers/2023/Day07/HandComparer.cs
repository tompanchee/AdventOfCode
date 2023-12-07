namespace Day07;

internal class HandComparer : IComparer<Hand>
{
    private const string CardOrder = "AKQJT98765432";
    private const string CardOrderWithJokers = "AKQT98765432J";

    private readonly bool useJokers;
    private readonly string order;

    public HandComparer(bool useJokers = false)
    {
        this.useJokers = useJokers;
        order = useJokers ? CardOrderWithJokers : CardOrder;
    }

    public int Compare(Hand? x, Hand? y)
    {
        if (x == null || y == null) return 0;

        var xt = x.ResolveType(useJokers);
        var yt = y.ResolveType(useJokers);

        if (xt < yt) return -1;
        if (xt > yt) return 1;

        for (int i = 0; i < 5; i++)
        {
            var ix = order.IndexOf(x.Cards[i]);
            var iy = order.IndexOf(y.Cards[i]);

            if (ix < iy) return -1;
            if (ix > iy) return 1;
        }

        return 0;
    }
}