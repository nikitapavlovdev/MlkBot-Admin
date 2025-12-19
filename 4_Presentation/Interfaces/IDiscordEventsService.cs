using MlkAdmin.Shared.Results;

namespace MlkAdmin._4_Presentation.Discord;

public interface IDiscordEventsService
{
    BaseResult SubscribeOnEvents();
    BaseResult UnsubscribeOnEvents();
}
