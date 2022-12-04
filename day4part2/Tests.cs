using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using System.Linq;
using Xunit.Abstractions;

namespace adventTests.day4part2;

public class ScheduleTests
{
    [Fact]
    public void InvalidSchedule()
    {
        var test = () =>
        {
            var schedule = new Schedule("2-56-8");
        };
        test.Should().Throw<Exception>();
    }

    [Fact]
    public void ValidSchedule()
    {
        var schedule = new Schedule("2-5,6-8");
        schedule.AssignmentOne.From.Should().Be(2);
        schedule.AssignmentOne.To.Should().Be(5);
        schedule.AssignmentTwo.From.Should().Be(6);
        schedule.AssignmentTwo.To.Should().Be(8);
    }


    [Fact]
    public void FullOverlapSchedule()
    {
        var schedule = new Schedule("2-8,4-6");
        schedule.AssignmentOne.From.Should().Be(2);
        schedule.AssignmentOne.To.Should().Be(8);
        schedule.AssignmentTwo.From.Should().Be(4);
        schedule.AssignmentTwo.To.Should().Be(6);
        schedule.IsFullyOverlapped().Should().BeTrue();
    }
    [Fact]
    public void NotOverlappedchedule()
    {
        var schedule = new Schedule("2-4,6-8");
        schedule.AssignmentOne.From.Should().Be(2);
        schedule.AssignmentOne.To.Should().Be(4);
        schedule.AssignmentTwo.From.Should().Be(6);
        schedule.AssignmentTwo.To.Should().Be(8);
        schedule.IsFullyOverlapped().Should().BeFalse();
        schedule.IsOverlapped().Should().BeFalse();
    }

    [Fact]
    public void PartialOverlappedchedule()
    {
        var schedule = new Schedule("2-4,3-8");
        schedule.AssignmentOne.From.Should().Be(2);
        schedule.AssignmentOne.To.Should().Be(4);
        schedule.AssignmentTwo.From.Should().Be(3);
        schedule.AssignmentTwo.To.Should().Be(8);
        schedule.IsOverlapped().Should().BeTrue();
        schedule.IsFullyOverlapped().Should().BeFalse();
    }
    [Fact]
    public void PartialOverlappedchedule2()
    {
        var schedule = new Schedule("5-9,3-8");
        schedule.AssignmentOne.From.Should().Be(5);
        schedule.AssignmentOne.To.Should().Be(9);
        schedule.AssignmentTwo.From.Should().Be(3);
        schedule.AssignmentTwo.To.Should().Be(8);
        schedule.IsOverlapped().Should().BeTrue();
        schedule.IsFullyOverlapped().Should().BeFalse();
    }
}
public class AssignmentTests
{
    private ITestOutputHelper helper;
    public AssignmentTests(ITestOutputHelper helper)
    {
        this.helper = helper;
    }


    [Fact]
    public void InvalidAssignment()
    {
        var test = () => { var assignment = new Assignment("222"); };
        test.Should().Throw<Exception>();

    }

    [Fact]
    public void InvalidTo()
    {
        var test = () => { var assignment = new Assignment("a-2"); };
        test.Should().Throw<Exception>();

    }

    [Fact]
    public void InvalidFrom()
    {
        var test = () => { var assignment = new Assignment("2-d"); };
        test.Should().Throw<Exception>();

    }

    [Fact]
    public void ValidToAndFrom()
    {
        var assignment = new Assignment("2-5");
        assignment.To.Should().Be(5);
        assignment.From.Should().Be(2);

    }
    [Fact]
    public void InvalidToAndFrom()
    {
        var test = () =>
        {
            var assignment = new Assignment("11-5");
        };

        test.Should().Throw<Exception>();

    }

    [Fact]
    public void OverlapsFalse()
    {
        var assignment = new Assignment("2-5");
        var assignment2 = new Assignment("6-8");
        assignment.Overlaps(assignment2).Should().BeFalse();


    }


    [Fact]
    public void LeftOverlapsTrue()
    {
        var assignment = new Assignment("2-5");
        var assignment2 = new Assignment("1-8");
        assignment.Overlaps(assignment2).Should().BeTrue();


    }

    [Fact]
    public void RightOverlapsTrue()
    {
        var assignment = new Assignment("2-7");
        var assignment2 = new Assignment("5-8");
        assignment2.Overlaps(assignment).Should().BeTrue();


    }

    [Fact]
    public void FullyOverlapsTrue()
    {
        var assignment = new Assignment("2-7");
        var assignment2 = new Assignment("3-5");

        assignment.FullyOverlaps(assignment2).Should().BeTrue();
        assignment2.FullyOverlaps(assignment).Should().BeFalse();


    }


}