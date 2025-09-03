namespace JDev.Tuteee.Rest.ApiClient.ApiClients;

using DTOs;
using System.Text.Json;

public class InvoiceRestApiClient(HttpClient client, JsonSerializerOptions options)
    : BaseRestApiClient(client, options), IInvoiceRestApiClient
{
    public async Task<IReadOnlyList<InvoiceDto>> GetListAsync(CancellationToken token) =>
        await GetAsync<IReadOnlyList<InvoiceDto>>(Endpoint.InvoiceBase, token) ?? [];
}