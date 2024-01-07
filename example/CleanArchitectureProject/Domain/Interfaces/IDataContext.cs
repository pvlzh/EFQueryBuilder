using Domain.Entities.Base;
using Domain.Interfaces.Entities;
using EFQueryBuilder.Abstractions;
using EFQueryBuilder.Interfaces;

namespace Domain.Interfaces;

/// <summary>
/// Провайдер работы с контекстом.
/// </summary>
public interface IDataContext
{
    /// <summary>
    /// Получить сущности по спецификации.
    /// </summary>
    /// <param name="specification"> Спецификация.</param>
    /// <typeparam name="TEntity"> Тип сущности.</typeparam>
    public IQuery<TEntity> Get<TEntity>(Specification<TEntity> specification)
        where TEntity : Entity;
    
    /// <summary>
    /// Получить проекцию сущностей.
    /// </summary>
    /// <param name="projection"> Проекция.</param>
    /// <typeparam name="TEntity"> Тип сущности.</typeparam>
    /// <typeparam name="TObject"> Тип объектов.</typeparam>
    public IProjectionQuery<TObject> Get<TEntity, TObject>(Projection<TEntity, TObject> projection)
        where TEntity : Entity;
    
    /// <summary>
    /// Добавить сущность.
    /// </summary>
    /// <param name="entity"> Сущность.</param>
    /// <typeparam name="TEntity"> Тип сущности.</typeparam>
    public void Add<TEntity>(TEntity entity)
        where TEntity : IAdditionalEntity;
    
    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="entity"> Сущность.</param>
    /// <typeparam name="TEntity"> Тип сущности.</typeparam>
    public void Remove<TEntity>(TEntity entity)
        where TEntity : IRemovableEntity;
    
    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public void SaveChanges();
    
    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    /// <param name="token">токен отмены операции.</param>
    public Task SaveChangesAsync(CancellationToken token = default);
}