using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.ButtonExecuted;

public class ButtonExecutedHandler(
    ILogger<ButtonExecutedHandler> logger) : INotificationHandler<ButtonExecuted>
{
    public async Task Handle(ButtonExecuted notification, CancellationToken cancellationToken)
    {
        try
        {
            if (notification.SocketMessageComponent.User is not SocketGuildUser socketGuildUser) return;

            await Task.CompletedTask;
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Error: {Message} StackTrace: {StackTrace}", 
                exception.Message,
                exception.StackTrace);

            throw;
        }
    }
}
