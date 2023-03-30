using Day9;
using Xunit;

namespace Day9Test;

public class StreamUtilTest
{
    [Theory]
    [InlineData("{}", 1)]
    [InlineData("{{{}}}", 6)]
    [InlineData("{{},{}}", 5)]
    [InlineData("{{{},{},{{}}}}", 16)]
    [InlineData("{<a>,<a>,<a>,<a>}", 1)]
    [InlineData("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
    [InlineData("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
    [InlineData("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
    public void ShouldCalculateScore(string stream, int score) {
        var util = new StreamUtil(stream);
        Assert.Equal(score, util.CalculateScore());
    }

    [Theory]
    [InlineData("<>", 0)]
    [InlineData("<random characters>", 17)]
    [InlineData("<<<<>", 3)]
    [InlineData("<{!>}>", 2)]
    [InlineData("<!!>", 0)]
    [InlineData("<!!!>>", 0)]
    [InlineData("<{o\"i!a,<{i<a>", 10)]
    public void ShouldCountGarbage(string stream, int count) {
        var util = new StreamUtil(stream);
        Assert.Equal(count, util.CountGarbage());
    }
}