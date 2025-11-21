namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;
using Core.BaseHttpClient;

public class RestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseHttpClient(client, options), IRestApiClient
{
    public async Task<ClientDto?> GetClientAsync(int id, CancellationToken token) =>
        await GetAsync<ClientDto?>($"{Endpoint.ClientBase}/{id}", token);

    public async Task<IReadOnlyList<ClientDto>> GetClientsAsync(CancellationToken token) =>
        await GetAsync<IReadOnlyList<ClientDto>>(Endpoint.ClientBase, token) ?? [];

    public async Task AddClientAsync(ClientDto clientDto, CancellationToken token) =>
        await PostAsync(Endpoint.ClientBase, clientDto, token);
    
    public async Task SaveTemporaryFileAsync(FileDto temporaryFile, CancellationToken token) =>
        await PostAsync("temporary-files", temporaryFile, token);
    
    public async Task SaveLessonAttachmentAsync(int lessonId, LessonAttachmentDto lessonAttachmentDto, CancellationToken token) =>
        await PostAsync($"{Endpoint.LessonBase}/{lessonId}/lesson-attachments", lessonAttachmentDto, token);

    public async Task<IReadOnlyList<LessonAttachmentDto>> GetLessonAttachmentsAsync(int lessonId, CancellationToken token) =>
        await GetAsync<IReadOnlyList<LessonAttachmentDto>>(
            $"{Endpoint.LessonBase}/{lessonId}/lesson-attachments", token) ?? [];

    public async Task<FileDto?> GetLessonAttachmentAsync(int lessonAttachmentId, CancellationToken token) =>
        await GetAsync<FileDto>($"{Endpoint.LessonAttachmentsBase}/{lessonAttachmentId}", token);

    public async Task DeleteLessonAttachmentAsync(int lessonAttachmentId, CancellationToken token) =>
        await DeleteAsync($"{Endpoint.LessonAttachmentsBase}/{lessonAttachmentId}", token);
    
    public async Task<IReadOnlyList<InvoiceDto>> GetInvoicesAsync(CancellationToken token) =>
        await GetAsync<IReadOnlyList<InvoiceDto>>(Endpoint.InvoiceBase, token) ?? [];
    
    public async Task<LessonDto?> GetLessonAsync(int id, CancellationToken token) =>
        await GetAsync<LessonDto?>($"{Endpoint.LessonBase}/{id}", token);

    public async Task<IReadOnlyList<LessonDto>> GetLessonsAsync(
        DateOnly? start = null, DateOnly? end = null, CancellationToken token = default)
    {
        var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
        if (start is not null) queryString.Add("start", start.Value.ToString("yyyy-MM-dd"));
        if (end is not null) queryString.Add("end", end.Value.ToString("yyyy-MM-dd"));
        return await GetAsync<IReadOnlyList<LessonDto>>($"{Endpoint.LessonBase}?{queryString}", token) ?? [];
    }

    public async Task AddLessonAsync(LessonDto lesson, CancellationToken token) =>
        await PostAsync(Endpoint.LessonBase, lesson, token);

    public async Task UpdateLessonAsync(LessonDto lesson, CancellationToken token) =>
        await PatchAsync(Endpoint.LessonBase, lesson, token);

    public async Task DeleteLessonAsync(int lessonId, CancellationToken token) =>
        await DeleteAsync($"{Endpoint.LessonBase}/{lessonId}", token);

    public async Task<IReadOnlyList<ReservationSlotDto>> GetReservationSlotsAsync(CancellationToken token) =>
        await GetAsync<IReadOnlyList<ReservationSlotDto>>(Endpoint.ReservationSlotBase, token) ?? [];

    public async Task AddRateAsync(int tuteeId, RateDto rateDto, CancellationToken token) =>
        await PostAsync($"{Endpoint.TuteeBase}/{tuteeId}/{Endpoint.RateBase}", rateDto, token);
    
    public async Task AddReservationSlotAsync(int tuteeId, ReservationSlotDto reservationSlotDto, CancellationToken token) =>
        await PostAsync($"{Endpoint.TuteeBase}/{tuteeId}/{Endpoint.ReservationSlotBase}", reservationSlotDto, token);

    public async Task<TuteeDto?> GetTuteeAsync(int id, CancellationToken token) =>
        await GetAsync<TuteeDto?>($"{Endpoint.TuteeBase}/{id}", token);

    public async Task AddTuteeAsync(TuteeDto tutee, CancellationToken token) =>
        await PostAsync(Endpoint.TuteeBase, tutee, token);
    
    public async Task AddTuteeRoleAsync(TuteeDto tutee, CancellationToken token) =>
        await PostAsync($"{Endpoint.TuteeBase}/role", tutee, token);
}