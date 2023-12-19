namespace Day12;

public static class Ext
{
    public static string[] SplitToGroups(this string input)
    {
        return input.Split('.', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    public static (int start, int length)[] SplitToDamagedGroups(this string input)
    {
        var result = new List<(int start, int length)>();

        int start = -1;
        for (int i = 0; i < input.Length; i++)
        {
            if (start > -1)
            {
                if (input[i] != '#')
                {
                    result.Add((start, i - start));
                    start = -1;
                }
            }
            else
            {
                if (input[i] == '#')
                {
                    start = i;
                }
            }
        }

        if (start > -1)
        {
            result.Add((start, input.Length - start));
        }

        return result.ToArray();
    }
}