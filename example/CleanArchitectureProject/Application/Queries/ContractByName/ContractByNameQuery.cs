using Application.Queries.Responses;
using Domain.Interfaces;
using Domain.Interfaces.CQRS.Query;

namespace Application.Queries.ContractByName;

/// <summary>
/// Запрос на получение информации о договоре по имени.
/// </summary>
/// <param name="Name"> Имя договора.</param>
public record ContractByNameQuery(string Name) : IQuery<ContractResponse?>;

/// <summary>
/// Обработчик получения информации о договоре по имени.
/// </summary>
public class ContractByNameQueryHandler : IQueryHandler<ContractByNameQuery, ContractResponse?>
{
    private readonly IDataContext _dataContext;

    public ContractByNameQueryHandler(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    /// <inheritdoc />
    public Task<ContractResponse?> Handle(ContractByNameQuery request, CancellationToken cancellationToken = default)
    {
        return _dataContext.Get(new Specification.ContractByName(request.Name))
            .ProjectTo(new ContractToContractResponse())
            .FirstOrDefaultAsync(cancellationToken);
    }
}