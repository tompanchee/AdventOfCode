namespace Day25;

internal static class SNAFUConverter
{
    public static string LongToSNAFU(this long value) {
        var result = "";
        while (value > 0) {
            var remainder = value % 5;
            if (remainder > 2) remainder -= 5;
            switch (remainder) {
                case 0:
                case 1:
                case 2:
                    result = $"{remainder}{result}";
                    break;
                case -1:
                    result = $"-{result}";
                    break;
                case -2:
                    result = $"={result}";
                    break;
            }

            value -= remainder;
            value /= 5;
        }

        return result;
    }

    public static long SNAFUToLong(this string? value) {
        value = new string(value?.Reverse().ToArray());
        var pos = 0;
        var result = 0L;
        foreach (var digit in value) {
            switch (digit) {
                case '0':
                    break;
                case '1':
                    result += Power(5, pos);
                    break;
                case '2':
                    result += 2 * Power(5, pos);
                    break;
                case '-':
                    result -= Power(5, pos);
                    break;
                case '=':
                    result -= 2 * Power(5, pos);
                    break;
            }

            pos++;
        }

        return result;
    }

    static long Power(long number, int power) {
        var result = 1L;
        for (var i = 0; i < power; i++) result *= number;
        return result;
    }
}