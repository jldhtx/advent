using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using static RockPaperScissorGameChoice;

namespace adventTests.dec2;

public class ElfRockPaperScissorTests
{
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
    public void InvalidChoice(string character, MoveChoice expectedChoice)
    {
        var choice = new RockPaperScissorGameChoice(character);
        choice.Choice.Should().Be(expectedChoice);
    }
}