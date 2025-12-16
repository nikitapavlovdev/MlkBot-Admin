using MlkAdmin.Shared.Results;

namespace MlkAdmin._1_Domain.Interfaces.Roles;

public interface IGuildRolesService
{
    Task<BaseResult> AssignRoleAsync(ulong memberId, ulong guildRoleId);
    Task<BaseResult> AssignRolesAsync(ulong memberId, IReadOnlyCollection<ulong> guildRolesIds);
    Task<BaseResult> RemoveRoleAsync(ulong memberId, ulong guildRoleId);
    Task<BaseResult> RemoveRolesAsync(ulong memberId, IReadOnlyCollection<ulong> guildRolesIds);
}