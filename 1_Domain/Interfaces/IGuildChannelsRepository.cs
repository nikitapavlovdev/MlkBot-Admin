using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces;

public interface IGuildChannelsRepository
{

    Task SyncDbVoiceChannelsWithGuildAsync(ulong guildId, CancellationToken token);

    Task UpsertDbTextChannelAsync(GuildTextChannel channel);
    Task UpsertDbVoiceChannelAsync(GuildVoiceChannel channel);

    Task RemoveDbTextChannelAsync(ulong id);
    Task RemoveDbVoiceChannelAsync(ulong id);

    Task<bool> IsTemporaryVoiceChannel(ulong id, CancellationToken token);
    Task<bool> IsGeneratingVoiceChannel(ulong id, CancellationToken token);
}
