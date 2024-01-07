using System.Linq.Expressions;

namespace EFQueryBuilder.Abstractions;

/// <summary>
/// Абстрактный класс проекции сущности на объект.
/// </summary>
/// <typeparam name="TEntity"> Тип сущности.</typeparam>
/// <typeparam name="TObject"> Тип объекта.</typeparam>
public abstract class Projection<TEntity, TObject>
{
    /// <summary>
    /// Выражение проекции.
    /// </summary>
    protected internal abstract Expression<Func<TEntity, TObject>> ProjectionExpression { get; }
}