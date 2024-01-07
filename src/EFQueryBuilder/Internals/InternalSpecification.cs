using System.Linq.Expressions;
using EFQueryBuilder.Abstractions;

namespace EFQueryBuilder.Internals;

/// <summary>
/// Внутренняя спецификация, используемая для преобразований.
/// </summary>
/// <typeparam name="TEntity"> Тип сущности.</typeparam>
internal sealed class InternalSpecification<TEntity> : Specification<TEntity>
{
    /// <inheritdoc />
    protected internal override Expression<Func<TEntity, bool>> Criteria { get; }

    /// <inheritdoc cref="InternalSpecification{TEntity}"/>
    public InternalSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
}