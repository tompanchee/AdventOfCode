using Day12;
using Xunit;

namespace Day12Test
{
    public class FoldedCombinationsTest
    {
        [Theory]
        [InlineData("???.### 1,1,3", 1L)]
        [InlineData(".??..??...?##. 1,1,3", 16384L)]
        [InlineData("?#?#?#?#?#?#?#? 1,3,1,6", 1L)]
        [InlineData("????.#...#... 4,1,1", 16L)]
        [InlineData("????.######..#####. 1,6,5", 2500L)]
        [InlineData("?###???????? 3,2,1", 506250L)]
        public void ShouldCalculateCombinations(string input, long expected)
        {
            var report = Report.FromInput(input);
            Assert.Equal(expected, report.CalculateFoldedCombinations());
        }
    }
}