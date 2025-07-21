namespace JDev.Tuteee.ApiClient;

using DTOs;

public interface IApiClient
{
    Task<ClientDto?> GetClientAsync(int id);
    Task<IReadOnlyList<ClientDto>> GetClientsAsync();
    Task AddClientAsync(ClientDto clientDto);
    Task<TuteeDto?> GetTuteeAsync(int id);
    Task AddTuteeAsync(TuteeDto tutee);
}