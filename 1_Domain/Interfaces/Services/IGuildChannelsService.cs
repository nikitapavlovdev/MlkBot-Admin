using Discord.WebSocket;
namespace MlkAdmin._1_Domain.Interfaces.Channels
{
    public interface IGuildChannelsService
    {
        public Task<SocketChannel?> GetChannelAsync(ulong channelId);
    }
}
