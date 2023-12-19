namespace Day12;

public class Report
{
    public static Report FromInput(string input)
    {
        var split = input.Split(' ');
        var row = split[0];
        var groups = split[1]
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        return new Report(row, groups);
    }

    private Report(string row, List<int> groups)
    {
        Row = row;
        Groups = groups;
    }

    public string Row { get; }
    public List<int> Groups { get; }

    public List<string> ValidArrangements { get; } = new();

    public bool IsValid(string candidate)
    {
        var split = candidate.SplitToGroups();
        return split.Length == Groups.Count
               && !Groups.Where((t, i) => split[i].Length != t).Any();
    }

    // Got a lot of hints from Reddit discussions
    public long CalculateFoldedCombinations()
    {
        // Fold data
        var str = Row + "?" + Row + "?" + Row + "?" + Row + "?" + Row; // Simple string repetition
        var groups = new List<int>();
        for (int i = 0; i < 5; i++) groups.AddRange(Groups);

        // Initialize cache
        var cache = new Dictionary<(int idx, int groupIdx, int groupItemsCount), long>();

        return CalculateCombinationsWithCache(0, 0, 0);

        long CalculateCombinationsWithCache(int idx, int groupIdx, int groupItemCount)
        {
            if (cache.ContainsKey((idx, groupIdx, groupItemCount))) return cache[(idx, groupIdx, groupItemCount)];

            var count = CalculateCombinations(idx, groupIdx, groupItemCount);
            cache.Add((idx, groupIdx, groupItemCount), count);

            return count;
        }

        long CalculateCombinations(int idx, int groupIdx, int groupItemCount)
        {
            // Check end of string
            if (idx == str.Length)
            {
                // All groups are ok
                if (groupIdx == groups.Count - 1 && groupItemCount == groups[groupIdx]) return 1L;

                // All groups full and currently not processing group 
                if (groupIdx == groups.Count && groupItemCount == 0) return 1L;

                // All other combinations are invalid
                return 0L;
            }

            // If reached end of groups and there are items in group
            if (groupIdx >= groups.Count && groupItemCount > 0) return 0L;

            // Check '.'
            if (str[idx] == '.')
            {
                // If not processing group, continue calculating
                if (groupItemCount == 0) return CalculateCombinationsWithCache(idx + 1, groupIdx, groupItemCount);

                // If processing group and group is not full
                if (groups[groupIdx] > groupItemCount) return 0L;

                // Continue calculating to next group
                return CalculateCombinationsWithCache(idx + 1, groupIdx + 1, 0);
            }

            // Check '#'
            if (str[idx] == '#')
            {
                // Exceeded groups count
                if (groupIdx >= groups.Count) return 0L;

                // Processed group length exceeds input group length
                if (groupItemCount >= groups[groupIdx]) return 0L;

                // Continue processing current group
                return CalculateCombinationsWithCache(idx + 1, groupIdx, groupItemCount + 1);
            }

            // Check '?' - wildcard
            if (str[idx] == '?')
            {
                // If all groups full continue to check that there is no starting group
                if (groupIdx == groups.Count && groupItemCount == 0) return CalculateCombinationsWithCache(idx + 1, groupIdx, 0);

                // If not processing group, get values for both '.' and '#'
                if (groupItemCount == 0)
                {
                    return CalculateCombinationsWithCache(idx + 1, groupIdx, 0) // '.'
                           + CalculateCombinationsWithCache(idx + 1, groupIdx, 1); // '#'
                }

                // If group not yet finished continue with '#'
                if (groupItemCount < groups[groupIdx]) return CalculateCombinationsWithCache(idx + 1, groupIdx, groupItemCount + 1);

                // If group is full continue with '.' in next group
                if (groupItemCount == groups[groupIdx]) return CalculateCombinationsWithCache(idx + 1, groupIdx + 1, 0);
            }

            return 0L;
        }
    }
}