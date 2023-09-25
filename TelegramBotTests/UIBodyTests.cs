using ToDoTaskManager.TelegramBot.UI;

namespace TelegramBotTests;

public class StringBodyTests
{
    [Fact]
    public void RenderTest()
    {
        //arrange
        var expectedString = "Test String";
        var stringBody = new StringBody(expectedString);

        //act
        var actualString = stringBody.Render();

        //assert
        Assert.Equal(expectedString, actualString);
    }
}

public class BotMessageBodyTests
{
    [Fact]
    public void RenderWithTitleTest()
    {
        //arrange
        var botMessageBody = new BotMessageBody();
        var header = "TestHeader";
        var title = "TestTitle";
        var botMessage = "TestBotMessage";

        var expectedRender = $"{header}{Environment.NewLine}{title}{Environment.NewLine}{botMessage}";

        //act
        botMessageBody.SetHeader(header);
        botMessageBody.SetTitle(title);
        botMessageBody.SetBotMessage(botMessage);

        var actualRender = botMessageBody.Render();

        //assert
        Assert.Equal(expectedRender, actualRender);
    }

    [Fact]
    public void RenderWithoutTitleTest()
    {
        //arrange
        var botMessageBody = new BotMessageBody();
        var header = "TestHeader";
        var botMessage = "TestBotMessage";

        var expectedRender = $"{header}{Environment.NewLine}{botMessage}";

        //act
        botMessageBody.SetHeader(header);
        botMessageBody.SetBotMessage(botMessage);

        var actualRender = botMessageBody.Render();

        //assert
        Assert.Equal(expectedRender, actualRender);
    }

    [Fact]
    public void RenderWithoutHeaderTest()
    {
        //arrange
        var botMessageBody = new BotMessageBody();
        var title = "TestTitle";
        var botMessage = "TestBotMessage";

        var expectedExeption = "Header cannot be empty";

        //act
        botMessageBody.SetTitle(title);
        botMessageBody.SetBotMessage(botMessage);

        var actualExeption = Assert.Throws<ArgumentException>(() => botMessageBody.Render());

        //assert
        Assert.Equal(expectedExeption, actualExeption.Message);
    }

    [Fact]
    public void RenderWithoutBotMessageTest()
    {
        //arrange
        var botMessageBody = new BotMessageBody();
        var header = "TestHeader";
        var title = "TestTitle";

        var expectedExeption = "Bot message cannot be empty";

        //act
        botMessageBody.SetHeader(header);
        botMessageBody.SetTitle(title);

        var actualExeption = Assert.Throws<ArgumentException>(() => botMessageBody.Render());

        //assert
        Assert.Equal(expectedExeption, actualExeption.Message);
    }
}

public class CurrentToDosBodyTests
{
    [Fact]
    public void RenderWithoutToDosTest() 
    {
        //arrange
        var emptyToDosList = new List<CurrentToDoDTO>();
        var currentToDosBody = new CurrentToDosBody(emptyToDosList);

        var expectedRender = "There is nothing to do! You are free! Yay!";

        //act
        var actualRender = currentToDosBody.Render();

        //assert
        Assert.Equal(expectedRender, actualRender);
    }

    [Fact]
    public void RenderWithToDosTest()
    {
        //arrange
        var dto1 = new CurrentToDoDTO("ToDo1", CurrentToDoPriority.High);
        var dto2 = new CurrentToDoDTO("ToDo2", CurrentToDoPriority.Medium);
        var dto3 = new CurrentToDoDTO("ToDo3", CurrentToDoPriority.Low);

        var header = "Current ToDos:";
        var highPriority = "🔥";
        var mediumPriority = "⏰";
        var lowPriority = "💤";

        var toDosList = new List<CurrentToDoDTO>() { dto1, dto2, dto3 };
        
        var currentToDosBody = new CurrentToDosBody(toDosList);

        var expectedRender = $"{header}{Environment.NewLine}" +
                            $"1. {dto1.Name} {highPriority}{Environment.NewLine}" +
                            $"2. {dto2.Name} {mediumPriority}{Environment.NewLine}" +
                            $"3. {dto3.Name} {lowPriority}{Environment.NewLine}";
        
        //act
        var actualRender = currentToDosBody.Render();

        //assert
        Assert.Equal(expectedRender, actualRender);
    }

}