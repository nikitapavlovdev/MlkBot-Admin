using MlkAdmin._1_Domain.Interfaces.Services;

namespace MlkAdmin._3_Infrastructure.Implementations.Services;

internal class GuildInitializationService : IGuildInitializationService
{
    public Task InitializeAsync(ulong guildId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
