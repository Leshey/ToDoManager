using System.Text;

namespace ToDoManager.Core;

public class ToDoTaskTextParser
{
    private string _path;

    public ToDoTaskTextParser(string path)
    {
        _path = path;
    }

    public void SaveInFile(IEnumerable<ToDoTask> data)
    {
        var parserConverter = new ParserConverter();

        using var streamWriter = File.CreateText(_path);

        foreach (var toDoTask in data)
        {
            streamWriter.WriteLine(parserConverter.ConvertToString(toDoTask));
        }
    }

    public IEnumerable<ToDoTask> ReadFromFile()
    {
        List<ToDoTask> data = new List<ToDoTask>();

        using var streamReader = File.OpenText(_path);

        int? temp = null;
        ToDoTaskDto? toDoTaskDto = null;

        bool readPropertyDescription = false;
        StringBuilder propertyDescription = new StringBuilder();
        StringBuilder value = new StringBuilder();

        do
        {
            temp = streamReader.Read();
            if (temp.Value == -1)
            {
                break;
            }
            var charSymbol = Convert.ToChar(temp);

            if (charSymbol == '%')
            {
                ToDoTask toDoTask = new ToDoTask(toDoTaskDto.Id, toDoTaskDto.Name, toDoTaskDto.Deadline, toDoTaskDto.TimeWhenCompleted);
                data.Add(toDoTask);
            }
            else if (charSymbol == '$')
            {
                toDoTaskDto = new ToDoTaskDto();
            }
            else if (charSymbol == '№')
            {
                if (propertyDescription.ToString() == nameof(toDoTaskDto.Id))
                {
                    toDoTaskDto.Id = uint.Parse(value.ToString().TrimEnd());
                }
                else if (propertyDescription.ToString() == nameof(toDoTaskDto.Name))
                {
                    toDoTaskDto.Name = value.ToString().TrimEnd();
                }
                else if (propertyDescription.ToString() == nameof(toDoTaskDto.Deadline))
                {
                    toDoTaskDto.Deadline = DateTime.Parse(value.ToString().TrimEnd());
                }
                else if (propertyDescription.ToString() == nameof(toDoTaskDto.TimeWhenCompleted))
                {
                    if(value.ToString() == String.Empty)
                    {
                        toDoTaskDto.TimeWhenCompleted = null;
                    }
                    else
                    {
                        toDoTaskDto.TimeWhenCompleted = DateTime.Parse(value.ToString().TrimEnd());
                    }
                    
                }
                propertyDescription.Clear();
                readPropertyDescription = true;
            }
            else if (readPropertyDescription)
            {
                if (charSymbol == ':')
                {
                    value.Clear();
                    readPropertyDescription = false;
                }
                else
                {
                    propertyDescription.Append(charSymbol);
                }
            }
            else if (readPropertyDescription == false)
            {
                value.Append(charSymbol);
            }
        }
        while (true);

        return data;
    }

    private class ToDoTaskDto
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? TimeWhenCompleted { get; set; }
        public bool Status { get; set; }
    }    
}

public class ParserConverter
{
    public string ConvertToString(ToDoTask toDoTask)
    {
        StringBuilder toDoString = new StringBuilder();

        toDoString.Append("$№").Append(nameof(toDoTask.Id)).Append(":").Append(toDoTask.Id)
                  .Append("№").Append(nameof(toDoTask.Name)).Append(":").Append(toDoTask.Name)
                  .Append("№").Append(nameof(toDoTask.Deadline)).Append(":").Append(toDoTask.Deadline)
                  .Append("№").Append(nameof(toDoTask.TimeWhenCompleted)).Append(":").Append(toDoTask.TimeWhenCompleted)
                  .Append("№").Append("%");

        return toDoString.ToString();
    }
}
