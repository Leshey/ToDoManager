using System;
using Xunit;
using ToDoManager.Core;

namespace ToDoManager.Tests;
public class ToDoTaskTests
{
    [Fact]
    public void CompleteTest()
    {
        //arrange
        var toDoTask = new ToDoTask("Test", new DateTime(2022, 7, 24, 20, 0, 0));
        var initialStatus = toDoTask.Status;

        //act
        toDoTask.Complete();

        //assert
        Assert.NotEqual(initialStatus, toDoTask.Status);
        Assert.True(toDoTask.Status);
    }

    [Fact]
    public void CompleteTestButTwice()
    {
        //arrange
        var toDoTask = new ToDoTask("Test", new DateTime(2022, 7, 24, 20, 0, 0));
        var initialStatus = toDoTask.Status;

        //act
        toDoTask.Complete();
        var deadline1 = toDoTask.TimeWhenCompleted;
        toDoTask.Complete();
        var deadline2 = toDoTask.TimeWhenCompleted;

        //assert
        Assert.NotEqual(initialStatus, toDoTask.Status);
        Assert.True(toDoTask.Status);
        Assert.Equal(deadline1, deadline2);
    }

    [Fact]
    public void ReopenWhileStatusIsOpenTest()
    {
        //arrange
        var toDoTask = new ToDoTask("Test", new DateTime(2022, 7, 24, 20, 0, 0));
        var expectedName = toDoTask.Name;
        var expectedDeadline = toDoTask.Deadline;
        var expectedTimeWhenCompleted = toDoTask.TimeWhenCompleted;
        var expectedStatus = toDoTask.Status;

        //act 
        toDoTask.Reopen();

        //asset
        Assert.Equal(expectedName, toDoTask.Name);
        Assert.Equal(expectedDeadline, toDoTask.Deadline);
        Assert.Equal(expectedTimeWhenCompleted, toDoTask.TimeWhenCompleted);
        Assert.Equal(expectedStatus, toDoTask.Status);
    }

    [Fact]
    public void ReopenWhileStatusIsCompleteTest()
    {
        //arrange
        var toDoTask = new ToDoTask("Test", new DateTime(2022, 7, 24, 20, 0, 0));
        toDoTask.Complete();

        //act
        var taskTimeWhenCompleted = toDoTask.TimeWhenCompleted;
        toDoTask.Reopen();

        //asset
        Assert.NotNull(taskTimeWhenCompleted);
        Assert.Null(toDoTask.TimeWhenCompleted);
    }

    [Fact]
    public void ReopenWithArgumentTest()
    {
        //arrange
        var toDoTask = new ToDoTask("Test", new DateTime(2022, 1, 1, 1, 1, 1));
        var newTime = new DateTime(2022, 2, 2, 2, 2, 2);
        toDoTask.Complete();

        //act
        var taskTimeWhenCompleted = toDoTask.TimeWhenCompleted;
        toDoTask.Reopen(newTime);

        //asset
        Assert.NotNull(taskTimeWhenCompleted);
        Assert.Null(toDoTask.TimeWhenCompleted);
        Assert.Equal(newTime, toDoTask.Deadline);
    }

    [Fact]
    public void PostponeTimeTest()
    {
        //arrange
        var toDoTask = new ToDoTask("Test", new DateTime(2022, 7, 24, 20, 0, 0));
        var initialTaskTime = toDoTask.Deadline;

        var newDeadline = new DateTime(2022, 7, 24, 22, 0, 0);
        var expectedTime = newDeadline;

        //act 
        toDoTask.PostponeDeadline(newDeadline);

        //asset
        Assert.NotEqual(initialTaskTime, toDoTask.Deadline);
        Assert.Equal(expectedTime, toDoTask.Deadline);
    }
}
