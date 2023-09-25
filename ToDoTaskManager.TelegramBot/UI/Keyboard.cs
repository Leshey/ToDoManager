using Telegram.Bot.Types.ReplyMarkups;

namespace ToDoTaskManager.TelegramBot.UI;

public abstract class Keyboard
{
    public abstract InlineKeyboardMarkup RenderKeyboard();
}

public class MessageKeyboard : Keyboard
{
    private List<List<InlineKeyboardButton>> _keyboard;
    private int _lineIndex;
    
    public MessageKeyboard() 
    {
        _keyboard = new List<List<InlineKeyboardButton>>
        {
            new List<InlineKeyboardButton>()
        };
        _lineIndex = 0;
    }

    public void AddButton(Buttons button) 
    {
        _keyboard[_lineIndex].Add(ButtonsConverter(button));
    }

    public void AddLine()
    {
        if (_keyboard[0].Count == 0)
        {
            throw new InvalidOperationException("Line contains no buttons");
        }

        _keyboard.Add(new List<InlineKeyboardButton>());
        _lineIndex++;
    }

    public override InlineKeyboardMarkup RenderKeyboard()
    {
        if (_keyboard[0].Count == 0) 
        {
            throw new InvalidOperationException("Keyboard contains no buttons");
        }

        return new InlineKeyboardMarkup(_keyboard.Select(x => x.ToArray()).ToArray());
    }

    private InlineKeyboardButton ButtonsConverter(Buttons button) 
    {
        switch (button) 
        {
            case Buttons.Back:
                return InlineKeyboardButton.WithCallbackData("\U000021A9", "back");
            case Buttons.NextPage:
                return InlineKeyboardButton.WithCallbackData("<<<", "nextpage");
            case Buttons.PreviousPage:
                return InlineKeyboardButton.WithCallbackData(">>>", "previospage");
            case Buttons.CreateNewToDo:
                return InlineKeyboardButton.WithCallbackData("Create new Todo", "createnewtodo");
            case Buttons.ManageToDos:
                return InlineKeyboardButton.WithCallbackData("Manage ToDos", "managetodos");
            case Buttons.CloseToDo:
                return InlineKeyboardButton.WithCallbackData("Close ToDo", "closetodo");
            case Buttons.DeleteToDo:
                return InlineKeyboardButton.WithCallbackData("Delete ToDo", "deletetodo");
            case Buttons.ReopenToDo:
                return InlineKeyboardButton.WithCallbackData("Reopen ToDo", "reopentodo");
            case Buttons.ChangeName:
                return InlineKeyboardButton.WithCallbackData("Change name", "changename");
            case Buttons.ChangePriority:
                return InlineKeyboardButton.WithCallbackData("Change priority", "changepriority");
            case Buttons.ClosedToDos:
                return InlineKeyboardButton.WithCallbackData("Closed ToDos", "closedtodos");
            case Buttons.BackToMainMenu:
                return InlineKeyboardButton.WithCallbackData("Back to Main menu", "backtomainmenu");
            case Buttons.HighPriority:
                return InlineKeyboardButton.WithCallbackData("High \U0001F525", "highpriority");
            case Buttons.MediumPriority:
                return InlineKeyboardButton.WithCallbackData("Medium \U000023F0", "mediumpriority");
            case Buttons.LowPriority:
                return InlineKeyboardButton.WithCallbackData("Low \U0001F4A4", "lowpriority");
            default: throw new IndexOutOfRangeException();
        }  
    }
}

public enum Buttons 
{
    Back = 0,
    NextPage,
    PreviousPage,
    CreateNewToDo,
    ManageToDos,
    CloseToDo,
    DeleteToDo,
    ReopenToDo,
    ChangeName,
    ChangePriority,
    ClosedToDos,
    BackToMainMenu,
    HighPriority,
    MediumPriority,
    LowPriority,
}