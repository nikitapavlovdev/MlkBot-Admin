using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Interfaces.Managers;

public interface IGuildMembersManager
{
    Task AuthorizeGuildMemberAsync(ulong memberId, string guildMemberMention);
    Task<BaseResult> UpdateGuildMemberColorRoleAsync(ulong guildMemberId, string guildRoleKey);
    Task<BaseResult> SendWelcomeMessageAsync(ulong guildMemberId);

}
