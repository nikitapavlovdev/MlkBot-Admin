using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Interfaces.Services;

public interface IGuildRolesService
{
    Task<BaseResult> AssignRoleAsync(ulong memberId, ulong guildRoleId);
    Task<BaseResult> AssignRolesAsync(ulong memberId, IReadOnlyCollection<ulong> guildRolesIds);
    Task<BaseResult> RemoveRoleAsync(ulong memberId, ulong guildRoleId);
    Task<BaseResult> RemoveRolesAsync(ulong memberId, IReadOnlyCollection<ulong> guildRolesIds);

    Task<BaseResult> RemoveRolesByFilterModeAsync(ulong memberId, IReadOnlyCollection<ulong> rolesIds, bool isMatching);
}