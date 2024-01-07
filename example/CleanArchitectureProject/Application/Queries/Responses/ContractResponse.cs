using System.Linq.Expressions;
using Domain.Entities;
using EFQueryBuilder.Abstractions;

namespace Application.Queries.Responses;

public sealed class ContractResponse
{
    /// <summary>
    /// Идентификатор договора.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Наименование договора.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Описание договора.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// ФИО участника.
    /// </summary>
    public string? ParticipantFio { get; set; }

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreationDate { get; set; }
}

/// <summary>
/// Проекция <see cref="Contract"/> в <see cref="ContractResponse"/>
/// </summary>
internal sealed class ContractToContractResponse : Projection<Contract, ContractResponse>
{
    /// <inheritdoc />
    protected override Expression<Func<Contract, ContractResponse>> ProjectionExpression { get; }

    public ContractToContractResponse()
    {
        ProjectionExpression = contract =>
            new ContractResponse
            {
                Id = contract.Id,
                Description = contract.Description,
                Name = contract.Name,
                CreationDate = contract.CreationDate,
                ParticipantFio = contract.Participant.Fio
            };
    }
}