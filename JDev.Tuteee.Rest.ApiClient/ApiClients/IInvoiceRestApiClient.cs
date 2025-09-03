namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;

public interface IInvoiceRestApiClient
{
    Task<IReadOnlyList<InvoiceDto>> GetListAsync(CancellationToken token);
}