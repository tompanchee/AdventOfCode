using System.Linq;
using Day21;
using Xunit;

namespace Day21Test;

public class GridTest
{
    [Fact]
    public void ShouldFlipHorizontally() {
        var grid = new Grid("..#", ".#.", "#..");
        grid.FlipHorizontally();
        Assert.Equal("#..", grid.Rows[0]);
        Assert.Equal(".#.", grid.Rows[1]);
        Assert.Equal("..#", grid.Rows[2]);

        grid = new Grid("...#", "..#.", ".#..", "#...");
        grid.FlipHorizontally();
        Assert.Equal("#...", grid.Rows[0]);
        Assert.Equal(".#..", grid.Rows[1]);
        Assert.Equal("..#.", grid.Rows[2]);
        Assert.Equal("...#", grid.Rows[3]);
    }

    [Fact]
    public void ShouldFLipVertically() {
        var grid = new Grid("..#", ".#.", "#..");
        grid.FlipVertically();
        Assert.Equal("#..", grid.Rows[0]);
        Assert.Equal(".#.", grid.Rows[1]);
        Assert.Equal("..#", grid.Rows[2]);

        grid = new Grid("...#", "..#.", ".#..", "#...");
        grid.FlipVertically();
        Assert.Equal("#...", grid.Rows[0]);
        Assert.Equal(".#..", grid.Rows[1]);
        Assert.Equal("..#.", grid.Rows[2]);
        Assert.Equal("...#", grid.Rows[3]);
    }

    [Fact]
    public void ShouldRotate() {
        // abc      cfi
        // def  =>  beh
        // ghi      adg
        var grid = new Grid("abc", "def", "ghi");
        grid.Rotate();
        Assert.Equal("cfi", grid.Rows[0]);
        Assert.Equal("beh", grid.Rows[1]);
        Assert.Equal("adg", grid.Rows[2]);

        // abcd      dhlp
        // efgh  =>  cgko
        // ijkl      bfjn
        // mnop      aeim
        grid = new Grid("abcd", "efgh", "ijkl", "mnop");
        grid.Rotate();
        Assert.Equal("dhlp", grid.Rows[0]);
        Assert.Equal("cgko", grid.Rows[1]);
        Assert.Equal("bfjn", grid.Rows[2]);
        Assert.Equal("aeim", grid.Rows[3]);
    }

    [Fact]
    public void ShouldSplit() {
        var grid = new Grid("abcd", "efgh", "ijkl", "mnop");
        var split = grid.SplitToSize(2).ToList();
        Assert.Equal(4, split.Count);
        Assert.Equal("ab", split[0].Rows[0]);
        Assert.Equal("ef", split[0].Rows[1]);
    }
}