using EFQueryBuilder.Abstractions;
using EFQueryBuilder.Enums;
using EFQueryBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFQueryBuilder.Queries;
/// <summary>
/// Внутренний класс, реализующий логику работы запроса с сущностью.
/// </summary>
/// <typeparam name="TEntity"> Тип сущности.</typeparam>
internal class Query<TEntity> : IQuery<TEntity> 
    where TEntity : class
{
    private readonly IQueryable<TEntity> _query;

    /// <inheritdoc cref="Query{TEntity}"/>
    public Query(IQueryable<TEntity> query)
    {
        _query = query;
    }

    /// <inheritdoc />
    public IProjectionQuery<TObject> ProjectTo<TObject>(Projection<TEntity, TObject> projection)
    {
        return QueryEvaluator.Evaluate(_query, projection);
    }

    /// <inheritdoc />
    public TEntity? FirstOrDefault(TrackingType trackingType = TrackingType.WithTracking)
    {
        var query = trackingType == TrackingType.AsNoTracking ? _query.AsNoTracking() : _query;
        return query.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<TEntity?> FirstOrDefaultAsync(TrackingType trackingType = TrackingType.WithTracking,
        CancellationToken cancellationToken = default)
    {
        var query = trackingType == TrackingType.AsNoTracking ? _query.AsNoTracking() : _query;
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public TEntity? LastOrDefault(TrackingType trackingType = TrackingType.WithTracking)
    {
        var query = trackingType == TrackingType.AsNoTracking ? _query.AsNoTracking() : _query;
        return query.LastOrDefault();
    }

    /// <inheritdoc />
    public async Task<TEntity?> LastOrDefaultAsync(TrackingType trackingType = TrackingType.WithTracking,
        CancellationToken cancellationToken = default)
    {
        var query = trackingType == TrackingType.AsNoTracking ? _query.AsNoTracking() : _query;
        return await query.LastOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public TEntity[] ToArray(TrackingType trackingType = TrackingType.WithTracking)
    {
        var query = trackingType == TrackingType.AsNoTracking ? _query.AsNoTracking() : _query;
        return query.ToArray();
    }

    /// <inheritdoc />
    public async Task<TEntity[]> ToArrayAsync(TrackingType trackingType = TrackingType.WithTracking, CancellationToken cancellationToken = default)
    {
        var query = trackingType == TrackingType.AsNoTracking ? _query.AsNoTracking() : _query;
        return await query.ToArrayAsync(cancellationToken);
    }
}