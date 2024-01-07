using System.Linq.Expressions;
using EFQueryBuilder.Internals;

namespace EFQueryBuilder.Abstractions;

/// <summary>
/// Абстрактный класс спецификации для сущности.
/// </summary>
/// <typeparam name="TEntity"> Тип сущности.</typeparam>
public abstract class Specification<TEntity>
{
    /// <summary>
    /// Выражение отбора последовательности.
    /// </summary>
    protected internal abstract Expression<Func<TEntity, bool>> Criteria { get; }

    
    public static implicit operator Expression<Func<TEntity, bool>>(
        Specification<TEntity> specification) => specification.Criteria;
    
    public static implicit operator Specification<TEntity>(
        Expression<Func<TEntity, bool>> expression) => new InternalSpecification<TEntity>(expression);


    public static Expression<Func<TEntity, bool>> operator &(
        Specification<TEntity> first,
        Specification<TEntity> second)
    {
        var entity = Expression.Parameter(typeof(TEntity));
        var andExpression = Expression.AndAlso(
            Expression.Invoke(first, entity), 
            Expression.Invoke(second, entity));
        return Expression.Lambda<Func<TEntity, bool>>(andExpression, entity);
    }


    public static Expression<Func<TEntity, bool>> operator |(
        Specification<TEntity> first,
        Specification<TEntity> second)
    {
        var entity = Expression.Parameter(typeof(TEntity));
        var orExpression = Expression.OrElse(
            Expression.Invoke(first, entity), 
            Expression.Invoke(second, entity));
        return Expression.Lambda<Func<TEntity, bool>>(orExpression, entity);
    }
}