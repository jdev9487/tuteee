namespace JDev.Tuteee.Grpc.Api;

using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

public class Repository(Context context) : IRepository
{
    public async Task<IReadOnlyList<Client>> GetBillableClientsAsync(CancellationToken token) =>
        await context.Clients.ToListAsync(cancellationToken: token);
}