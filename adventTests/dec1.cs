using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace adventTests;

public class ElfCalorieCalculatorTests
{
    [Fact]
    public void ElfEmptyCalculator()
    {
        var calc = new ElfCalorieCalculator();
        calc.Elves.Should().BeEmpty();

    }

    [Fact]
    public void ElfNoElves()
    {
        var calc = new ElfCalorieCalculator();
        calc.Process(new string[] { });
        calc.Elves.Should().BeEmpty();

    }

    [Fact]
    public void ElfTwoElves()
    {
        var input = new List<string>();
        input.Add("100");
        input.Add("200");
        input.Add(string.Empty);

        input.Add("33");
        input.Add("66");
        input.Add(string.Empty);

        var calc = new ElfCalorieCalculator();
        calc.Process(input.ToArray());
        calc.Elves.Count.Should().Be(2);
        calc.Elves[0].Calories.Should().Be(300);
        calc.Elves[1].Calories.Should().Be(99);

    }

    [Fact]
    public void ElfThreeElvesLastLineNotEmpty()
    {
        var input = new List<string>();
        input.Add("100");
        input.Add("200");
        input.Add(string.Empty);

        input.Add("33");
        input.Add("66");
        input.Add(string.Empty);

        input.Add("11");
        input.Add("22");

        var calc = new ElfCalorieCalculator();
        calc.Process(input.ToArray());
        calc.Elves.Count.Should().Be(3);
        calc.Elves[0].Calories.Should().Be(300);
        calc.Elves[1].Calories.Should().Be(99);
        calc.Elves[2].Calories.Should().Be(33);

    }

    [Fact]
    public void ElfCaloriesStartsAtZero()
    {
        var elf = new ElfCalorieCalculator.Elf();
        elf.Calories.Should().Be(0);

    }

    [Fact]
    public void ElfCaloriesAddsValue()
    {
        var elf = new ElfCalorieCalculator.Elf();
        elf.AddCalories(new[] { "100" });
        elf.Calories.Should().Be(100);

    }

    [Fact]
    public void ElfCaloriesAddsValueMultiLine()
    {
        var elf = new ElfCalorieCalculator.Elf();
        elf.AddCalories(new[] { "100", "200" });
        elf.Calories.Should().Be(300);

    }

    [Fact]
    public void ElfCaloriesAddsNoValueWhenInvalid()
    {
        var elf = new ElfCalorieCalculator.Elf();
        elf.AddCalories(new[] { "asdf" });
        elf.Calories.Should().Be(0);

    }
}