using System;
using System.IO;
using System.Text;
using Xunit;

namespace ToDoManager.Tests;

public class FileTests
{
    [Fact]
    public void CreateFile()
    {
        string path = "helloworld.txt";
        StreamWriter streamWriter = File.CreateText(path);
        streamWriter.WriteLine("Hello World!");
        streamWriter.WriteLine("Hello World!");
        streamWriter.WriteLine("Hello World!");
        streamWriter.WriteLine("Hello World!");
        streamWriter.Dispose();

        using StreamReader streamReader = File.OpenText(path);

        string? message = null;
        
        while ((message = streamReader.ReadLine()) != null)
        {
            Assert.Equal("Hello World!", message);
        }    
    }

    [Fact]
    public void ParseFile()
    {
        var path = "parsefile.txt";

        using(var streamWriter = File.CreateText(path))
        {
            streamWriter.WriteLine("$№IntValue:100№BoolValue:true№");
        }

        using var streamReader = File.OpenText(path);

        int? temp = null;
        TestDto? testDto = null;
        bool readPropertyDescription = false;
        StringBuilder propertyDescription = new StringBuilder();
        StringBuilder value = new StringBuilder();

        do
        {
            temp = streamReader.Read();
            if(temp.Value == -1)
            {
                break;
            }
            var charSymbol = Convert.ToChar(temp);
            
            if(charSymbol == '$')
            {
                testDto = new TestDto();
            }
            else if(charSymbol == '№')
            {
                if(propertyDescription.ToString() == "IntValue")
                {
                    testDto.IntValue = int.Parse(value.ToString());
                }
                else if(propertyDescription.ToString() == "BoolValue")
                {
                    testDto.BoolValue = bool.Parse(value.ToString());
                }
                propertyDescription.Clear();
                readPropertyDescription = true;
            }
            else if (readPropertyDescription)
            {
                if(charSymbol == ':')
                {
                    value.Clear();
                    readPropertyDescription = false;
                }
                else
                {
                    propertyDescription.Append(charSymbol);
                }
            }
            else if(readPropertyDescription == false)
            {
                value.Append(charSymbol);
            }
        }
        while (true);

        Assert.NotNull(testDto);
        Assert.Equal(100, testDto.IntValue);
        Assert.True(testDto.BoolValue);
    }

    private class TestDto
    {
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }
    }
}
