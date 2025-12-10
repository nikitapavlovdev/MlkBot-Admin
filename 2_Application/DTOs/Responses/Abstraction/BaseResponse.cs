namespace MlkAdmin._2_Application.DTOs.Responses.Abstraction;

public abstract class BaseResponse(bool isSuccess, string message = "", string error = "")
{
    public bool IsSuccess { get; protected set; } = isSuccess;
    public string Message { get; protected set; } = message;
    public string Error { get; protected set; } = error;

    
}
