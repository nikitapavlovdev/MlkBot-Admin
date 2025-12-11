using MlkAdmin._2_Application.DTOs.Responses.Abstraction;

namespace MlkAdmin._2_Application.DTOs.Responses.Specialized;

public class PersonalVChannelNameResponse(bool isSuccess = false, string message = "", string error = "") : BaseResponse(isSuccess, message, error)
{
    public string PersonalVoiceChannelName { get; set; } = string.Empty;
}
