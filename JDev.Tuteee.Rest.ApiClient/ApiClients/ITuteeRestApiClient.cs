namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface ITuteeRestApiClient
{
    Task<TuteeDto?> GetAsync(int id, CancellationToken token);
    Task AddAsync(TuteeDto tutee, CancellationToken token);
}