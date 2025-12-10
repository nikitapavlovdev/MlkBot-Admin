using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.JsonModels.Pictures
{
    public class RootDiscordPictures
    {
        [JsonProperty(nameof(Pinterest))]
        public Pinterest? Pinterest { get; set; }
    }

    public class Pinterest
    {
        [JsonProperty(nameof(ForMessage))]
        public ForMessage? ForMessage { get; set; }
    }

    public class ForMessage
    {
        [JsonProperty(nameof(LinkPurpleEyes))]
        public string LinkPurpleEyes { get; set; } = string.Empty;

        [JsonProperty(nameof(LinkPinkEyes))]
        public string LinkPinkEyes { get; set; } = string.Empty;

        [JsonProperty(nameof(LinkGreenEyes))]
        public string LinkGreenEyes { get; set; } = string.Empty;

        [JsonProperty(nameof(BlackWhiteCloud1))]
        public string BlackWhiteCloud1 { get; set; } = string.Empty;

        [JsonProperty(nameof(RolesBanner))]
        public string RolesBanner { get; set; } = string.Empty;

        [JsonProperty(nameof(ColorNameBanner))]
        public string ColorNameBanner { get; set; } = string.Empty;

        [JsonProperty(nameof(RulesBanner))]
        public string RulesBanner { get; set; } = string.Empty;

        [JsonProperty(nameof(WelcomeMessage))]
        public string WelcomeMessage { get; set; } = string.Empty;

        [JsonProperty(nameof(AuMessage))]
        public string AuMessage { get; set; } = string.Empty;

        [JsonProperty(nameof(AutoLobbyNamingMessage))]
        public string AutoLobbyNamingMessage { get; set; } = string.Empty;

        [JsonProperty(nameof(ServerPeculiaritiesImg))]
        public string ServerPeculiaritiesImg { get; set; } = string.Empty;
    }
}
