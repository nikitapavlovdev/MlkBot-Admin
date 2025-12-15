using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._1_Domain.Interfaces.Channels;

public interface IGuildChannelsService
{
    Task<BaseResult<SocketGuildChannel>> GetGuildChannelAsync(ulong channelId);
    Task<BaseResult<RestVoiceChannel>> CreateVoiceChannelAsync(ulong guildMemberId);
}
