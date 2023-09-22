using System.Text.Json.Serialization;

namespace ToDoTaskManager.TelegramBot.Requests;

public record CreateNewToDoRequest ([property: JsonPropertyName("name")] string Name);
public record CreateNewToDoResponse([property: JsonPropertyName("id")] Guid Id);
