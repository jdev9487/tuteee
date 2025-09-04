namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface IRateRestApiClient
{
    Task AddAsync(int tuteeId, RateDto rateDto, CancellationToken token);
}