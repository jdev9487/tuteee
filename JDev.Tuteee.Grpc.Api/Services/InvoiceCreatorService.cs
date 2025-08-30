namespace JDev.Tuteee.Grpc.Api.Services;

using Protos;
using DAL.Entities;
using global::Grpc.Core;

public class InvoiceCreatorService(IRepository repository, ILogger<InvoiceCreatorService> logger)
    :  InvoiceCreator.InvoiceCreatorBase
{
    public override async Task<BillClientResponse> BillClient(BillClientRequest request, ServerCallContext serverCallContext)
    {
        var client = await repository.GetClientAsync(request.ClientId, serverCallContext.CancellationToken);
        if (client is null) return new BillClientResponse { Success = false };
        var invoice = new Invoice
        {
            Client = client,
            Paid = false,
            Lessons = client.Tutees.SelectMany(t => t.Lessons.Where(l => l.Invoice is null)).ToList()
        };
        await repository.Add(invoice, serverCallContext.CancellationToken);
        await repository.SaveChangesAsync(serverCallContext.CancellationToken);
        return new BillClientResponse
        {
            Success = true
        };
    }
}