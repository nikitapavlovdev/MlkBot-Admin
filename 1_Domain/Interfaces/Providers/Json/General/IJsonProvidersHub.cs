using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

namespace MlkAdmin._1_Domain.Interfaces.Providers;

public interface IJsonProvidersHub
{
    IJsonCategoriesProvider Categories { get; }
    IJsonChannelsProvider Channels { get; }
    IJsonDiscordConfigProvider DiscordConfig { get; }
    IJsonDynamicMessageProvider DynamicMessage { get; }
    IJsonEmotesProvider Emotes { get; }
    IJsonPicturesProvider Pictures { get; }
}
