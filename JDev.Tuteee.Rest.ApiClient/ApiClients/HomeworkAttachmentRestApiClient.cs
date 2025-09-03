namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class HomeworkAttachmentRestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), IHomeworkAttachmentRestApiClient
{
    public async Task SaveAsync(int lessonId, HomeworkAttachmentDto homeworkAttachmentDto, CancellationToken token) =>
        await PostAsync($"{Endpoint.LessonBase}/{lessonId}/homework-attachments", homeworkAttachmentDto, token);

    public async Task<IReadOnlyList<HomeworkAttachmentDto>> GetListAsync(int lessonId, CancellationToken token) =>
        await GetAsync<IReadOnlyList<HomeworkAttachmentDto>>(
            $"{Endpoint.LessonBase}/{lessonId}/homework-attachments", token) ?? [];

    public async Task<FileDto?> GetAsync(int homeworkAttachmentId, CancellationToken token) =>
        await GetAsync<FileDto>($"{Endpoint.HomeworkAttachmentsBase}/{homeworkAttachmentId}", token);

    public async Task DeleteAsync(int homeworkAttachmentId, CancellationToken token) =>
        await DeleteAsync($"{Endpoint.HomeworkAttachmentsBase}/{homeworkAttachmentId}", token);
}