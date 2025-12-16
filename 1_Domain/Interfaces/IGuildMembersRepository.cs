using MlkAdmin._1_Domain.Entities;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._1_Domain.Interfaces.Users;

public interface IGuildMembersRepository
{
    Task<BaseResult> UpsertGuildMemberAsync(GuildMember member, CancellationToken token);
    Task<BaseResult<GuildMember>> GetDbGuildMemberAsync(ulong memberId, CancellationToken token);
    Task<BaseResult> RemoveDbGuildMemberAsync(GuildMember member, CancellationToken token);
    Task<BaseResult> SyncGuildMembersAsync(CancellationToken token);
    Task<BaseResult<string>> GetPersonalRoomNameFromDbAsync(ulong memberId, CancellationToken token);
    Task<BaseResult> UpdatePersonalRoomNameAsync(ulong memberId, string roomName, CancellationToken token);


}
