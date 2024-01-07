using EFQueryBuilder.Abstractions;
using EFQueryBuilder.Interfaces;
using EFQueryBuilder.Queries;

namespace EFQueryBuilder;

/// <summary>
/// Класс предоставляющий открытые методы по работе c <see cref="Specification{TEntity}"/> и <see cref="Projection{TEntity,TObject}"/>.
/// </summary>
public static class QueryEvaluator
{
    /// <summary>
    /// Применить спецификацию к источнику данных.
    /// </summary>
    /// <param name="query"> Источник данных.</param>
    /// <param name="specification"> Спецификация.</param>
    /// <typeparam name="TEntity"> Тип сущности.</typeparam>
    /// <returns> Интерфейс с методами управления запросом.</returns>
    public static IQuery<TEntity> Evaluate<TEntity>(IQueryable<TEntity> query, Specification<TEntity> specification)
        where TEntity : class 
    {
        return new Query<TEntity>(query.Where(specification.Criteria));
    }
    
    /// <summary>
    /// Применить проекцию к источнику данных.
    /// </summary>
    /// <param name="query"> Источник данных.</param>
    /// <param name="projection"> Проекция.</param>
    /// <typeparam name="TEntity"> Тип сущности.</typeparam>
    /// <typeparam name="TObject"> Тип объекта.</typeparam>
    /// <returns> Интерфейс с методами управления запросом.</returns>
    public static IProjectionQuery<TObject> Evaluate<TEntity, TObject>(IQueryable<TEntity> query, Projection<TEntity, TObject> projection)
        where TEntity : class 
    {
        return new ProjectionQuery<TObject>(query.Select(projection.ProjectionExpression));
    }
}