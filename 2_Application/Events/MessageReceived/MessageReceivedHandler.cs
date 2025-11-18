using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Managers.Users.Stat;

namespace MlkAdmin._2_Application.Events.MessageReceived;

public class MessageReceivedHandler(
    ILogger<MessageReceivedHandler> logger,
    UserStatManager userStatManager) : INotificationHandler<MessageReceived>
{
    public async Task Handle(MessageReceived notification, CancellationToken cancellationToken)
    {
        try
        {
            if (notification.SocketMessage.Author.IsBot || notification.SocketMessage is not SocketUserMessage socketUserMessage) 
                return;

            SocketGuildUser? author = notification.SocketMessage.Author as SocketGuildUser;

            await userStatManager.UpdateMessageStatAsync(author.Id, notification.SocketMessage);
        }
        catch (Exception ex)
        {
            logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        }
    }
}
