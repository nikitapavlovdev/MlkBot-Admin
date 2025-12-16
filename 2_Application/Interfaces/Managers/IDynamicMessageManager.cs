using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Interfaces;

public interface IDynamicMessageManager
{
    Task<BaseResult> RefreshDynamicMessagesAsync();
}