namespace Day9;

public class StreamUtil
{
    readonly string stream;

    public StreamUtil(string stream) {
        this.stream = stream;
    }

    public int CalculateScore() {
        var score = 0;
        var depth = 0;

        var garbage = false;

        for (var i = 0; i < stream.Length; i++) {
            if (stream[i] == '!') {
                i++;
                continue;
            }

            if (stream[i] == '{' && !garbage) depth++;

            if (stream[i] == '}' && !garbage) {
                score += depth;
                depth--;
            }

            if (stream[i] == '<') garbage = true;
            if (stream[i] == '>') garbage = false;
        }

        return score;
    }

    public int CountGarbage() {
        var count = 0;

        var garbage = false;

        for (var i = 0; i < stream.Length; i++) {
            if (stream[i] == '!') {
                i++;
                continue;
            }

            if (stream[i] == '<' && !garbage) {
                garbage = true;
                continue;
            }

            if (stream[i] == '>' && garbage) {
                garbage = false;
                continue;
            }

            if (garbage) count++;
        }

        return count;
    }
}