using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Managers;

namespace MlkAdmin._2_Application.Events.ReactionAdded;

public class ReactionAddedHandler(
    ILogger<ReactionAddedHandler> logger,
    IJsonProvidersHub providersHub,
    IGuildMembersManager membersManager) : INotificationHandler<ReactionAdded>
{
    public async Task Handle(ReactionAdded notification, CancellationToken cancellationToken)
    {
        try
        {
            if (notification.Message.Id == providersHub.DynamicMessage.AuthorizationMessageId)
                await membersManager.AuthorizeGuildMemberAsync(notification.Reaction.User.Value.Id, notification.Reaction.User.Value.Mention);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Error: {Message} StackTrace: {StackTrace}",
                exception.Message,
                exception.StackTrace);
        }
    }
}