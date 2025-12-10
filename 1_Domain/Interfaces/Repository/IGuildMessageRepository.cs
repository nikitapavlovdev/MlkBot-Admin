using MlkAdmin._1_Domain.Entities;

namespace MlkAdmin._1_Domain.Interfaces.Messages;

public interface IGuildMessageRepository
{
    public Task AddMessageAsync(GuildMessage message, CancellationToken token);
}
