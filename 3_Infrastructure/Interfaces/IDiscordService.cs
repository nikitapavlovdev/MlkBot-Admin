using Discord.WebSocket;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._3_Infrastructure.Interfaces;

public interface IDiscordService
{
    Task<BaseResult<SocketGuildUser>> GetGuildMemberAsync(ulong memberId);
    Task<BaseResult<IReadOnlyCollection<SocketGuildUser>>> GetGuildMembersAsync();
    Task<string> GetGuildMemberMentionByIdAsync(ulong memberId);
    SocketGuild GetGuild();
    DiscordSocketClient DiscordClient { get; }
}
