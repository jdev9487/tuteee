namespace JDev.Tuteee.ApiClient;

using System.Text;
using System.Text.Json;
using DTOs;

public class ApiClient(HttpClient client) : IApiClient
{
    public async Task<ClientDto?> GetClientAsync(int id) => await GetAsync<ClientDto?>($"/accounts/{id}");

    public async Task<IReadOnlyList<ClientDto>> GetClientsAsync() =>
        await GetAsync<IReadOnlyList<ClientDto>>("/accounts") ?? [];

    public async Task AddClientAsync(ClientDto clientDto) => await PostAsync("/accounts", clientDto);

    public async Task<TuteeDto?> GetTuteeAsync(int id) => await GetAsync<TuteeDto?>($"/tutees/{id}");

    private async Task<TResponseObject?> GetAsync<TResponseObject>(string uri)
    {
        var response = await client.GetAsync(uri);
        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<TResponseObject>(json, options) ?? default;
    }

    private async Task PostAsync<TRequest>(string uri, TRequest request)
    {
        using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        await client.PostAsync(uri, jsonContent);
    }
}