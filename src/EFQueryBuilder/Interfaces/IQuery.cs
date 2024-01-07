using EFQueryBuilder.Abstractions;
using EFQueryBuilder.Enums;

namespace EFQueryBuilder.Interfaces;

/// <summary>
/// Интерфейс, предоставляющий методы управления запросом.
/// </summary>
/// <typeparam name="TEntity"> Тип сущности.</typeparam>
public interface IQuery<TEntity>
{
    /// <summary>
    /// Проецировать последовательность сущностей в объект.
    /// </summary>
    /// <param name="projection"> Проекция.</param>
    /// <typeparam name="TObject"> Тип объекта проекции.</typeparam>
    /// <returns></returns>
    IProjectionQuery<TObject> ProjectTo<TObject>(Projection<TEntity, TObject> projection);
    
    /// <summary>
    /// Получить первый элемент последовательности.
    /// </summary>
    /// <param name="trackingType"> Режим отслеживания сущности.</param>
    /// <returns> Первый объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    TEntity? FirstOrDefault(TrackingType trackingType = TrackingType.WithTracking);
    
    /// <summary>
    /// Асинхронно получить первый элемент последовательности.
    /// </summary>
    /// <param name="trackingType"> Режим отслеживания сущности.</param>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    /// <returns> Первый объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    Task<TEntity?> FirstOrDefaultAsync(TrackingType trackingType = TrackingType.WithTracking, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить последний элемент последовательности.
    /// </summary>
    /// <param name="trackingType"> Режим отслеживания сущности.</param>
    /// <returns> Последний объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    TEntity? LastOrDefault(TrackingType trackingType = TrackingType.WithTracking);
    
    /// <summary>
    /// Асинхронно получить последний элемент последовательности.
    /// </summary>
    /// <param name="trackingType"> Режим отслеживания сущности.</param>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    /// <returns> Последний объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    Task<TEntity?> LastOrDefaultAsync(TrackingType trackingType = TrackingType.WithTracking, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить элементы последовательности.
    /// </summary>
    /// <param name="trackingType"> Режим отслеживания сущности.</param>
    /// <returns> Массив объектов последовательности.</returns>
    TEntity[] ToArray(TrackingType trackingType = TrackingType.WithTracking);
    
    /// <summary>
    /// Асинхронно получить элементы последовательности.
    /// </summary>
    /// <param name="trackingType"> Режим отслеживания сущности.</param>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    /// <returns> Массив объектов последовательности.</returns>
    Task<TEntity[]> ToArrayAsync(TrackingType trackingType = TrackingType.WithTracking, CancellationToken cancellationToken = default);
}