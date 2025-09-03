namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface ILessonRestApiClient
{
    Task<LessonDto?> GetAsync(int id, CancellationToken token);
    Task AddAsync(LessonDto lesson, CancellationToken token);
    Task EditAsync(LessonDto lesson, CancellationToken token);
}