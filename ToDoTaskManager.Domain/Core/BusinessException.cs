namespace ToDoTaskManager.Domain.Core;

public abstract class BusinessExeption : Exception
{
    protected BusinessExeption(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}
