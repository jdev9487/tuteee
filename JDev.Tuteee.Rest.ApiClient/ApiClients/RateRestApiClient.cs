namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class RateRestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), IRateRestApiClient
{
    public async Task AddAsync(int tuteeId, RateDto rateDto, CancellationToken token) =>
        await PostAsync($"{Endpoint.TuteeBase}/{tuteeId}/rates", rateDto, token);
}