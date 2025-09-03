namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class LessonRestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), ILessonRestApiClient
{
    public async Task<LessonDto?> GetAsync(int id, CancellationToken token) =>
        await GetAsync<LessonDto?>($"{Endpoint.LessonBase}/{id}", token);

    public async Task AddAsync(LessonDto lesson, CancellationToken token) =>
        await PostAsync(Endpoint.LessonBase, lesson, token);

    public async Task EditAsync(LessonDto lesson, CancellationToken token) =>
        await PatchAsync(Endpoint.LessonBase, lesson, token);
}