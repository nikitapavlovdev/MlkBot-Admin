namespace MlkAdmin._1_Domain.Interfaces.Services;

public interface IGuildInitializationService
{
    Task InitializeAsync(ulong guildId, CancellationToken token);
}
