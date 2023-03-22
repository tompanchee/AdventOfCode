using Day21;

namespace Day21Test;

public class ScramblerTest
{
    [Theory]
    [InlineData("swap position 4 with position 0", "abcde", "ebcda")]
    [InlineData("swap letter d with letter b", "ebcda", "edcba")]
    [InlineData("reverse positions 0 through 4", "edcba", "abcde")]
    [InlineData("rotate left 1 step", "abcde", "bcdea")]
    [InlineData("rotate right 2 step", "abcde", "deabc")]
    [InlineData("move position 1 to position 4", "bcdea", "bdeac")]
    [InlineData("move position 3 to position 0", "bdeac", "abdec")]
    [InlineData("rotate based on position of letter b", "abdec", "ecabd")]
    [InlineData("rotate based on position of letter d", "ecabd", "decab")]
    [InlineData("swap position 2 with position 7", "abcdefgh", "abhdefgc")]
    [InlineData("swap letter f with letter a", "abhdefgc", "fbhdeagc")]
    [InlineData("swap letter c with letter a", "fbhdeagc", "fbhdecga")]
    [InlineData("rotate based on position of letter g", "fbhdecga", "fbhdecga")]
    [InlineData("rotate based on position of letter f", "fbhdecga", "afbhdecg")]
    [InlineData("rotate based on position of letter b", "afbhdecg", "ecgafbhd")]
    [InlineData("reverse positions 4 through 7", "abcdefgh", "abcdhgfe")]
    [InlineData("swap position 0 with position 1", "abcdefgh", "bacdefgh")]
    public void ShouldScramble(string operation, string input, string expected) {
        var scrambler = new Scrambler(new[] { operation });
        Assert.Equal(expected, scrambler.Scramble(input));
    }

    [Fact]
    public void ShouldScrambleWithMultipleInstructions() {
        var operations = new[] {
            "swap position 4 with position 0",
            "swap letter d with letter b",
            "reverse positions 0 through 4",
            "rotate left 1 step", "abcde",
            "move position 1 to position 4",
            "move position 3 to position 0",
            "rotate based on position of letter b",
            "rotate based on position of letter d"
        };

        var scrambler = new Scrambler(operations);
        Assert.Equal("decab", scrambler.Scramble("abcde"));
    }

    [Theory]
    [InlineData(0, 7, "76543210")]
    [InlineData(0, 1, "10234567")]
    [InlineData(0, 2, "21034567")]
    [InlineData(0, 3, "32104567")]
    [InlineData(1, 2, "02134567")]
    [InlineData(1, 3, "03214567")]
    [InlineData(6, 7, "01234576")]
    [InlineData(5, 7, "01234765")]
    [InlineData(4, 7, "01237654")]
    [InlineData(3, 4, "01243567")]
    [InlineData(3, 5, "01254367")]
    public void ShouldReverse(int start, int stop, string expected) {
        var operation = $"reverse positions {start} through {stop}";
        var scrambler = new Scrambler(new[] { operation });
        Assert.Equal(expected, scrambler.Scramble("01234567"));
    }

    [Theory]
    [InlineData('a', 'b', "bacdefgh")]
    [InlineData('a', 'c', "cbadefgh")]
    [InlineData('a', 'h', "hbcdefga")]
    [InlineData('g', 'h', "abcdefhg")]
    [InlineData('c', 'd', "abdcefgh")]
    public void ShouldSwapCharacters(char c1, char c2, string expected) {
        var operation = $"swap letter {c1} with letter {c2}";
        var scrambler = new Scrambler(new[] { operation });
        Assert.Equal(expected, scrambler.Scramble("abcdefgh"));
    }

    [Theory]
    [InlineData(0, 1, "10234567")]
    [InlineData(0, 2, "21034567")]
    [InlineData(0, 7, "71234560")]
    [InlineData(1, 7, "07234561")]
    [InlineData(6, 7, "01234576")]
    [InlineData(3, 4, "01243567")]
    public void ShouldSwapPositions(int p1, int p2, string expected) {
        var operation = $"swap position {p1} with position {p2}";
        var scrambler = new Scrambler(new[] { operation });
        Assert.Equal(expected, scrambler.Scramble("01234567"));
    }

    [Theory]
    [InlineData(0, 7, "12345670")]
    [InlineData(7, 0, "70123456")]
    [InlineData(1, 2, "02134567")]
    [InlineData(2, 1, "02134567")]
    [InlineData(6, 7, "01234576")]
    [InlineData(7, 6, "01234576")]
    public void ShouldMove(int p1, int p2, string expected) {
        var operation = $"move position {p1} to position {p2}";
        var scrambler = new Scrambler(new[] { operation });
        Assert.Equal(expected, scrambler.Scramble("01234567"));
    }
}