namespace MlkAdmin._1_Domain.Managers;

public interface IGuildMembersManager
{
    Task AuthorizeGuildMemberAsync(ulong memberId, string guildMemberMention);
}
