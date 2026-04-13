namespace Jantee.BuildingBlocks.Core.Cqrs;

public interface ICommandHandler<in TCommand, TCommandResult>
{
    Task<TCommandResult> Handle(TCommand command, CancellationToken ct);
}
