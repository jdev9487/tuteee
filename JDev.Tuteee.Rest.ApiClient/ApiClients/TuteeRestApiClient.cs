namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class TuteeRestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), ITuteeRestApiClient
{
    public async Task<TuteeDto?> GetAsync(int id, CancellationToken token) =>
        await GetAsync<TuteeDto?>($"{Endpoint.TuteeBase}/{id}", token);

    public async Task AddAsync(TuteeDto tutee, CancellationToken token) =>
        await PostAsync(Endpoint.TuteeBase, tutee, token);
}