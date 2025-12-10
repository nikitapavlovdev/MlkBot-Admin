using Newtonsoft.Json;

namespace MlkAdmin._3_Infrastructure.JsonModels.Emotes
{
    public class RootDiscordEmotes
    {
        [JsonProperty(nameof(AnimatedEmotes))]
        public AnimatedEmotes? AnimatedEmotes { get; set; }

        [JsonProperty(nameof(StaticEmotes))]
        public StaticEmotes? StaticEmotes { get; set; }
    }

    public class AnimatedEmotes
    {
        [JsonProperty("Zero2Peace")]
        public string ZeroPeaceEmoteName { get; set; } = string.Empty;

        [JsonProperty("Zero2Hyped")]
        public string ZeroHypedEmoteName { get; set; } = string.Empty;

        [JsonProperty("EmberBunnyDance")]
        public string EmberBunnyDanceEmoteName { get; set; } = string.Empty;
    }
    
    public class StaticEmotes
    {
        [JsonProperty("Zero2Woh")]
        public string ZeroWohEmoteName { get; set; } = string.Empty;

        [JsonProperty("Zero2Heart")]
        public string ZeroHeartEmoteName { get; set; } = string.Empty;
    }
}
