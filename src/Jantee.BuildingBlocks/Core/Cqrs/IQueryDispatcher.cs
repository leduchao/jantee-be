namespace Jantee.BuildingBlocks.Core.Cqrs;

public interface IQueryDispatcher
{
    Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken ct);
}
