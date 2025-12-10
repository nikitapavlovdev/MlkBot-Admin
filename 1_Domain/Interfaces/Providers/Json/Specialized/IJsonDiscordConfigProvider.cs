namespace MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

public interface IJsonDiscordConfigProvider
{
    string MalenkieApiKey { get; }
    string DeveloperName { get; }
    string DeveloperAvatarLink { get; }
    ulong GuildId { get; }
}
