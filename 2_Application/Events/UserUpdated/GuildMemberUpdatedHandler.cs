using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.UserUpdated;

public class GuildMemberUpdatedHandler(
    ILogger<GuildMemberUpdatedHandler> logger) : INotificationHandler<GuildMemberUpdated>
{
    public async Task Handle(GuildMemberUpdated notification, CancellationToken cancellationToken)
    {
			try
			{
            
        }
        catch (Exception ex)
        {
            logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        }
    }
}
