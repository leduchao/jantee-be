namespace Jantee.BuildingBlocks.Core.Cqrs;

public interface ICommand<TResult>
{

}

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken ct);
}