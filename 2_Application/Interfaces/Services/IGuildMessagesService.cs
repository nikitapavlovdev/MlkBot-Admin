using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._1_Domain.Interfaces.Messages;

public interface IGuildMessagesService
{
    Task SendMessageInChannelAsync(ulong channelId, GuildMessageDto content);
}
