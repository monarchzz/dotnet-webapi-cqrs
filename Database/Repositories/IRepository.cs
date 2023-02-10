using System.Linq.Expressions;
using Domain.Entities;

namespace Database.Repositories;

public interface IRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Add entity to the context
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add the range of entities to the context
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update entity in the context
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update the range of entities in the context
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove entity from the context
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> Remove(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove the range of entities from the context
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>TEntity</returns>
    Task<TEntity?> Find(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    ///Find entity by list of ids 
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> Finds(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if entity exists
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Any entity satisfies the condition
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> Any(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// All entities satisfies the condition
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> All(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queryable with no tracking and split query
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IQueryable<TEntity> SplitQuery(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Queryable with no tracking
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IQueryable<TEntity> NoTrackingQuery(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Queryable with tracking
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IQueryable<TEntity> TrackingQuery(Expression<Func<TEntity, bool>>? predicate = null);

    /// <summary>
    /// Save changes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}