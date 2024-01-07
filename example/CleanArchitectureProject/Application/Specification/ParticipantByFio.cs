using System.Linq.Expressions;
using Domain.Entities;
using EFQueryBuilder.Abstractions;

namespace Application.Specification;

/// <summary>
/// Участник договора по ФИО.
/// </summary>
public class ParticipantByFio : Specification<Participant>
{
    /// <inheritdoc />
    protected override Expression<Func<Participant, bool>> Criteria { get; }

    /// <inheritdoc cref="ParticipantByFio"/>
    public ParticipantByFio(string participantFio)
    {
        Criteria = participant => participant.Fio == participantFio;
    }
}