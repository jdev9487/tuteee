namespace JDev.Tuteee.DAL.Repository;

using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;

public class GenericRepository(Context context) : IGenericRepository
{
    public async Task AddAsync<TEntity>(TEntity entity, CancellationToken token)
        where TEntity : BaseEntity =>
        await context.Set<TEntity>().AddAsync(entity, token);

    public async Task<TEntity?> FindAsync<TEntity>(int primaryKey, CancellationToken token)
        where TEntity : BaseEntity =>
        await context.FindAsync<TEntity>([primaryKey], token);
    
    public async Task<IReadOnlyList<TEntity>> GetListAsync<TEntity>(CancellationToken token)
        where TEntity : BaseEntity
    {
        return await context.Set<TEntity>()
            .ToListAsync(cancellationToken: token);
    }

    public async Task<TEntity?> DeleteAsync<TEntity>(int primaryKey, CancellationToken token) where TEntity : BaseEntity
    {
        var entity = await FindAsync<TEntity>(primaryKey, token);
        if (entity is not null)
            context.Set<TEntity>().Remove(entity);
        return entity;
    }

    public async Task SaveChangesAsync(CancellationToken token) =>
        await context.SaveChangesAsync(token);
}