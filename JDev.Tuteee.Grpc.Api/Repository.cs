namespace JDev.Tuteee.Grpc.Api;

using DAL;
using DAL.Entities;

public class Repository(Context context) : IRepository
{
    public async Task<Client?> GetClientAsync(int clientId, CancellationToken token) =>
        await context.Clients.FindAsync([clientId], cancellationToken: token);

    public async Task Add<TEntity>(TEntity entity, CancellationToken token) where TEntity : class =>
        await context.Set<TEntity>().AddAsync(entity, token);

    public async Task SaveChangesAsync(CancellationToken token) => await context.SaveChangesAsync(token);
}