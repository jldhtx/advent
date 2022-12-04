using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using System.Linq;
using Xunit.Abstractions;
using static RockPaperScissorGameChoice;

namespace adventTests.day3;


public class RucksackTests
{
    private ITestOutputHelper helper;
    public RucksackTests(ITestOutputHelper helper)
    {
        this.helper = helper;
    }
    [Fact]
    public void InvalidRucksackLength()
    {
        var test = () =>
        {
            var sack = new Rucksack("123");
        };
        test.Should().Throw<Exception>();
    }

    [Fact]
    public void InvalidCharacters()
    {
        var test = () =>
        {
            var sack = new Rucksack("abc!");
        };
        test.Should().Throw<Exception>();
    }

    [Fact]
    public void Valid()
    {
        var sack = new Rucksack("ABde");
        sack.CompartmentOne.Items.Count.Should().Be(2);
        sack.CompartmentOne.Items.Should().Contain(i => i.Value == 'A');
        sack.CompartmentOne.Items.Should().Contain(i => i.Value == 'B');
        sack.CompartmentTwo.Items.Count.Should().Be(2);
        sack.CompartmentTwo.Items.Should().Contain(i => i.Value == 'd');
        sack.CompartmentTwo.Items.Should().Contain(i => i.Value == 'e');

    }

    [Fact]
    public void GetDuplicate()
    {
        var sack = new Rucksack("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL");
        helper.WriteLine(sack.ToString());
        var dup = sack.GetDuplicate();
        dup.Priority.Should().Be(38);

    }
}
public class RucksackCompartmentTests
{
    [Fact]
    public void EmptyItems()
    {
        var compartment = new RucksackCompartment("");
        compartment.Items.Should().NotBeNull();
        compartment.Items.Should().BeEmpty();
    }

    [Fact]
    public void ThreeItems()
    {
        var compartment = new RucksackCompartment("abc");
        compartment.Items.Should().NotBeNull();
        compartment.Items.Count.Should().Be(3);
    }

    [Fact]
    public void InvalidItem()
    {
        var test = () =>
        {
            var compartment = new RucksackCompartment("!");
        };
        test.Should().Throw<Exception>();
    }


}
public class RucksackItemTests
{
    [Fact]
    public void CreateItemInvalid()
    {

        var test = () => { var item = new RucksackItem('!'); };
        test.Should().Throw<Exception>();

    }

    [Theory]
    [MemberData(nameof(LowerCaseTestData))]
    public void CreateAllLowerCase(int expectedPriority, char characterToTest)
    {
        var item = new RucksackItem(characterToTest);
        item.Priority.Should().Be(expectedPriority);

    }

    [Theory]
    [MemberData(nameof(UpperCaseTestData))]
    public void CreateAllUpperCase(int expectedPriority, char characterToTest)
    {
        var item = new RucksackItem(characterToTest);
        item.Priority.Should().Be(expectedPriority);

    }

    public static IEnumerable<object[]> LowerCaseTestData()
    {
        return GetTestData(1, 26, 97, 122);
    }

    public static IEnumerable<object[]> UpperCaseTestData()
    {
        return GetTestData(27, 25, 65, 25);
    }

    private static IEnumerable<object[]> GetTestData(int priorityLow, int priorityCount,
    int characterLow, int characterCount)
    {
        var priorities = Enumerable.Range(priorityLow, priorityCount);
        var charactersToTest = Enumerable.Range(characterLow, characterCount);

        return priorities.Zip(charactersToTest, (p, c) => new object[] { p, c }).ToList();
    }
}