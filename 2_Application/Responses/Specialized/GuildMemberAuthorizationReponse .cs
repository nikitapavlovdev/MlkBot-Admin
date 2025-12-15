using MlkAdmin._2_Application.DTOs.Responses.Abstraction;

namespace MlkAdmin._2_Application.DTOs.Responses.Specialized;

public class GuildMemberAuthorizationReponse(bool isSuccess = false, string message = "", string error ="") : BaseResponse(isSuccess, message, error)
{
}
