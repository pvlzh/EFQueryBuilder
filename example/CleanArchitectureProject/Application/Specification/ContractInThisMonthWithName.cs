using System.Linq.Expressions;
using Domain.Entities;
using EFQueryBuilder.Abstractions;

namespace Application.Specification;

/// <summary>
/// Договор с определенным именем, созданный в этом месяце.
/// </summary>
public class ContractInThisMonthWithName : Specification<Contract>
{
    /// <inheritdoc />
    protected override Expression<Func<Contract, bool>> Criteria { get; }

    /// <inheritdoc cref="ContractInThisMonthWithName"/>
    public ContractInThisMonthWithName(string contractName)
    {
        Criteria = new ContractByName(contractName) & new ContractInThisMonth();
    }
}