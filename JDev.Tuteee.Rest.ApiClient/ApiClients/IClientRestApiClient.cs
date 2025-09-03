namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface IClientRestApiClient
{
    Task<ClientDto?> GetAsync(int id, CancellationToken token);
    Task<IReadOnlyList<ClientDto>> GetListAsync(CancellationToken token);
    Task AddAsync(ClientDto clientDto, CancellationToken token);
}