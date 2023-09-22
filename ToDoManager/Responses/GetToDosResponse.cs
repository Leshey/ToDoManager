namespace ToDoTaskManager.WebApi.Responses;

public sealed record GetToDosResponse(IEnumerable<GetToDosResponseItem> toDos);

public sealed record GetToDosResponseItem(Guid Id, string Name, DateTime? DoneTime, bool IsDone);