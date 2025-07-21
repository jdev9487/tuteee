namespace JDev.Tuteee.ApiClient;

using DTOs;

public interface IApiClient
{
    Task<ClientDto?> GetClientAsync(int id);
    Task<IReadOnlyList<ClientDto>> GetClientsAsync();
    Task<TuteeDto?> GetTuteeAsync(int id);
}