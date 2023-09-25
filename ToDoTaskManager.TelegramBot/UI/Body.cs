using System.Text;

namespace ToDoTaskManager.TelegramBot.UI;

public abstract class Body
{
    public abstract string Render();
}

public class StringBody : Body 
{
    private readonly string _str;

    public StringBody(string str) 
    {
        _str = str;
    }
    
    public override string Render()
    {
        return _str;
    }
}

public class BotMessageBody : Body
{
    private string _header;
    private string? _title;
    private string _botMessage;

    public void SetHeader(string newHeader) 
    {
        _header = newHeader;
    }

    public void SetTitle(string newTitle)
    {
        _title = newTitle;
    }

    public void SetBotMessage(string newBotMessage)
    {
        _botMessage = newBotMessage;
    }

    public override string Render()
    {
        if (String.IsNullOrEmpty(_header)) 
        {
            throw new ArgumentException("Header cannot be empty");
        }

        if (String.IsNullOrEmpty(_botMessage))
        {
            throw new ArgumentException("Bot message cannot be empty");
        }

        return $"{_header}{Environment.NewLine}{(String.IsNullOrEmpty(_title) ? String.Empty : $"{_title}{Environment.NewLine}")}{_botMessage}";
    }
}

public class CurrentToDosBody : Body 
{
    private readonly IReadOnlyList<CurrentToDoDTO> _currentToDoDTOs;

    public CurrentToDosBody(IEnumerable<CurrentToDoDTO> toDoList) 
    {
        _currentToDoDTOs = toDoList.ToList().AsReadOnly();
    }

    public override string Render()
    {
        if (_currentToDoDTOs.Count == 0) 
        {
            return "There is nothing to do! You are free! Yay!";
        }

        var sb = new StringBuilder();

        sb.AppendLine("Current ToDos:");
        int count = 1;

        foreach (var toDo in _currentToDoDTOs) 
        {
            sb.AppendLine($"{count}. {toDo.Name} {PriorityToString(toDo.Priority)}");
            count++;
        }

        return sb.ToString();
    }

    private string PriorityToString(CurrentToDoPriority priority) 
    {
        switch (priority)
        {
            case CurrentToDoPriority.High:
                return "\U0001F525";
            case CurrentToDoPriority.Medium:
                return "\U000023F0";
            case CurrentToDoPriority.Low:
                return "\U0001F4A4";
            default : throw new IndexOutOfRangeException();
        }
    }
}

public record CurrentToDoDTO(string Name, CurrentToDoPriority Priority);
public enum CurrentToDoPriority 
{
    High = 0,
    Medium,
    Low,
}

