namespace Domain.Interfaces.CQRS.Command;

public interface ICommandHandler<in TCommand, TResult> 
    where TCommand : ICommand<TResult>
{
    Task<TResult> Handle(TCommand request, CancellationToken cancellationToken = default);
}