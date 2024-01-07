using System.Linq.Expressions;
using Domain.Entities;
using EFQueryBuilder.Abstractions;

namespace Application.Specification;

/// <summary> Договор, созданный в текущем месяце. </summary>
public class ContractInThisMonth : Specification<Contract>
{
    /// <inheritdoc />
    protected override Expression<Func<Contract, bool>> Criteria { get; }
    
    /// <inheritdoc cref="ContractInThisMonth"/>
    public ContractInThisMonth()
    {
        Criteria = contract => contract.CreationDate.Year == DateTime.UtcNow.Year &&
                               contract.CreationDate.Month == DateTime.UtcNow.Month;
    }
}