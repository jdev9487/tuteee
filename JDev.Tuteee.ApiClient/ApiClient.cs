namespace JDev.Tuteee.ApiClient;

using DTOs;
using System.Text;
using System.Text.Json;

public class ApiClient(HttpClient client) : IApiClient
{
    public async Task<ClientDto?> GetClientAsync(int id) => await GetAsync<ClientDto?>($"clients/{id}");

    public async Task<IReadOnlyList<ClientDto>> GetClientsAsync() =>
        await GetAsync<IReadOnlyList<ClientDto>>("clients") ?? [];

    public async Task AddClientAsync(ClientDto clientDto) => await PostAsync("clients", clientDto);

    public async Task<TuteeDto?> GetTuteeAsync(int id) => await GetAsync<TuteeDto?>($"tutees/{id}");
    
    public async Task AddTuteeAsync(TuteeDto tutee) => await PostAsync("tutees", tutee);
    
    public async Task<LessonDto?> GetLessonAsync(int id) => await GetAsync<LessonDto?>($"lessons/{id}");
    public async Task AddLessonAsync(LessonDto lesson) => await PostAsync("lessons", lesson);

    public async Task SaveTemporaryFile(TemporaryFileDto temporaryFile) =>
        await PostAsync("temporary-files", temporaryFile);

    public async Task SaveHomeworkAttachment(HomeworkAttachmentDto homeworkAttachmentDto) =>
        await PostAsync("homework-files", homeworkAttachmentDto);

    public async Task<IReadOnlyList<HomeworkAttachmentDto>> GetHomeworkAttachments(int lessonId) =>
        await GetAsync<IReadOnlyList<HomeworkAttachmentDto>>($"homework-files/{lessonId}") ?? [];

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