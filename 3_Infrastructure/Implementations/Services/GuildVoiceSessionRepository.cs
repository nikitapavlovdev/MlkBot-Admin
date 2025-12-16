using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces.Users;

namespace MlkAdmin._3_Infrastructure.Implementations.Services;

public class GuildVoiceSessionRepository(ILogger<GuildVoiceSessionRepository> logger) : IGuildVoiceSessionRepository
{
    public Task AddGuildVoiceSessionAsync(GuildVoiceSession session)
    {
        throw new NotImplementedException();
    }
}
