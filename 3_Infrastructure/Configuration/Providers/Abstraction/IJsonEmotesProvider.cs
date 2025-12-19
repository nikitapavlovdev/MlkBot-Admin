using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
public interface IJsonEmotesProvider : IJsonProvider, IJsonConfigurableProvider
{
    string ZeroPeaceEmoteName { get; }
    string ZeroHypedEmoteName { get; }
    string ZeroWohEmoteName { get; }
    string ZeroHeartEmoteName { get; }
}
