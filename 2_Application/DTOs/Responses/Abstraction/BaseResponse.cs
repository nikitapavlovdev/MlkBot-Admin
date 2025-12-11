namespace MlkAdmin._2_Application.DTOs.Responses.Abstraction;

public abstract class BaseResponse(bool isSuccess, string message = "", string error = "")
{
    public bool IsSuccess { get; set; } = isSuccess;
    public string Message { get; set; } = message;
    public string Error { get; set; } = error;

}
