namespace ToDoTaskManager.Application.ToDos.Exceptions;

public abstract class ApplicationExeption : Exception
{
    protected ApplicationExeption(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}
