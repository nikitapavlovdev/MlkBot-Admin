using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Interfaces.Roles;
using MlkAdmin._1_Domain.Managers;

namespace MlkAdmin._2_Application.Managers;

public class AuthorizationManager(
    ILogger<AuthorizationManager> logger,
    IJsonProvidersHub providersHub,
    IGuildRolesService roleService,
    IGuildMessagesService messagesService) : IGuildMembersManager
{
    public async Task AuthorizeGuildMemberAsync(ulong guildMemberId, string guildMemberMention)
    {
        try
        {
            await roleService.RemoveRoleFromUserAsync(guildMemberId, providersHub.Roles.GetRoleByKey("NotAuthorized").Value.Id);
            await roleService.AddRolesToUserAsync(
               guildMemberId,
               [
                   providersHub.Roles.GetRoleByKey("GuildMember").Value.Id,
                        providersHub.Roles.GetRoleByKey("PlayersClub").Value.Id
               ]
            );
        }
        catch (Exception exception)
        {
            logger.LogError(
                "Произошла ошибка про попытке авторизовать пользователя с Id {MemberId}\nОшибка: {Error}",
                guildMemberId,
                exception.Message);

            return;
        }

        try
        {
            await messagesService.SendMessageInChannelAsync(
                providersHub.Channels.AdminChatTextChannelId,
                new()
                {
                    Title = $"Авторизация пользователя {guildMemberMention}",
                    Description = $"Пользователь успешно прошел авторизацию",
                    MemberId = guildMemberId
                }
            );
        }
        catch (Exception exception)
        {
            logger.LogError(
                "Произошла ошибка про попытке отправить сообщение в канал с Id {}\nОшибка: {Error}",
                providersHub.Channels.AdminChatTextChannelId,
                exception.Message);

            return;
        }
    }
}