namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface IRestApiClient
{
    Task<ClientDto?> GetClientAsync(int id, CancellationToken token);
    Task<IReadOnlyList<ClientDto>> GetClientsAsync(CancellationToken token);
    Task AddClientAsync(ClientDto clientDto, CancellationToken token);
    Task SaveTemporaryFileAsync(FileDto temporaryFile, CancellationToken token);
    Task SaveHomeworkAttachmentAsync(int lessonId, HomeworkAttachmentDto homeworkAttachmentDto, CancellationToken token);
    Task<IReadOnlyList<HomeworkAttachmentDto>> GetHomeworkAttachmentsAsync(int lessonId, CancellationToken token);
    Task<FileDto?> GetHomeworkAttachmentAsync(int homeworkAttachmentId, CancellationToken token);
    Task DeleteHomeworkAttachmentAsync(int homeworkAttachmentId, CancellationToken token);
    Task<IReadOnlyList<InvoiceDto>> GetInvoicesAsync(CancellationToken token);
    Task<LessonDto?> GetLessonAsync(int id, CancellationToken token);
    Task<IReadOnlyList<LessonDto>> GetLessonsAsync(CancellationToken token);
    Task AddLessonAsync(LessonDto lesson, CancellationToken token);
    Task UpdateLessonAsync(LessonDto lesson, CancellationToken token);
    Task AddRateAsync(int tuteeId, RateDto rateDto, CancellationToken token);
    Task<TuteeDto?> GetTuteeAsync(int id, CancellationToken token);
    Task AddTuteeAsync(TuteeDto tutee, CancellationToken token);
    Task AddTuteeRoleAsync(TuteeDto tutee, CancellationToken token);
}