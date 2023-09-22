using System.Text.Json.Serialization;

namespace ToDoTaskManager.TelegramBot.Requests;

public record CloseByIdRequest([property: JsonPropertyName("id")] Guid Id);
public record CloseByIdResponse();
