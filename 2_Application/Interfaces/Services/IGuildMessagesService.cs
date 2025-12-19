using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Interfaces.Services;

public interface IGuildMessagesService
{
    Task SendMessageInChannelAsync(ulong channelId, GuildMessageDto content);
}
