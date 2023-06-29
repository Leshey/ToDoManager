namespace ToDoTaskManager.Infrastructure.Exceptions;

public class InvalidPageException : InfrastructureException
{
    private const int STATUS_CODE = 403;

    public InvalidPageException() : base("Page cannot be less than zero", STATUS_CODE)
    {
    }
}
