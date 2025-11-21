namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface IRestApiClient
{
    Task<ClientDto?> GetClientAsync(int id, CancellationToken token);
    Task<IReadOnlyList<ClientDto>> GetClientsAsync(CancellationToken token);
    Task AddClientAsync(ClientDto clientDto, CancellationToken token);
    Task SaveTemporaryFileAsync(FileDto temporaryFile, CancellationToken token);
    Task SaveLessonAttachmentAsync(int lessonId, LessonAttachmentDto lessonAttachmentDto, CancellationToken token);
    Task<IReadOnlyList<LessonAttachmentDto>> GetLessonAttachmentsAsync(int lessonId, CancellationToken token);
    Task<FileDto?> GetLessonAttachmentAsync(int lessonAttachmentId, CancellationToken token);
    Task DeleteLessonAttachmentAsync(int lessonAttachmentId, CancellationToken token);
    Task<IReadOnlyList<InvoiceDto>> GetInvoicesAsync(CancellationToken token);
    Task<LessonDto?> GetLessonAsync(int id, CancellationToken token);
    Task<IReadOnlyList<LessonDto>> GetLessonsAsync(DateOnly? start = null, DateOnly? end = null, CancellationToken token = default);
    Task AddLessonAsync(LessonDto lesson, CancellationToken token);
    Task UpdateLessonAsync(LessonDto lesson, CancellationToken token);
    Task DeleteLessonAsync(int lessonId, CancellationToken token);
    Task<IReadOnlyList<ReservationSlotDto>> GetReservationSlotsAsync(CancellationToken token);
    Task AddRateAsync(int tuteeId, RateDto rateDto, CancellationToken token);
    Task AddReservationSlotAsync(int tuteeId, ReservationSlotDto reservationSlotDto, CancellationToken token);
    Task<TuteeDto?> GetTuteeAsync(int id, CancellationToken token);
    Task AddTuteeAsync(TuteeDto tutee, CancellationToken token);
    Task AddTuteeRoleAsync(TuteeDto tutee, CancellationToken token);
}