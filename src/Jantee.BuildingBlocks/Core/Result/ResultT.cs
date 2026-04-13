namespace Jantee.BuildingBlocks.Core.Result;

public class Result<T> : Result
{
    public T Data { get; }

    private Result(T data) : base(true, Error.None)
    {
        Data = data;
    }

    private Result(Error error) : base(false, error)
    {
        Data = default!;
    }

    public static Result<T> Success(T data) => new(data);
    public static new Result<T> Failure(Error error) => new(error);
}
