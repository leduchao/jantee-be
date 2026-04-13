using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Jantee.BuildingBlocks.Core.Cqrs.Implements;

public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private static readonly ConcurrentDictionary<Type, Func<IServiceProvider, object, CancellationToken, Task<object>>> _cache = new();

    public async Task<TResult> Dispatch<TResult>(ICommand<TResult> command, CancellationToken ct)
    {
        var commandType = command.GetType();
        var handlerDelegate = _cache.GetOrAdd(commandType, CreateDelegate);

        var result = await handlerDelegate(_serviceProvider, command, ct);

        return (TResult)result;
    }

    private static Func<IServiceProvider, object, CancellationToken, Task<object>> CreateDelegate(Type commandType)
    {
        // Find TResult from ICommand<TResult>
        var interfaceType = commandType
            .GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>));

        var resultType = interfaceType.GetGenericArguments()[0];

        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, resultType);

        // Parameters
        var spParam = Expression.Parameter(typeof(IServiceProvider), "sp");
        var commandParam = Expression.Parameter(typeof(object), "command");
        var ctParam = Expression.Parameter(typeof(CancellationToken), "ct");

        // Resolve handler: sp.GetRequiredService(handlerType)
        var getServiceMethod = typeof(ServiceProviderServiceExtensions)
            .GetMethod(nameof(ServiceProviderServiceExtensions.GetRequiredService), [typeof(IServiceProvider), typeof(Type)])!;

        var handlerObj = Expression.Call(getServiceMethod, spParam, Expression.Constant(handlerType));

        // Cast handler
        var handlerCast = Expression.Convert(handlerObj, handlerType);

        // Cast command
        var commandCast = Expression.Convert(commandParam, commandType);

        // Call Handle
        var handleMethod = handlerType.GetMethod("Handle")!;
        var callHandle = Expression.Call(handlerCast, handleMethod, commandCast, ctParam);

        // Convert Task<TResult> → Task<object>
        var convertResult = Expression.Call(
            typeof(CommandDispatcher),
            nameof(ConvertTaskResult),
            [resultType],
            callHandle
        );

        var lambda = Expression.Lambda<Func<IServiceProvider, object, CancellationToken, Task<object>>>(
            convertResult,
            spParam,
            commandParam,
            ctParam
        );

        return lambda.Compile();
    }

    public static async Task<object?> ConvertTaskResult<TResult>(Task<TResult> task)
    {
        return await task;
    }
}
