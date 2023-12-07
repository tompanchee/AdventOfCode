namespace Day07;

internal class Hand
{
    private const string JokerReplacements = "AKQT98765432";

    private readonly int bid;

    public static Hand FromInput(string input)
    {
        return new Hand(
            input[..5],
            int.Parse(input[6..])
        );
    }

    private Hand(string cards, int bid)
    {
        Cards = cards;
        this.bid = bid;
    }

    public HandType ResolveType(bool useJokers = false)
    {
        if (useJokers)
        {
            var bestHand = HandType.High;
            foreach (char replacement in JokerReplacements)
            {
                var replaced = Cards.Replace('J', replacement);
                var jokerGroups = replaced.GroupBy(c => c).ToList();
                var type = ResolveFromGroups(jokerGroups);
                if (type < bestHand) bestHand = type;
            }

            return bestHand;
        }

        // No Jokers (part 1)
        var groups = Cards.GroupBy(c => c).ToList();
        return ResolveFromGroups(groups);

        static HandType ResolveFromGroups(IReadOnlyCollection<IGrouping<char, char>> g)
        {
            return g.Count switch
            {
                1 => HandType.FiveOfAKind,
                2 when g.Any(g => g.Count() == 4) => HandType.FourOfAKind,
                2 => HandType.FullHouse,
                3 when g.Any(g => g.Count() == 3) => HandType.ThreeOfAKind,
                3 => HandType.TwoPair,
                4 => HandType.OnePair,
                _ => HandType.High
            };
        }
    }

    public int CalculateWinning(int multiplier) => bid * multiplier;

    public string Cards { get; }

    internal enum HandType
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        High
    }
}