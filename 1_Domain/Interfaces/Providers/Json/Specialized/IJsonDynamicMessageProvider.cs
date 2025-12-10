using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

public interface IJsonDynamicMessageProvider : IJsonProvider, IJsonConfigurableProvider
{
    ulong AutorizationMessageId { get; set; }
    ulong RulesMessageId { get; set; }
    ulong GeneralRolesMessageId { get; set; }
    ulong ColorNicknameMessageId { get; set; }
}
