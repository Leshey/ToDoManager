using System.Text;
using System.Text.Json;
using ToDoTaskManager.TelegramBot.Requests;

namespace ToDoTaskManager.TelegramBot.Integration;

public record ToDoTaskManagerService
{
    private readonly ToDoTaskManagerClient _client;

    public ToDoTaskManagerService(ToDoTaskManagerClient client) 
    {
        _client = client;
    }

    public async Task<CreateNewToDoResponse> CreateNewToDo(CreateNewToDoRequest request, CancellationToken cancellationToken)
    {
        var uri = new Uri("/ToDo", UriKind.Relative);
        var method = HttpMethod.Post;
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync<CreateNewToDoResponse>(content, uri, method, cancellationToken);

        return response;
    }

    public async Task<RemoveByIdResponse> RemoveById(RemoveByIdRequest request, CancellationToken cancellationToken) 
    {
        var uri = new Uri($"/ToDo?id={request.Id}", UriKind.Relative);
        var method = HttpMethod.Delete;
        var content = default(HttpContent);

        var response = await _client.SendAsync<RemoveByIdResponse>(content, uri, method, cancellationToken);

        return response;
    }

    public async Task<CloseByIdResponse> CloseById(CloseByIdRequest request, CancellationToken cancellationToken) 
    {
        var uri = new Uri($"/ToDo?id={request.Id}", UriKind.Relative);
        var method = HttpMethod.Put;
        var content = default(HttpContent);

        var response = await _client.SendAsync<CloseByIdResponse>(content, uri, method, cancellationToken);

        return response;
    }
}
 