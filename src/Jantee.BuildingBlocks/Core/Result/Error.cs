namespace Jantee.BuildingBlocks.Core.Result;

public record Error(string ErrorKey, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}
