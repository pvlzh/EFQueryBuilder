using EFQueryBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFQueryBuilder.Queries;

/// <summary>
/// Внутренний класс, реализующий логику работы запроса с проекцией.
/// </summary>
/// <typeparam name="TObject"> Тип проекции.</typeparam>
internal sealed class ProjectionQuery<TObject> : IProjectionQuery<TObject>
{
    private readonly IQueryable<TObject> _query;

    /// <inheritdoc cref="ProjectionQuery{TObject}"/>
    public ProjectionQuery(IQueryable<TObject> query)
    {
        _query = query;
    }

    /// <inheritdoc />
    public TObject? FirstOrDefault()
    {
        return _query.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<TObject?> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        return await _query.FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public TObject? LastOrDefault()
    {
        return _query.LastOrDefault();
    }

    /// <inheritdoc />
    public async Task<TObject?> LastOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        return await _query.LastOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public TObject[] ToArray()
    {
        return _query.ToArray();
    }

    /// <inheritdoc />
    public Task<TObject[]> ToArrayAsync(CancellationToken cancellationToken = default)
    {
        return _query.ToArrayAsync(cancellationToken);
    }
}