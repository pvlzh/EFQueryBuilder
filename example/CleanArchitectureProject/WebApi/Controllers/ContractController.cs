using Application.Commands.CreateContract;
using Application.Queries.ContractByName;
using Application.Queries.ContractInThisMonth;
using Application.Queries.ContractInThisMonthWithName;
using Application.Queries.Responses;
using Domain.Interfaces.CQRS.Command;
using Domain.Interfaces.CQRS.Query;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContractController : Controller
{
    /// <summary>
    /// Найти договор по наименованию.
    /// </summary>
    /// <param name="query"> Данные запроса.</param>
    /// <param name="handler"> Обработчик.</param>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    [HttpGet("by-name")]
    public async Task<ActionResult<ContractResponse>> ContractByName(
        [FromQuery] ContractByNameQuery query,
        [FromServices] IQueryHandler<ContractByNameQuery, ContractResponse?> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(query, cancellationToken);
        return result != null ? new OkObjectResult(result)
            : new NotFoundObjectResult($"Договоров с именем {query.Name} не найдено.");
    }

    /// <summary>
    /// Найти договора в этом месяце.
    /// </summary>
    /// <param name="handler"> Обработчик.</param>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    [HttpGet("this-month")]
    public async Task<ActionResult<IReadOnlyCollection<ContractResponse>>> ContractsInThisMonth(
        [FromServices] IQueryHandler<ContractsInThisMonthQuery, IReadOnlyCollection<ContractResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new ContractsInThisMonthQuery(), cancellationToken);
        return new OkObjectResult(result);
    }

    /// <summary>
    /// Найти договор по наименованию в текущем месяце.
    /// </summary>
    /// <param name="query"> Данные запроса.</param>
    /// <param name="handler"> Обработчик.</param>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    [HttpGet("by-name-this-month")]
    public async Task<ActionResult<ContractResponse>> ContractsInThisMonthWithName(
        [FromQuery] ContractInThisMonthWithNameQuery query,
        [FromServices] IQueryHandler<ContractInThisMonthWithNameQuery, ContractResponse?> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(query, cancellationToken);
        return result != null ? new OkObjectResult(result)
            : new NotFoundObjectResult($"Договоров в этом месяце с именем {query.Name} не найдено.");
    }


    /// <summary>
    /// Создать договор.
    /// </summary>
    /// <param name="command"> Данные запроса.</param>
    /// <param name="handler"> Обработчик.</param>
    /// <param name="cancellationToken"> Токен отмены операции.</param>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateContract(
        [FromQuery] CreateContractCommand command,
        [FromServices] ICommandHandler<CreateContractCommand, Guid> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(command, cancellationToken);
        
        return result != default ? new OkObjectResult(result) 
            : new BadRequestObjectResult("Ошибка при создании договора.");
    }
}