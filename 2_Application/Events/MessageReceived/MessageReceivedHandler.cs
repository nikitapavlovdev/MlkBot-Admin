using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces;

namespace MlkAdmin._2_Application.Events.MessageReceived;

public class MessageReceivedHandler(
    ILogger<MessageReceivedHandler> logger,
    IGuildMessagesRepository messageRepository) : INotificationHandler<MessageReceived>
{
    public async Task Handle(MessageReceived notification, CancellationToken token)
    {
        try
        {
            await messageRepository.AddMessageAsync(
                new()
                {
                    DiscordUserId = notification.SocketMessage.Author.Id,
                    SentAt = DateTime.UtcNow,
                    TChannelId = notification.SocketMessage.Channel.Id,
                    Content = notification.SocketMessage.Content
                }, 
                token
            );
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