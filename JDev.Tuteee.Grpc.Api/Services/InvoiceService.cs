namespace JDev.Tuteee.Grpc.Api.Services;

using Protos;
using DAL.Entities;
using global::Grpc.Core;
using Core.EfCore.Repository;
using Invoice = DAL.Entities.Invoice;

public class InvoiceService(IGenericRepository repository) : Protos.Invoice.InvoiceBase
{
    public override async Task<BillClientResponse> BillClient(BillClientRequest request, ServerCallContext serverCallContext)
    {
        var client = await repository.FindAsync<ClientRole>(request.ClientId, serverCallContext.CancellationToken);
        if (client is null) return new BillClientResponse { Success = false };
        var invoice = new Invoice
        {
            ClientRole = client,
            Paid = false,
            Lessons = client.TuteeRoles.SelectMany(t => t.Lessons.Where(l => l.Invoice is null)).ToList()
        };
        await repository.AddAsync(invoice, serverCallContext.CancellationToken);
        await repository.SaveChangesAsync(serverCallContext.CancellationToken);
        return new BillClientResponse
        {
            Success = true
        };
    }
}