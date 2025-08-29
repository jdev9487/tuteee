namespace JDev.Tuteee.Rest.ApiClient;

using DTOs;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RestApiClient(HttpClient client) : IRestApiClient
{
    public async Task<ClientDto?> GetClientAsync(int id) => await GetAsync<ClientDto?>($"{Endpoint.ClientBase}/{id}");

    public async Task<IReadOnlyList<ClientDto>> GetClientsAsync() =>
        await GetAsync<IReadOnlyList<ClientDto>>(Endpoint.ClientBase) ?? [];

    public async Task AddClientAsync(ClientDto clientDto) => await PostAsync(Endpoint.ClientBase, clientDto);

    public async Task<TuteeDto?> GetTuteeAsync(int id) => await GetAsync<TuteeDto?>($"{Endpoint.TuteeBase}/{id}");
    
    public async Task AddTuteeAsync(TuteeDto tutee) => await PostAsync(Endpoint.TuteeBase, tutee);
    
    public async Task<LessonDto?> GetLessonAsync(int id) => await GetAsync<LessonDto?>($"{Endpoint.LessonBase}/{id}");
    public async Task AddLessonAsync(LessonDto lesson) => await PostAsync(Endpoint.LessonBase, lesson);

    public async Task SaveTemporaryFile(TemporaryFileDto temporaryFile) =>
        await PostAsync("temporary-files", temporaryFile);

    public async Task SaveHomeworkAttachment(HomeworkAttachmentDto homeworkAttachmentDto) =>
        await PostAsync("homework-files", homeworkAttachmentDto);

    public async Task<IReadOnlyList<HomeworkAttachmentDto>> GetHomeworkAttachments(int lessonId) =>
        await GetAsync<IReadOnlyList<HomeworkAttachmentDto>>($"homework-files/{lessonId}") ?? [];

    public async Task<IReadOnlyList<InvoiceDto>> GetInvoicesAsync() =>
        await GetAsync<IReadOnlyList<InvoiceDto>>(Endpoint.InvoiceBase) ?? [];

    private async Task<TResponseObject?> GetAsync<TResponseObject>(string uri)
    {
        var response = await client.GetAsync(uri);
        var json = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        return JsonSerializer.Deserialize<TResponseObject>(json, options) ?? default;
    }

    private async Task PostAsync<TRequest>(string uri, TRequest request)
    {
        using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        await client.PostAsync(uri, jsonContent);
    }
}