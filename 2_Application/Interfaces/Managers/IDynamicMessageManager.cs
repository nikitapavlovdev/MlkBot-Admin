using MlkAdmin._1_Domain.Enums;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Interfaces.Managers;

public interface IDynamicMessageManager
{
    Task<BaseResult> RefreshDynamicMessagesAsync();
    Task<BaseResult> GetDynamicMessageContentFromJson(DynamicMessages type);
}