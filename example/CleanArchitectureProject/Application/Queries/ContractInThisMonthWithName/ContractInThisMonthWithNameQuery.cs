using Application.Queries.Responses;
using Domain.Interfaces;
using Domain.Interfaces.CQRS.Query;

namespace Application.Queries.ContractInThisMonthWithName;

/// <summary>
/// Договора с определенным именем и созданные в текущем месяце.
/// </summary>
public record ContractInThisMonthWithNameQuery(string Name) : IQuery<ContractResponse?>;

public class ContractInThisMonthWithNameQueryHandler : IQueryHandler<ContractInThisMonthWithNameQuery, ContractResponse?>
{
    private readonly IDataContext _context;

    public ContractInThisMonthWithNameQueryHandler(IDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<ContractResponse?> Handle(ContractInThisMonthWithNameQuery request, CancellationToken cancellationToken = default)
    {
        return await _context.Get(new Specification.ContractInThisMonthWithName(request.Name))
            .ProjectTo(new ContractToContractResponse())
            .FirstOrDefaultAsync(cancellationToken);
    }
}