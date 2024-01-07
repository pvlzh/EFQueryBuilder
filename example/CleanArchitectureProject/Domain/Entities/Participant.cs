using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
/// Участник договора.
/// </summary>
public class Participant : Entity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public uint Id { get; private set; }
    /// <summary>
    /// Ф.И.О.
    /// </summary>
    public string Fio { get; private set; }

    private Participant() { }
    
    public Participant(string fio)
    {
        Fio = fio;
    }
}