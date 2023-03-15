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
        var scrambler = new Scrambler(new[] {operation});
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
}