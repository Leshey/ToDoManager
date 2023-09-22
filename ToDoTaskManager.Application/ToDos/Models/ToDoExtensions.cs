using ToDoTaskManager.Domain.ToDos;
namespace ToDoTaskManager.Application.ToDos.Models;

public static class ToDoExtensions
{
    public static IEnumerable<ToDoModel> ToToDoModels(this IEnumerable<ToDo> toDos)
    {
        return toDos.Select(x => new ToDoModel(x.Id, x.Name, x.DoneTime, x.IsDone));
    }
}
