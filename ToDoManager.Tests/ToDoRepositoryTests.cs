using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Core;
using Xunit;

namespace ToDoManager.Tests;

public class ToDoRepositoryTests
{
    [Fact]
    public void GetToDoWithinRangeTest()
    {
        //arrange
        var toDoTaskRepository = new ToDoTaskRepository();
        var toDoTask1 = new ToDoTask("Test", new DateTime(2022, 8, 10, 0, 0, 0));
        var toDoTask2 = new ToDoTask("Test", new DateTime(2022, 8, 4, 0, 0, 0));
        var toDoTask3 = new ToDoTask("Test", new DateTime(2022, 8, 6, 0, 0, 0));

        toDoTaskRepository.Add(toDoTask1);
        toDoTaskRepository.Add(toDoTask2);
        toDoTaskRepository.Add(toDoTask3);

        var expectedValue = new List<ToDoTask>();
        expectedValue.Add(toDoTask1);
        expectedValue.Add(toDoTask3);

        //act
        var actualValue = toDoTaskRepository.GetToDoWithinRange(new DateTime(2022, 8, 6), new DateTime(2022, 8, 12));

        //asset
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetToDoWithinRangeTestWithNulls()
    {
        //arrange
        var toDoTaskRepository = new ToDoTaskRepository();
        var toDoTask1 = new ToDoTask("Test", new DateTime(2022, 8, 10, 0, 0, 0));
        var toDoTask2 = new ToDoTask("Test", new DateTime(2022, 8, 4, 0, 0, 0));
        var toDoTask3 = new ToDoTask("Test", new DateTime(2022, 8, 6, 0, 0, 0));

        toDoTaskRepository.Add(toDoTask1);
        toDoTaskRepository.Add(toDoTask2);
        toDoTaskRepository.Add(toDoTask3);

        var expectedValue = new List<ToDoTask>
        {
            toDoTask1,
            toDoTask2,
            toDoTask3
        };

        //act
        var actualValue = toDoTaskRepository.GetToDoWithinRange(null, null);

        //asset
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetToDoWithinRangeTestWithNullFrom()
    {
        //arrange
        var toDoTaskRepository = new ToDoTaskRepository();
        var toDoTask1 = new ToDoTask("Test", new DateTime(2022, 8, 10, 0, 0, 0));
        var toDoTask2 = new ToDoTask("Test", new DateTime(2022, 8, 4, 0, 0, 0));
        var toDoTask3 = new ToDoTask("Test", new DateTime(2022, 8, 6, 0, 0, 0));

        toDoTaskRepository.Add(toDoTask1);
        toDoTaskRepository.Add(toDoTask2);
        toDoTaskRepository.Add(toDoTask3);

        var expectedValue = new List<ToDoTask>();
        expectedValue.Add(toDoTask2);
        expectedValue.Add(toDoTask3);

        //act
        var actualValue = toDoTaskRepository.GetToDoWithinRange(null, new DateTime(2022, 8, 9));

        //asset
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void GetToDoWithinRangeTestWithNullWithTo()
    {
        //arrange
        var toDoTaskRepository = new ToDoTaskRepository();
        var toDoTask1 = new ToDoTask("Test", new DateTime(2022, 8, 10, 0, 0, 0));
        var toDoTask2 = new ToDoTask("Test", new DateTime(2022, 8, 4, 0, 0, 0));
        var toDoTask3 = new ToDoTask("Test", new DateTime(2022, 8, 6, 0, 0, 0));

        toDoTaskRepository.Add(toDoTask1);
        toDoTaskRepository.Add(toDoTask2);
        toDoTaskRepository.Add(toDoTask3);

        var expectedValue = new List<ToDoTask>();
        expectedValue.Add(toDoTask1);
        expectedValue.Add(toDoTask3);

        //act
        var actualValue = toDoTaskRepository.GetToDoWithinRange(new DateTime(2022, 8, 5), null);

        //asset
        Assert.Equal(expectedValue, actualValue);
    }
}
