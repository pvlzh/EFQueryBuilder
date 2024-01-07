using EFQueryBuilder.Enums;

namespace EFQueryBuilder.Interfaces;

/// <summary>
/// Интерфейс, предоставляющий методы управления запросом, после проекции сущности в TObject.
/// Повторяет логику <see cref="IQuery{TEntity}"/> за исключением отсутствия параметра <see cref="TrackingType"/>,
/// т.к. объекты проекции по умолчанию не могут отслеживаться.
/// </summary>
/// <typeparam name="TObject"> Тип проекции.</typeparam>
public interface IProjectionQuery<TObject>
{
    /// <summary>
    /// Получить первый элемент последовательности.
    /// </summary>
    /// <returns> Первый объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    TObject? FirstOrDefault();
    
    /// <summary>
    /// Асинхронно получить первый элемент последовательности.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    /// <returns> Первый объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    Task<TObject?> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить последний элемент последовательности.
    /// </summary>
    /// <returns> Последний объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    TObject? LastOrDefault();
    
    /// <summary>
    /// Асинхронно получить последний элемент последовательности.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    /// <returns> Последний объект в последовательности если последовательность не пуста. null если последовательность пуста.</returns>
    Task<TObject?> LastOrDefaultAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить элементы последовательности.
    /// </summary>
    /// <returns> Массив объектов последовательности.</returns>
    TObject[] ToArray();
    
    /// <summary>
    /// Асинхронно получить элементы последовательности.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    /// <returns> Массив объектов последовательности.</returns>
    Task<TObject[]> ToArrayAsync(CancellationToken cancellationToken = default);
}