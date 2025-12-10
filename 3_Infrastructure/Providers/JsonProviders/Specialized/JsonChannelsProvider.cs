using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
using MlkAdmin._3_Infrastructure.JsonModels.Channels;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider;

public class JsonChannelsProvider(string path, ILogger<JsonChannelsProvider> logger) : JsonProviderBase<RootChannel>(path, logger), IJsonChannelsProvider
{
    public ulong GeneratingVoiceChannelId => GetConfig().Channels.VoiceChannels.AutoLobby.AutoGamesLobby.Id;
    public ulong GeneralVoiceChannelId => GetConfig().Channels.VoiceChannels.MainLobby.GeneralLobby.Id;
    public ulong AdminChatTextChannelId => GetConfig().Channels.TextChannels.AdministratorCategory.Chat.Id;
    public ulong LogsTextChannelId => GetConfig().Channels.TextChannels.AdministratorCategory.Logs.Id;
    public ulong WelcomeTextChannelId => GetConfig().Channels.TextChannels.ServerCategory.Starting.Id;
    public ulong HubTextChannelId => GetConfig().Channels.TextChannels.ServerCategory.Hub.Id;
    public ulong RulesTextChannelId => GetConfig().Channels.TextChannels.ServerCategory.Rules.Id;
    public ulong RolesTextChannelId => GetConfig().Channels.TextChannels.ServerCategory.Roles.Id;
    public ulong NewsTextChannelId => GetConfig().Channels.TextChannels.ServerCategory.News.Id;
    public ulong BotCommandsTextChannelId => GetConfig().Channels.TextChannels.ServerCategory.BotCommands.Id;
    public string BotCommandsTextChannelLink => GetConfig().Channels.TextChannels.ServerCategory.BotCommands.Https;
    public ulong GeneralChatTextChannelId => GetConfig().Channels.TextChannels.BaseCategory.Chat.Id;
    public ulong HighlightTextChannelId => GetConfig().Channels.TextChannels.GameCategory.Highlight.Id;
    public ulong ValorantChatTextChannelId => GetConfig().Channels.TextChannels.GameCategory.ValChat.Id;
    public ulong DestinyChatTextChannelId => GetConfig().Channels.TextChannels.GameCategory.D2Chat.Id;
}
