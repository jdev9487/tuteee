namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface IHomeworkAttachmentRestApiClient
{
    Task SaveAsync(int lessonId, HomeworkAttachmentDto homeworkAttachmentDto, CancellationToken token);
    Task<IReadOnlyList<HomeworkAttachmentDto>> GetListAsync(int lessonId, CancellationToken token);
    Task<FileDto?> GetAsync(int homeworkAttachmentId, CancellationToken token);
    Task DeleteAsync(int homeworkAttachmentId, CancellationToken token);
}