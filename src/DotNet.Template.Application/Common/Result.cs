namespace DotNet.Template.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; private init; }
    public T? Value { get; private init; }
    public string? Error { get; private init; }
    public List<string> Errors { get; private init; } = new();

    private Result(bool isSuccess, T? value, string? error, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Errors = errors ?? new List<string>();
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
    public static Result<T> Failure(List<string> errors) => new(false, default, null, errors);
}

public class Result
{
    public bool IsSuccess { get; private init; }
    public string? Error { get; private init; }
    public List<string> Errors { get; private init; } = new();

    private Result(bool isSuccess, string? error, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors ?? new List<string>();
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
    public static Result Failure(List<string> errors) => new(false, null, errors);
}
