using MlkAdmin._1_Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MlkAdmin.Shared.DTOs.GuildData;

public class GuildRoleInfo
{
    [JsonProperty(nameof(Id))]
    public ulong Id { get; set; }

    [JsonProperty(nameof(Key))]
    public string Key { get; set; } = string.Empty;

    [JsonProperty(nameof(Name))]
    public string? Name { get; set; }

    [JsonProperty(nameof(Description))]
    public string? Description { get; set; }

    [JsonProperty(nameof(Type))]
    [JsonConverter(typeof(StringEnumConverter))]
    public RoleType Type { get; set; }
}
