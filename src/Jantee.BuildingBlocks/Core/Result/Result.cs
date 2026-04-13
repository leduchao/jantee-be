namespace Jantee.BuildingBlocks.Core.Result;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && !error.Equals(Error.None))
        {
            throw new ArgumentException("Cannot have errors if success", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    
    public static Result Failure(Error error) => new(false, error);
}
