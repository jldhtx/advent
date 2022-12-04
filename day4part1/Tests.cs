using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using System.Linq;
using Xunit.Abstractions;

namespace adventTests.day4part1;

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

}