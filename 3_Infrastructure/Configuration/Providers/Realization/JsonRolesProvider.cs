using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
using MlkAdmin._3_Infrastructure.JsonModels.Roles;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;
using MlkAdmin.Shared.DTOs.GuildData;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider;

public class JsonRolesProvider(string path, ILogger<JsonRolesProvider> logger) : JsonProviderBase<RolesListModel>(path, logger), IJsonRolesProvider
{
    public List<GuildRoleInfo> GuildRoles => GetConfig().GuildRoles;

    public BaseResult<List<ulong>> GetColorRolesIds()
    {
        var guildRoles = GetConfig().GuildRoles;

        return BaseResult<List<ulong>>.Success([.. guildRoles.Where(x => x.Type == _1_Domain.Enums.RoleType.COLOR).Select(x => x.Id)]);
    }

    public BaseResult<GuildRoleInfo> GetRoleByKey(string key)
    {
        throw new NotImplementedException();
    }
}