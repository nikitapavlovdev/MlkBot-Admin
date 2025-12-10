using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
using MlkAdmin._3_Infrastructure.JsonModels.Emotes;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider;

public class JsonEmotesProvider(string path, ILogger<JsonEmotesProvider> logger) : JsonProviderBase<RootDiscordEmotes>(path, logger), IJsonEmotesProvider
{
    public string ZeroPeaceEmoteName => GetConfig().AnimatedEmotes.ZeroPeaceEmoteName;

    public string ZeroHypedEmoteName => GetConfig().AnimatedEmotes.ZeroHypedEmoteName;

    public string ZeroWohEmoteName => GetConfig().StaticEmotes.ZeroWohEmoteName;

    public string ZeroHeartEmoteName => GetConfig().StaticEmotes.ZeroHeartEmoteName;
}