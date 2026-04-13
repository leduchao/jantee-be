using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Jantee.BuildingBlocks.Core.Cqrs.Implements;

public class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private static readonly ConcurrentDictionary<Type, Func<IServiceProvider, object, CancellationToken, Task<object>>> _cache = new();

    public async Task<TResult> Dispatch<TResult>(IQuery<TResult> query, CancellationToken ct)
    {
        var queryType = query.GetType();
        var handlerDelegate = _cache.GetOrAdd(queryType, CreateDelegate);

        var result = await handlerDelegate(_serviceProvider, query, ct);

        return (TResult)result!;
    }

    private static Func<IServiceProvider, object, CancellationToken, Task<object>> CreateDelegate(Type queryType)
    {
        // Find TResult from IQuery<TResult>
        var interfaceType = queryType
            .GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<>));

        var resultType = interfaceType.GetGenericArguments()[0];

        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);

        // Parameters
        var spParam = Expression.Parameter(typeof(IServiceProvider), "sp");
        var queryParam = Expression.Parameter(typeof(object), "query");
        var ctParam = Expression.Parameter(typeof(CancellationToken), "ct");

        // Resolve handler: sp.GetRequiredService(handlerType)
        var getServiceMethod = typeof(ServiceProviderServiceExtensions)
            .GetMethod(nameof(ServiceProviderServiceExtensions.GetRequiredService), [typeof(IServiceProvider), typeof(Type)])!;

        var handlerObj = Expression.Call(getServiceMethod, spParam, Expression.Constant(handlerType));

        // Cast handler
        var handlerCast = Expression.Convert(handlerObj, handlerType);

        // Cast query
        var queryCast = Expression.Convert(queryParam, queryType);

        // Call Handle
        var handleMethod = handlerType.GetMethod("Handle")!;
        var callHandle = Expression.Call(handlerCast, handleMethod, queryCast, ctParam);

        // Convert Task<TResult> → Task<object>
        var convertResult = Expression.Call(
            typeof(QueryDispatcher),
            nameof(ConvertTaskResult),
            [resultType],
            callHandle
        );

        var lambda = Expression.Lambda<Func<IServiceProvider, object, CancellationToken, Task<object>>>(
            convertResult,
            spParam,
            queryParam,
            ctParam
        );

        return lambda.Compile();
    }

    public static async Task<object?> ConvertTaskResult<TResult>(Task<TResult> task)
    {
        return await task;
    }
}
