namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class RestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), IRestApiClient
{
    public async Task<ClientDto?> GetClientAsync(int id, CancellationToken token) =>
        await GetAsync<ClientDto?>($"{Endpoint.ClientBase}/{id}", token);

    public async Task<IReadOnlyList<ClientDto>> GetClientsAsync(CancellationToken token) =>
        await GetAsync<IReadOnlyList<ClientDto>>(Endpoint.ClientBase, token) ?? [];

    public async Task AddClientAsync(ClientDto clientDto, CancellationToken token) =>
        await PostAsync(Endpoint.ClientBase, clientDto, token);
    
    public async Task SaveTemporaryFileAsync(FileDto temporaryFile, CancellationToken token) =>
        await PostAsync("temporary-files", temporaryFile, token);
    
    public async Task SaveHomeworkAttachmentAsync(int lessonId, HomeworkAttachmentDto homeworkAttachmentDto, CancellationToken token) =>
        await PostAsync($"{Endpoint.LessonBase}/{lessonId}/homework-attachments", homeworkAttachmentDto, token);

    public async Task<IReadOnlyList<HomeworkAttachmentDto>> GetHomeworkAttachmentsAsync(int lessonId, CancellationToken token) =>
        await GetAsync<IReadOnlyList<HomeworkAttachmentDto>>(
            $"{Endpoint.LessonBase}/{lessonId}/homework-attachments", token) ?? [];

    public async Task<FileDto?> GetHomeworkAttachmentAsync(int homeworkAttachmentId, CancellationToken token) =>
        await GetAsync<FileDto>($"{Endpoint.HomeworkAttachmentsBase}/{homeworkAttachmentId}", token);

    public async Task DeleteHomeworkAttachmentAsync(int homeworkAttachmentId, CancellationToken token) =>
        await DeleteAsync($"{Endpoint.HomeworkAttachmentsBase}/{homeworkAttachmentId}", token);
    
    public async Task<IReadOnlyList<InvoiceDto>> GetInvoicesAsync(CancellationToken token) =>
        await GetAsync<IReadOnlyList<InvoiceDto>>(Endpoint.InvoiceBase, token) ?? [];
    
    public async Task<LessonDto?> GetLessonAsync(int id, CancellationToken token) =>
        await GetAsync<LessonDto?>($"{Endpoint.LessonBase}/{id}", token);

    public async Task AddLessonAsync(LessonDto lesson, CancellationToken token) =>
        await PostAsync(Endpoint.LessonBase, lesson, token);

    public async Task UpdateLessonAsync(LessonDto lesson, CancellationToken token) =>
        await PatchAsync(Endpoint.LessonBase, lesson, token);
    
    public async Task AddRateAsync(int tuteeId, RateDto rateDto, CancellationToken token) =>
        await PostAsync($"{Endpoint.TuteeBase}/{tuteeId}/{Endpoint.RateBase}", rateDto, token);
    
    public async Task<TuteeDto?> GetTuteeAsync(int id, CancellationToken token) =>
        await GetAsync<TuteeDto?>($"{Endpoint.TuteeBase}/{id}", token);

    public async Task AddTuteeAsync(TuteeDto tutee, CancellationToken token) =>
        await PostAsync(Endpoint.TuteeBase, tutee, token);
}