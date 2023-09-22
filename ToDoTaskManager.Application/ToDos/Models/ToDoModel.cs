namespace ToDoTaskManager.Application.ToDos.Models;

public sealed record ToDoModel(Guid Id, string Name, DateTime? DoneTime, bool IsDone);