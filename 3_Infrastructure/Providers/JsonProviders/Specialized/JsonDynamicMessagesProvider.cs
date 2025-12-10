using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
using MlkAdmin._3_Infrastructure.JsonModels.DynamicMessages;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider
{
    public class JsonDynamicMessagesProvider(string path, ILogger<JsonDynamicMessagesProvider> logger) : JsonProviderBase<RootDynamicMessages>(path, logger), IJsonDynamicMessageProvider
    {
        public ulong AutorizationMessageId => GetConfig().Messages.ServerHub.Autorization.Id;
        public ulong RulesMessageId => GetConfig().Messages.Rules.Id;
        public ulong GeneralRolesMessageId => GetConfig().Messages.Roles.Main.Id;
        public ulong ColorNicknameMessageId => GetConfig().Messages.Roles.NameColor.Id;
    }
}
