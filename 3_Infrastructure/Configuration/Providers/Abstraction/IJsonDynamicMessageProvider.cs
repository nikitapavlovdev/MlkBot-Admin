using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

public interface IJsonDynamicMessageProvider : IJsonProvider, IJsonConfigurableProvider
{
    ulong AuthorizationMessageId { get; }
    ulong RulesMessageId { get; }
    ulong GeneralRolesMessageId { get; }
    ulong ColorNicknameMessageId { get; }
}
