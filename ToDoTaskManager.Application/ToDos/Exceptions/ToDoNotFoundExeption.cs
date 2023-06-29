namespace ToDoTaskManager.Application.ToDos.Exceptions;

public sealed class ToDoNotFoundExeption : ApplicationExeption
{
    private const int STATUS_CODE = 404;

    public ToDoNotFoundExeption(Guid id) : base($"No ToDo has been found with this id: {id}", STATUS_CODE)
    {
    }
}
