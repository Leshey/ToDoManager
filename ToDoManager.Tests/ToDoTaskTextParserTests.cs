using System;
using System.Collections.Generic;
using System.IO;
using ToDoManager.Core;
using Xunit;

namespace ToDoManager.Tests;

public class ToDoTaskTextParserTests
{
    [Fact]
    public void SaveInFileTest()
    {
        //arrange
        var path = "dataSaveInFile.txt";
        var parserConverter = new ParserConverter();

        var toDoTask1 = new ToDoTask("Test", new DateTime(1, 1, 1, 1, 1, 1));
        var toDoTask2 = new ToDoTask("Test", new DateTime(2, 2, 2, 2, 2, 2), new DateTime(2, 2, 2, 3, 3, 3));
        var toDoTask3 = new ToDoTask("Test", new DateTime(3, 3, 3, 3, 3, 3));

        var data = new Dictionary<uint, ToDoTask>();
        data.Add(toDoTask1.Id, toDoTask1);
        data.Add(toDoTask2.Id, toDoTask2);
        data.Add(toDoTask3.Id, toDoTask3);

        var toDoTaskTextParser = new ToDoTaskTextParser(path);

        var expected1 = parserConverter.ConvertToString(toDoTask1);
        var expected2 = parserConverter.ConvertToString(toDoTask2);
        var expected3 = parserConverter.ConvertToString(toDoTask3);

        //act 
        toDoTaskTextParser.SaveInFile(data.Values);

        //assert
        string[] actualArray = new string[3];
        int index = 0;

        using StreamReader streamReader = File.OpenText(path);
        string? message = null;
        
        while ((message = streamReader.ReadLine()) != null)
        {
            actualArray[index] = message;
            index += 1;
        }

        Assert.Equal(expected1, actualArray[0]);
        Assert.Equal(expected2, actualArray[1]);
        Assert.Equal(expected3, actualArray[2]);
    }

    [Fact]
    public void PrepareStringToSaveTest()
    {
        //arrange
        var parserConverter = new ParserConverter();

        var toDoTask1 = new ToDoTask("Test", new DateTime(2, 2, 2, 2, 2, 2), new DateTime(3, 3, 3, 3, 3, 3));
        var expected1 = $"$№Id:{toDoTask1.Id}№Name:Test№Deadline:02.02.0002 2:02:02№TimeWhenCompleted:03.03.0003 3:03:03№%";
        var toDoTask2 = new ToDoTask("This is not test", new DateTime(5, 5, 5, 5, 5, 5));
        var expected2 = $"$№Id:{toDoTask2.Id}№Name:This is not test№Deadline:05.05.0005 5:05:05№TimeWhenCompleted:№%";

        //act
        var actual1 = parserConverter.ConvertToString(toDoTask1);
        var actual2 = parserConverter.ConvertToString(toDoTask2);

        //assert
        Assert.Equal(expected1, actual1);
        Assert.Equal(expected2, actual2);
    }

    [Fact]
    public void ReadFromFileTest()
    {
        //arrange
        var path = "dataStringToSave.txt";
        var toDoTask1 = new ToDoTask("Test", new DateTime(1, 1, 1, 0, 0, 0));
        var toDoTask2 = new ToDoTask("Test", new DateTime(2, 2, 2, 0, 0, 0), new DateTime(2, 2, 2, 1, 1, 1));
        var toDoTask3 = new ToDoTask("Test", new DateTime(3, 3, 3, 0, 0, 0));

        var data = new List<ToDoTask>();
        data.Add(toDoTask1);
        data.Add(toDoTask2);
        data.Add(toDoTask3);

        var expected = data;

        var toDoTaskTextParser = new ToDoTaskTextParser(path);
        toDoTaskTextParser.SaveInFile(data);

        //act
        var actual = toDoTaskTextParser.ReadFromFile();

        //assert
        Assert.Equal(expected, actual);
    }

}
