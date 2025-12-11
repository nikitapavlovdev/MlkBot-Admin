namespace MlkAdmin._2_Application.DTOs.Responses.Abstraction;

public readonly struct BaseResult<T>(bool isSuccess, T value, Error? error)
{
    public bool IsSuccess { get; } = isSuccess;
    public T Value { get; } = value;
    public Error? Error { get; } = error;


    public static BaseResult<T> Success(T value) => new(true, value, null);
    public static BaseResult<T> Fail(Error error) => new(false, default, error);
}

public record Error(string Code, string Message, Exception Exception);
