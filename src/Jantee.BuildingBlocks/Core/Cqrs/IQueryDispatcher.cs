namespace Jantee.BuildingBlocks.Core.Cqrs;

public interface IQueryDispatcher
{
    Task<TResult> Dispatch<TResult>(IQuery<TResult> query, CancellationToken ct);
}
