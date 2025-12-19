using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Managers;
using MlkAdmin.Shared.Extensions;

namespace MlkAdmin._2_Application.Events.SelectMenuExecuted;

class SelectMenuExecutedHandler(
    ILogger<SelectMenuExecutedHandler> logger,
    IGuildMembersManager membersManager) : INotificationHandler<SelectMenuExecuted>
{
    public async Task Handle(SelectMenuExecuted notification, CancellationToken cancellationToken)
    {
        try
        {
            await notification.SocketMessageComponent.DeferAsync();

            var values = notification.SocketMessageComponent.Data.Values;

            if (!EnumsExtension.TryParseCustomId(notification.SocketMessageComponent.Data.CustomId, out SelectionMenuCustomIds menuId))
            {
                logger.LogWarning(
                    "Не удалось преобразовать кастомное id меню {SelectionMenuId} в тип перечесления SelectionMenuCustomIds",
                    notification.SocketMessageComponent.Data.CustomId);

                return;
            };

            switch (menuId)
            {
                case SelectionMenuCustomIds.GUILD_NAMECOLOR_CHANGE:

                    await membersManager.UpdateGuildMemberColorRoleAsync(
                        notification.SocketMessageComponent.User.Id,
                        values.First());
                    
                    break;

                default:

                    logger.LogWarning(
                        "Использованное меню {SelectionMenuId} еще не добавлено в обработчик событий",
                        notification.SocketMessageComponent.Data.CustomId);

                    break;
            }
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