using ToDoTaskManager.Domain.Core;

namespace ToDoTaskManager.Domain.ToDos.Exceptions;

public class CannotCloseToDoException : BusinessExeption
{
    private const int STATUS_CODE = 400;

    public CannotCloseToDoException(Guid id) : base($"Can't close toDo with id {id}", STATUS_CODE)
    {
    }
}
