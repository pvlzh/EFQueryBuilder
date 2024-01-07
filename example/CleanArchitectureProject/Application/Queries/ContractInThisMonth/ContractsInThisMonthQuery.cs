using Application.Queries.Responses;
using Domain.Interfaces;
using Domain.Interfaces.CQRS.Query;

namespace Application.Queries.ContractInThisMonth;

/// <summary>
/// Договора созданные в текущем месяце.
/// </summary>
public record ContractsInThisMonthQuery() : IQuery<IReadOnlyCollection<ContractResponse>>;

public class ContractsInThisMonthQueryHandler : IQueryHandler<ContractsInThisMonthQuery, IReadOnlyCollection<ContractResponse>>
{
    private readonly IDataContext _dataContext;

    public ContractsInThisMonthQueryHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ContractResponse>> Handle(
        ContractsInThisMonthQuery request, CancellationToken cancellationToken = default)
    {
        return await _dataContext.Get(new Specification.ContractInThisMonth())
            .ProjectTo(new ContractToContractResponse())
            .ToArrayAsync(cancellationToken);
    }
}