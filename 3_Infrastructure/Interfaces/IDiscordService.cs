using Discord.WebSocket;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._3_Infrastructure.Interfaces;

public interface IDiscordService
{
    Task<BaseResult<SocketGuildUser>> GetGuildMemberAsync(ulong memberId); 
    DiscordSocketClient DiscordClient { get; }
}
