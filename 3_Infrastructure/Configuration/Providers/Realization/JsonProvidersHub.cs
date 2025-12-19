using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

namespace MlkAdmin._3_Infrastructure.Configuration.Providers.Jsons.Realization;

public class JsonProvidersHub(
    IJsonCategoriesProvider categoriesProvider,
    IJsonChannelsProvider channelsProvider,
    IJsonDiscordConfigProvider configProvider,
    IJsonDynamicMessageProvider dynamicMessageProvider,
    IJsonEmotesProvider emotesProvider,
    IJsonPicturesProvider picturesProvider,
    IJsonRolesProvider rolesProvider) : IJsonProvidersHub
{
    public IJsonCategoriesProvider Categories => categoriesProvider;

    public IJsonChannelsProvider Channels => channelsProvider;

    public IJsonDiscordConfigProvider DiscordConfig => configProvider;

    public IJsonDynamicMessageProvider DynamicMessage => dynamicMessageProvider;

    public IJsonEmotesProvider Emotes => emotesProvider;

    public IJsonPicturesProvider Pictures => picturesProvider;

    public IJsonRolesProvider Roles => rolesProvider;
}
