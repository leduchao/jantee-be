namespace Jantee.BuildingBlocks.Core.Cqrs;

public interface ICommandDispatcher
{
    Task<TResult> Dispatch<TResult>(ICommand<TResult> command, CancellationToken ct);
}
