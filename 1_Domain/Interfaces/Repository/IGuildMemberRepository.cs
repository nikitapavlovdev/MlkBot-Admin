using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces.Users;

public interface IGuildMemberRepository
{
    Task UpsertGuildMemberAsync(GuildMember member);
    Task SyncGuildMembersAsync(ulong guildId);
    Task RemoveDbGuildMemberAsync(ulong id);
    Task<GuildMember?> GetDbGuildMemberAsync(ulong id);
}
