using System.Linq.Expressions;
using Domain.Entities;
using EFQueryBuilder.Abstractions;

namespace Application.Specification;

/// <summary>
/// Договор с определенным именем.
/// </summary>
public class ContractByName : Specification<Contract>
{
    /// <inheritdoc />
    protected override Expression<Func<Contract, bool>> Criteria { get; }
    
    /// <inheritdoc cref="ContractByName"/>
    public ContractByName(string contractName)
    {
        Criteria = contract => contract.Name == contractName;
    }
}