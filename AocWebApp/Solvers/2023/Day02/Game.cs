namespace Day02;

class Game
{
    private const int MaxRed = 12;
    private const int MaxGreen = 13;
    private const int MaxBlue = 14;

    readonly private List<Round> rounds;

    private Game(int id, List<Round> rounds)
    {
        ID = id;
        this.rounds = rounds;
    }

    public static Game FromInput(string input)
    {
        var idx = input.IndexOf(':');
        var id = int.Parse(input[4..idx]);

        var roundsInput = input[(idx + 1)..];
        var split = roundsInput.Split(';');
        var rounds = new List<Round>();
        foreach (string color in split)
        {
            var colors = color.Split(',', StringSplitOptions.TrimEntries);
            var red = 0;
            var green = 0;
            var blue = 0;
            foreach (string col in colors)
            {
                var idx2 = col.IndexOf(' ');
                var c = col[(idx2 + 1)..];
                var amount = int.Parse(col[..idx2]);
                switch (c)
                {
                    case "red":
                        red = amount;
                        break;
                    case "green":
                        green = amount;
                        break;
                    case "blue":
                        blue = amount;
                        break;
                }
            }

            rounds.Add(new Round(red, green, blue));
        }

        return new Game(id, rounds);
    }

    public int ID { get; }

    public bool IsImpossible()
    {
        return rounds.Any(round => round.Red > MaxRed || round.Blue > MaxBlue || round.Green > MaxGreen);
    }

    public int Power()
    {
        var red = rounds.Select(r => r.Red).Max();
        var green = rounds.Select(r => r.Green).Max();
        var blue = rounds.Select(r => r.Blue).Max();

        return red * green * blue;
    }

    record Round(int Red, int Green, int Blue);
}