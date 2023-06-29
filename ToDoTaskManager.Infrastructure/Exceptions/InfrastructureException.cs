namespace ToDoTaskManager.Infrastructure.Exceptions;

public abstract class InfrastructureException : Exception
{
    protected InfrastructureException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}
