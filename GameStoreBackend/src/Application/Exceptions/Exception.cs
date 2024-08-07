namespace Application.Exceptions;

public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public bool IsFailure => !IsSuccess;
    public int StatusCode { get; }

    protected Result(bool isSuccess, string? error, int statusCode)
    {
        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result Success()
    {
        return new Result(true, null, 200);
    }

    public static Result Failure(string error, int statusCode = 400) => new(false, error, statusCode);

    public static Result NotFound(string error = "Not Found") => new(false, error, 404);
}

public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(T? value, bool isSuccess, string? error, int statusCode)
        : base(isSuccess, error, statusCode)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value, true, null, 200);

    public static new Result<T> Failure(string error, int statusCode = 400) => new(default, false, error, statusCode);

    public static new Result<T> NotFound(string error = "Not Found") => new(default, false, error, 404);
}

