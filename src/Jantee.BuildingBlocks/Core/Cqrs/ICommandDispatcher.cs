namespace Jantee.BuildingBlocks.Core.Cqrs;

public interface ICommandDispatcher
{
    Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken ct);
}
