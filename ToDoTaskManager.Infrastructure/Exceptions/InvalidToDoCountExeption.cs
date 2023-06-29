namespace ToDoTaskManager.Infrastructure.Exceptions;

public class InvalidToDoCountExeption : InfrastructureException
{
    private const int STATUS_CODE = 403;

    public InvalidToDoCountExeption() : base("ToDo count cannot be less than one", STATUS_CODE)
    {
    }
}
