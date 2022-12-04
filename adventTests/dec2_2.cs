using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using static RockPaperScissorGameChoice2;

namespace adventTests.dec2_2;



public class ElfRockPaperScissorTests
{
    private ITestOutputHelper helper;
    public ElfRockPaperScissorTests(ITestOutputHelper helper)
    {
        this.helper = helper;
    }
    [Fact]
    public void InvalidChoice()
    {
        var test = () => { var choice = new RockPaperScissorGameChoice2("i"); };
        test.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData("X", MoveChoice2.Rock)]
    [InlineData("Y", MoveChoice2.Paper)]
    [InlineData("Z", MoveChoice2.Scissors)]
    [InlineData("A", MoveChoice2.Rock)]
    [InlineData("B", MoveChoice2.Paper)]
    [InlineData("C", MoveChoice2.Scissors)]
    public void ValidChoices(string character, MoveChoice2 expectedChoice)
    {
        var choice = new RockPaperScissorGameChoice2(character);
        choice.Choice.Should().Be(expectedChoice);
    }

    [Theory]
    [InlineData(MoveChoice2.Rock, MoveChoice2.Rock, false, true, 3)]
    [InlineData(MoveChoice2.Rock, MoveChoice2.Paper, false, true, 1)]
    [InlineData(MoveChoice2.Rock, MoveChoice2.Scissors, false, true, 2)]
    [InlineData(MoveChoice2.Paper, MoveChoice2.Rock, false, false, 4)]
    [InlineData(MoveChoice2.Paper, MoveChoice2.Paper, false, false, 5)]
    [InlineData(MoveChoice2.Paper, MoveChoice2.Scissors, false, false, 6)]
    [InlineData(MoveChoice2.Scissors, MoveChoice2.Rock, true, false, 8)]
    [InlineData(MoveChoice2.Scissors, MoveChoice2.Paper, true, false, 9)]
    [InlineData(MoveChoice2.Scissors, MoveChoice2.Scissors, true, false, 7)]
    public void WinningRounds(MoveChoice2 me, MoveChoice2 them, bool expectMeToWin, bool expectThemToWin,
    int expectedTotalPoints)
    {
        var myChoice = new RockPaperScissorGameChoice2(me);
        var theirChoice = new RockPaperScissorGameChoice2(them);
        var round = new RockPaperScissorGameRound2(myChoice, theirChoice);

        helper.WriteLine($"{round.Me.Choice.ToString()} vs {theirChoice.Choice.ToString()} - I won: {round.IWon.ToString()} They Won: {round.TheyWon.ToString()}");
        helper.WriteLine($"expected: {expectedTotalPoints} - got {round.TotalPoints}");
        round.IWon.Should().Be(expectMeToWin);
        round.TheyWon.Should().Be(expectThemToWin);
        round.TotalPoints.Should().Be(expectedTotalPoints);

    }
}