using Newtonsoft.Json;
using MlkAdmin.Shared.DTOs.GuildData;

namespace MlkAdmin._3_Infrastructure.JsonModels.Roles;

public class RolesListModel
{
    [JsonProperty(nameof(GuildRoles))]
    public List<GuildRoleInfo> GuildRoles { get; set; } = [];
}



