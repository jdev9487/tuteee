namespace JDev.Tuteee.Grpc.Api;

using DAL.Entities;

public interface IRepository
{
    Task<Client?> GetClientAsync(int clientId, CancellationToken token);

    Task Add<TEntity>(TEntity entity, CancellationToken token) where TEntity : class;
    Task SaveChangesAsync(CancellationToken token);
}