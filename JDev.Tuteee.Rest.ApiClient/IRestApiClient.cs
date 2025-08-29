namespace JDev.Tuteee.Rest.ApiClient;

using DTOs;

public interface IRestApiClient
{
    Task<ClientDto?> GetClientAsync(int id);
    Task<IReadOnlyList<ClientDto>> GetClientsAsync();
    Task AddClientAsync(ClientDto clientDto);
    Task<TuteeDto?> GetTuteeAsync(int id);
    Task AddTuteeAsync(TuteeDto tutee);
    Task<LessonDto?> GetLessonAsync(int id);
    Task AddLessonAsync(LessonDto lesson);
    Task SaveTemporaryFile(TemporaryFileDto temporaryFile);
    Task SaveHomeworkAttachment(HomeworkAttachmentDto homeworkAttachmentDto);
    Task<IReadOnlyList<HomeworkAttachmentDto>> GetHomeworkAttachments(int lessonId);
    Task<IReadOnlyList<InvoiceDto>> GetInvoicesAsync();
}