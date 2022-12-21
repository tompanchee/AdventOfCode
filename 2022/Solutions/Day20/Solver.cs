using AOCCommon;
using Serilog.Core;

namespace Day20;

[Day(20, "Grove Positioning System")]
internal class Solver : SolverBase
{
    EncryptedFile? file;

    public Solver(string path, Logger logger) : base(path, logger) { }

    public override void Solve1() {
        logger.Information("Decrypting...");
        logger.Debug(file!.ValuesAsString());
        for (var idx = 0; idx < file.Data.Count; idx++) {
            var item = file.GetWithOriginalIndex(idx);
            file.MoveItem(item);
            logger.Debug(file.ValuesAsString());
        }

        var zeroItem = file.Data.Single(i => i.Value == 0);
        var zeroIndex = file.Data.IndexOf(zeroItem);
        var sum = file[zeroIndex + 1000].Value + file[zeroIndex + 2000].Value + file[zeroIndex + 3000].Value;

        logger.Information("Sum of coordinates forming Grove location is {0}", sum);
    }

    public override void Solve2() {
        const long DECRYPTION_KEY = 811589153;

        PostConstruct(); // Reset data

        logger.Information("Applying Decryption key...");
        foreach (var item in file!.Data) item.Value *= DECRYPTION_KEY;

        for (var i = 0; i < 10; i++) {
            logger.Information("Running decryption round {0}", i + 1);
            for (var idx = 0; idx < file.Data.Count; idx++) {
                var item = file.GetWithOriginalIndex(idx);
                file.MoveItem(item);
            }

            logger.Debug(file.ValuesAsString());
        }

        var zeroItem = file.Data.Single(i => i.Value == 0);
        var zeroIndex = file.Data.IndexOf(zeroItem);
        var sum = file[zeroIndex + 1000].Value + file[zeroIndex + 2000].Value + file[zeroIndex + 3000].Value;

        logger.Information("Sum of coordinates forming Grove location is {0}", sum);
    }

    protected override void PostConstruct() {
        logger.Information("Reading position file...");
        file = EncryptedFile.Parse(data);
    }
}

internal class EncryptedFile
{
    public List<Item> Data { get; } = new();

    public Item this[int idx] => Data[idx % Data.Count];

    public static EncryptedFile Parse(IEnumerable<string> data) {
        var file = new EncryptedFile();
        file.Data.Clear();
        var idx = 0;
        foreach (var row in data) {
            if (string.IsNullOrWhiteSpace(row)) continue;
            file.Data.Add(new Item(long.Parse(row), idx++));
        }

        return file;
    }

    public Item GetWithOriginalIndex(int idx) {
        var item = Data.SingleOrDefault(i => i.OriginalIndex == idx);
        if (item == null) throw new IndexOutOfRangeException();
        return item;
    }

    public void MoveItem(Item item) {
        var idx = Data.IndexOf(item);

        if (item.Value == 0) return;
        var newIdx = Modulo(idx + item.Value, Data.Count - 1);

        Data.RemoveAt(idx);
        if (newIdx == 0) Data.Add(item);
        else Data.Insert((int) newIdx, item);

        static long Modulo(long a, long b) {
            var mod = a % b;
            // ReSharper: "Always false", bug?
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return mod >= 0 ? mod : mod + b;
        }
    }

    public string ValuesAsString() {
        return string.Join(',', Data.Select(i => i.Value));
    }
}