namespace Day21;

internal static class Enhancer
{
    public static Grid Enhance(Grid input, IList<Rule> rules) {
        var splitSize = input.Size % 2 == 0 ? 2 : 3;

        var split = input.SplitToSize(splitSize);
        var splitsEnhanced = new List<Grid>();

        foreach (var grid in split)
        foreach (var rule in rules) {
            var e = rule.Transform(grid);
            if (e != null) {
                splitsEnhanced.Add(e);
                break;
            }
        }

        var enhancedSize = (int)Math.Sqrt(splitsEnhanced.Count);
        var enhanced = new Grid[enhancedSize, enhancedSize];
        for (var r = 0; r < enhancedSize; r++)
        for (var c = 0; c < enhancedSize; c++)
            enhanced[r, c] = splitsEnhanced[r * enhancedSize + c];

        var newRows = new List<string>();

        for (var r = 0; r < enhancedSize; r++)
        for (var er = 0; er < enhanced[r, 0].Size; er++) {
            var newRow = string.Empty;
            for (var c = 0; c < enhancedSize; c++) newRow += enhanced[r, c].Rows[er];
            newRows.Add(newRow);
        }

        return new Grid(newRows.ToArray());
    }
}