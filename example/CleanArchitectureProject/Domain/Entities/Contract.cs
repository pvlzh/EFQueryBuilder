using Domain.Entities.Base;
using Domain.Interfaces.Entities;

namespace Domain.Entities;

/// <summary>
/// Договор.
/// </summary>
public class Contract : Entity, IAdditionalEntity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreationDate { get; private set; }

    /// <summary>
    /// Участник договора.
    /// </summary>
    public Participant Participant { get; private set; }

    /// <inheritdoc cref="Contract"/>
    private Contract() { }

    /// <inheritdoc cref="Contract"/>
    /// <param name="name"> Наименование договора.</param>
    /// <param name="description"> Описание договора.</param>
    /// <param name="participant"> Участник.</param>
    public Contract(string name, string description, Participant participant)
    {
        Participant = participant;
        Name = name;
        Description = description;
        CreationDate = DateTime.UtcNow;
    }
}