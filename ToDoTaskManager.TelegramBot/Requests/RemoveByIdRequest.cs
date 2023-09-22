using System.Text.Json.Serialization;

namespace ToDoTaskManager.TelegramBot.Requests;

public record RemoveByIdRequest([property: JsonPropertyName("id")] Guid Id);
public record RemoveByIdResponse();
