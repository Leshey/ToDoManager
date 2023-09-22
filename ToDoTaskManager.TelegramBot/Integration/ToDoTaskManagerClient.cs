using System.Text.Json;

namespace ToDoTaskManager.TelegramBot.Integration;

public class ToDoTaskManagerClient
{
    private readonly Uri _host;

    public ToDoTaskManagerClient(string host) 
    {
        _host = new Uri(host, UriKind.Absolute);
    }

    public async Task<T> SendAsync<T>(
        HttpContent httpContent,
        Uri uri,
        HttpMethod httpMethod,
        CancellationToken cancellationToken)
    {
        using var client = new HttpClient();

        client.BaseAddress = _host;

        var requestMessage = new HttpRequestMessage(httpMethod, uri)
        {
            Content = httpContent
        };

        var responce = await client.SendAsync(requestMessage, cancellationToken);

        if (responce.IsSuccessStatusCode) 
        {
            var stream = await responce.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken);
        }

        throw new InvalidOperationException(
            $"Code: {responce.StatusCode}\nMessage: {await responce.Content.ReadAsStringAsync(cancellationToken)}"
        );
    }
}
