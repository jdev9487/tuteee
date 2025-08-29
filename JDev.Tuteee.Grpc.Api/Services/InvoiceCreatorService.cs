namespace JDev.Tuteee.Grpc.Api.Services;

using global::Grpc.Core;

public class InvoiceCreatorService(IRepository repository, ILogger<InvoiceCreatorService> logger)
    : InvoiceCreator.InvoiceCreatorBase
{
    public override async Task<BillClientResponse> BillClient(BillClientRequest request, ServerCallContext serverCallContext)
    {
        var clients = await repository.GetBillableClientsAsync(serverCallContext.CancellationToken);
        return new BillClientResponse
        {
            Success = true
        };
    }
}