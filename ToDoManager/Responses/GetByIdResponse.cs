namespace ToDoTaskManager.WebApi.Responses;

public sealed record GetByIdResponse(Guid Id, string Name, DateTime? DoneTime, bool IsDone);