using MlkAdmin.Shared.DTOs.GuildData;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

public interface IJsonRolesProvider
{
    List<GuildRoleInfo> GuildRoles { get; }
    BaseResult<GuildRoleInfo> GetRoleByKey(string key);
}
