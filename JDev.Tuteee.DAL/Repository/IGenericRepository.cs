namespace JDev.Tuteee.DAL.Repository;

using Entities;

public interface IGenericRepository
{
    Task AddAsync<TEntity>(TEntity entity, CancellationToken token) where TEntity : BaseEntity;
    Task<TEntity?> FindAsync<TEntity>(int primaryKey, CancellationToken token) where TEntity : BaseEntity;
    Task<IReadOnlyList<TEntity>> GetListAsync<TEntity>(CancellationToken token) where TEntity : BaseEntity;
    Task<TEntity?> DeleteAsync<TEntity>(int primaryKey, CancellationToken token) where TEntity : BaseEntity;
    Task SaveChangesAsync(CancellationToken token);
}