using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
using MlkAdmin._3_Infrastructure.JsonModels.Configuration;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider;

public class JsonDiscordConfigProvider(string path, ILogger<JsonDiscordConfigProvider> logger) : JsonProviderBase<RootDiscordConfiguration>(path, logger), IJsonDiscordConfigProvider
{
    public string MalenkieApiKey => GetConfig().MalenkieAdminBot.API_KEY;
    public string DeveloperName => GetConfig().DevelopersData.Name;
    public string DeveloperAvatarLink => GetConfig().DevelopersData.IconLink;
    public ulong GuildId => GetConfig().Guild.Id;
}