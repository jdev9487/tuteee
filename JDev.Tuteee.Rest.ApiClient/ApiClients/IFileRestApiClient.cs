namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface IFileRestApiClient
{
    Task SaveTemporaryAsync(FileDto temporaryFile, CancellationToken token);
}