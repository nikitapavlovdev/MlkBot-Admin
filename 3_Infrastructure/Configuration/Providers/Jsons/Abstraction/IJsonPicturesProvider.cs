using MlkAdmin._1_Domain.Interfaces.Discord;

namespace MlkAdmin._1_Domain.Interfaces.Providers.Json.Specialized;

public interface IJsonPicturesProvider : IJsonProvider, IJsonConfigurableProvider
{
    string PurpleEyesLink { get; }
    string PinkEyesLink {  get; }
    string GreenEyesLink {  get; }
    string BlackWhiteCloudLink { get; }
    string RolesBannerLink { get; }
    string ColorNameBannerLink { get;  }
    string RulesBannerLink { get; }
    string WelcomeMessagePictureLink { get; }
    string AuMessagePictureLink { get; }
}
