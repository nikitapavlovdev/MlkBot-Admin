using MlkAdmin._1_Domain.Entities;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._1_Domain.Interfaces.Messages;

public interface IGuildMessagesRepository
{
    public Task<BaseResult> AddMessageAsync(GuildMessage message, CancellationToken token);
}
