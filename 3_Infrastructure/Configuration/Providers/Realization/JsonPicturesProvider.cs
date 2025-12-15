
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;
using MlkAdmin._3_Infrastructure.JsonModels.Pictures;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider.Abstraction;

namespace MlkAdmin._3_Infrastructure.Providers.JsonProvider;

public class JsonPicturesProvider(string path, ILogger<JsonPicturesProvider> logger) : JsonProviderBase<RootDiscordPictures>(path, logger), IJsonPicturesProvider
{
    public string PurpleEyesLink => GetConfig().Pinterest.ForMessage.LinkPurpleEyes;
    public string PinkEyesLink => GetConfig().Pinterest.ForMessage.LinkPinkEyes;
    public string GreenEyesLink => GetConfig().Pinterest.ForMessage.LinkGreenEyes;
    public string BlackWhiteCloudLink => GetConfig().Pinterest.ForMessage.BlackWhiteCloud1;
    public string RolesBannerLink => GetConfig().Pinterest.ForMessage.RulesBanner;
    public string ColorNameBannerLink => GetConfig().Pinterest.ForMessage.ColorNameBanner;
    public string RulesBannerLink => GetConfig().Pinterest.ForMessage.RulesBanner;
    public string WelcomeMessagePictureLink => GetConfig().Pinterest.ForMessage.WelcomeMessage;
    public string AuMessagePictureLink => GetConfig().Pinterest.ForMessage.AuMessage;
}