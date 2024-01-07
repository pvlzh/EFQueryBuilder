using Application.Specification;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.CQRS.Command;

namespace Application.Commands.CreateContract;

/// <summary>
/// Команда на создание договора.
/// </summary>
/// <param name="ParticipantFio"> ФИО участника.</param>
/// <param name="Name"> Наименование договора.</param>
/// <param name="Description"> Описание договора.</param>
public record CreateContractCommand(string ParticipantFio, string Name, string Description) : ICommand<Guid>;

/// <summary>
/// Обработчик создания договора.
/// </summary>
public class CreateContractCommandHandler : ICommandHandler<CreateContractCommand, Guid>
{
    private readonly IDataContext _context;

    public CreateContractCommandHandler(IDataContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Guid> Handle(CreateContractCommand request, CancellationToken cancellationToken = default)
    {
        var participant = await _context.Get(new ParticipantByFio(request.ParticipantFio))
                              .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                          ?? new Participant(request.ParticipantFio);

        var contract = new Contract(request.Name, request.Description, participant);
        
        _context.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);
        
        return contract.Id;
    }
}