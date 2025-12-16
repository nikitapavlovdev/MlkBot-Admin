using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Managers;

namespace MlkAdmin._2_Application.Events.UserJoined;

class UserJoinedHandler(
    ILogger<UserJoinedHandler> logger, 
    IGuildMembersManager membersManager) : INotificationHandler<UserJoined>
{
    public async Task Handle(UserJoined notification, CancellationToken cancellationToken)
    {
        try
        {
            if (notification.SocketGuildUser.IsBot)
            {
                logger.LogInformation(
                    "Участник {MemberName} является ботом",
                    notification.SocketGuildUser.GlobalName);

                return;
            }

            await membersManager.AuthorizeGuildMemberAsync(notification.SocketGuildUser.Id, notification.SocketGuildUser.Mention);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при работе события UserJoinedHandler");
        }
    }
}