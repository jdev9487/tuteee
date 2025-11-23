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
        var today = DateTime.Today;
        var client = await repository.FindAsync<ClientRole>(request.ClientId, serverCallContext.CancellationToken);
        if (client is null) return new BillClientResponse { Success = false };
        var invoices = client.TuteeRoles
            .SelectMany(t => t.Lessons.Where(l => l.Invoice is null))
            .Where(l => l.Date < new DateOnly(today.Year, today.Month, 1))
            .GroupBy(l => new Month(l.Date.Year, l.Date.Month), (month, lessons) => new Invoice
            {
                ClientRole = client,
                From = month.Start,
                To = month.End,
                Lessons = lessons.ToList()
            });
        await repository.AddRangeAsync(invoices, serverCallContext.CancellationToken);
        await repository.SaveChangesAsync(serverCallContext.CancellationToken);
        return new BillClientResponse
        {
            Success = true
        };
    }
}

internal readonly struct Month(int year, int month)
{
    internal DateOnly Start { get; init; } = new(year, month, 1);
    internal DateOnly End { get; init; } = new DateOnly(year, month, 1).AddMonths(1).AddDays(-1);
}