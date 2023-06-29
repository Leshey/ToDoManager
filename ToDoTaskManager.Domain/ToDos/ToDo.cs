using ToDoTaskManager.Domain.Core;
using ToDoTaskManager.Domain.ToDos.Exceptions;

namespace ToDoTaskManager.Domain.ToDos;

public class ToDo
{
    internal ToDo(
        Guid id,
        string name,
        DateTime? doneTime)
    {
        Id = id;
        Name = name;
        DoneTime = doneTime;
    }

    public Guid Id { get; }
    public string Name { get; }
    public DateTime? DoneTime { get; private set; }
    public bool IsDone => DoneTime.HasValue;

    public void Close()
    {
        if (IsDone)
        {
            throw new CannotCloseToDoException(Id);
        }

        DoneTime = DateTimeProvider.UtcNow;
    }

    public override bool Equals(object? obj)
    {
        var todo = obj as ToDo;

        return todo is not null && todo.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static ToDo Create(string name)
    {
        var id = Guid.NewGuid();

        return new ToDo(id, name, null);
    }
}
