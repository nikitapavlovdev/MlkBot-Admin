using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

public interface IJsonChannelsProvider : IJsonProvider, IJsonConfigurableProvider
{
    ulong GeneratingVoiceChannelId { get; }
    ulong GeneralVoiceChannelId { get; }
    ulong AdminChatTextChannelId { get; }
    ulong LogsTextChannelId { get; }
    ulong WelcomeTextChannelId { get; }
    ulong HubTextChannelId { get; }
    ulong RulesTextChannelId { get; }
    ulong RolesTextChannelId { get; }
    ulong NewsTextChannelId { get; }
    ulong BotCommandsTextChannelId { get; }
    string BotCommandsTextChannelLink { get; }
    ulong GeneralChatTextChannelId { get; }
    ulong HighlightTextChannelId { get; }
    ulong ValorantChatTextChannelId { get; }
    ulong DestinyChatTextChannelId { get; }
}