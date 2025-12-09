using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces.Users;

public interface IGuildVoiceSessionRepository
{
    public Task AddGuildVoiceSessionAsync(GuildVoiceSession session);
}
