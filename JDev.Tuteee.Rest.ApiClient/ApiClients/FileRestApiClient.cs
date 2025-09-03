namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class FileRestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), IFileRestApiClient
{
    public async Task SaveTemporaryAsync(FileDto temporaryFile, CancellationToken token) =>
        await PostAsync("temporary-files", temporaryFile, token);
}