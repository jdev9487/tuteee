namespace JDev.Tuteee.Rest.ApiClient;

using System.Text;
using System.Text.Json;

public class BaseRestApiClient(HttpClient client, JsonSerializerOptions options)
{
    protected async Task<TResponseObject?> GetAsync<TResponseObject>(string uri, CancellationToken token = default)
    {
        var response = await client.GetAsync(uri, token);
        var json = await response.Content.ReadAsStringAsync(token);
        return JsonSerializer.Deserialize<TResponseObject>(json, options) ?? default;
    }

    protected async Task PostAsync<TRequest>(string uri, TRequest request, CancellationToken token = default)
    {
        using StringContent jsonContent =
            new(JsonSerializer.Serialize(request, options), Encoding.UTF8, "application/json");
        await client.PostAsync(uri, jsonContent, token);
    }

    protected async Task DeleteAsync(string uri, CancellationToken token = default) => await client.DeleteAsync(uri, token);
}