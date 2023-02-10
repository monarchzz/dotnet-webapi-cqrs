using System.Linq.Expressions;
using Database;
using Database.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly ApplicationDbContext DbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = DbContext.Set<TEntity>();
    }

    public async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await _dbSet.AddAsync(entity, cancellationToken);
        await _saveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _saveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = _dbSet.Update(entity);
        await _saveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.UpdateRange(entities);
        await _saveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> Remove(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = _dbSet.Remove(entity);
        await _saveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(entities);
        await _saveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> Find(Guid id, CancellationToken cancellationToken)
    {
        // // Without base entity
        // var property = typeof(TEntity).GetProperty("Id");
        // if (property == null)
        //     throw new Exception("Entity does not have Id property");
        //
        // var lambdaArg = Expression.Parameter(typeof(TEntity));
        // var propertyAccess = Expression.MakeMemberAccess(lambdaArg, property);
        // var propertyEquals = Expression.Equal(propertyAccess, Expression.Constant(id, typeof(Guid)));
        //
        // var expression = Expression.Lambda<Func<TEntity, bool>>(propertyEquals, lambdaArg);
        //
        // return await _noTrackingQuery().FirstOrDefaultAsync(expression, cancellationToken);


        if (typeof(TEntity).IsSubclassOf(typeof(BaseEntity)) == false) return null;

        return await _noTrackingQuery()
            .FirstOrDefaultAsync(entity => ((BaseEntity)(object)entity).Id == id, cancellationToken);
    }

    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        if (typeof(TEntity).IsSubclassOf(typeof(BaseEntity)) == false) return false;

        return await _noTrackingQuery()
            .AnyAsync(entity => ((BaseEntity)(object)entity).Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> Finds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        // // Without base entity
        // var method = ids.GetType().GetMethod("Contains");
        // if (method == null)
        //     throw new Exception("Ids does not have Contains method");
        //
        // var property = typeof(TEntity).GetProperty("Id");
        // if (property == null)
        //     throw new Exception("Entity does not have Id property");
        //
        // var lambdaArg = Expression.Parameter(typeof(TEntity));
        // var propertyAccess = Expression.MakeMemberAccess(lambdaArg, property);
        //
        // var call = Expression.Call(Expression.Constant(ids), method, propertyAccess);
        //
        // var expression = Expression.Lambda<Func<TEntity, bool>>(call, lambdaArg);
        //
        // return await _noTrackingQuery().Where(expression).ToListAsync(cancellationToken);

        if (typeof(TEntity).IsSubclassOf(typeof(BaseEntity)) == false)
            return new List<TEntity>();

        return await _noTrackingQuery()
            .Where(entity => ids.Contains(((BaseEntity)(object)entity).Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> Any(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    )
    {
        return await _noTrackingQuery().AnyAsync(predicate, cancellationToken);
    }

    public Task<bool> All(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return _noTrackingQuery().AllAsync(predicate, cancellationToken);
    }

    public IQueryable<TEntity> SplitQuery(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null ? _splitQuery() : _splitQuery().Where(predicate);
    }

    public IQueryable<TEntity> NoTrackingQuery(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null ? _noTrackingQuery() : _noTrackingQuery().Where(predicate);
    }

    public IQueryable<TEntity> TrackingQuery(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null ? _dbSet : _dbSet.Where(predicate);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _saveChangesAsync(cancellationToken);
    }

    private IQueryable<TEntity> _noTrackingQuery()
    {
        return _dbSet.AsNoTracking();
    }

    private IQueryable<TEntity> _splitQuery()
    {
        return _dbSet.AsNoTracking().AsSplitQuery();
    }

    private async Task _saveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}