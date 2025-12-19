using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Interfaces.Services;

public interface IGuildChannelsService
{
    Task<BaseResult<SocketGuildChannel>> GetGuildChannelAsync(ulong channelId);
    Task<BaseResult<RestVoiceChannel>> CreateVoiceChannelAsync(ulong guildMemberId);
}
