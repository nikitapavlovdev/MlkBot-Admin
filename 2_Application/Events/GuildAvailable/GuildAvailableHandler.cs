using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Services;

namespace MlkAdmin._2_Application.Events.GuildAvailable;

class GuildAvailableHandler(
    ILogger<GuildAvailableHandler> logger,
    IGuildInitializationService initializationService) : INotificationHandler<GuildAvailable>
{
    public async Task Handle(GuildAvailable notification, CancellationToken token)
    {
        try
        {
            await initializationService.InitializeAsync(notification.SocketGuild.Id, token);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Error: {Message}\nStackTrace: {StackTrace}",
                exception.Message, 
                exception.StackTrace);
        }
    }
}