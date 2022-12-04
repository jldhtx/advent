using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using static RockPaperScissorGameChoice;

namespace adventTests.dec2;



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
        var test = () => { var choice = new RockPaperScissorGameChoice("i"); };
        test.Should().Throw<Exception>();
    }

    [Theory]
    [InlineData("X", MoveChoice.Rock)]
    [InlineData("Y", MoveChoice.Paper)]
    [InlineData("Z", MoveChoice.Scissors)]
    [InlineData("A", MoveChoice.Rock)]
    [InlineData("B", MoveChoice.Paper)]
    [InlineData("C", MoveChoice.Scissors)]
    public void ValidChoices(string character, MoveChoice expectedChoice)
    {
        var choice = new RockPaperScissorGameChoice(character);
        choice.Choice.Should().Be(expectedChoice);
    }

    [Theory]
    [InlineData(MoveChoice.Rock, MoveChoice.Rock, false, false, 4)]
    [InlineData(MoveChoice.Rock, MoveChoice.Paper, false, true, 1)]
    [InlineData(MoveChoice.Rock, MoveChoice.Scissors, true, false, 7)]
    [InlineData(MoveChoice.Paper, MoveChoice.Rock, true, false, 8)]
    [InlineData(MoveChoice.Paper, MoveChoice.Paper, false, false, 5)]
    [InlineData(MoveChoice.Paper, MoveChoice.Scissors, false, true, 2)]
    [InlineData(MoveChoice.Scissors, MoveChoice.Rock, false, true, 3)]
    [InlineData(MoveChoice.Scissors, MoveChoice.Paper, true, false, 9)]
    [InlineData(MoveChoice.Scissors, MoveChoice.Scissors, false, false, 6)]
    public void WinningRounds(MoveChoice me, MoveChoice them, bool expectMeToWin, bool expectThemToWin,
    int expectedTotalPoints)
    {
        var myChoice = new RockPaperScissorGameChoice(me);
        var theirChoice = new RockPaperScissorGameChoice(them);
        var round = new RockPaperScissorGameRound(myChoice, theirChoice);

        helper.WriteLine($"{me.ToString()} vs {them.ToString()} - I won: {round.IWon.ToString()} They Won: {round.TheyWon.ToString()}");
        round.IWon.Should().Be(expectMeToWin);
        round.TheyWon.Should().Be(expectThemToWin);
        round.TotalPoints.Should().Be(expectedTotalPoints);

    }
}