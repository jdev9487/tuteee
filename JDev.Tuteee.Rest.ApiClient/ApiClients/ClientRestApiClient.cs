namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class ClientRestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), IClientRestApiClient
{
    public async Task<ClientDto?> GetAsync(int id, CancellationToken token) =>
        await GetAsync<ClientDto?>($"{Endpoint.ClientBase}/{id}", token);

    public async Task<IReadOnlyList<ClientDto>> GetListAsync(CancellationToken token) =>
        await GetAsync<IReadOnlyList<ClientDto>>(Endpoint.ClientBase, token) ?? [];

    public async Task AddAsync(ClientDto clientDto, CancellationToken token) =>
        await PostAsync(Endpoint.ClientBase, clientDto, token);
}