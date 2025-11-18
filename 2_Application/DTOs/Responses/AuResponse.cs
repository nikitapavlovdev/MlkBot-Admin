namespace MlkAdmin._2_Application.DTOs.Responses;

public class AuResponse
{
    public string Status { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty; 
    public bool IsSuccess { get; set; } = false;
}