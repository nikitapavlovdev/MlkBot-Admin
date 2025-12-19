using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Interfaces.Roles;
using MlkAdmin._1_Domain.Managers;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin.Shared.Results;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Implementations.Managers;

public class GuildMembersManager(
    ILogger<GuildMembersManager> logger,
    IJsonProvidersHub providersHub,
    IGuildRolesService roleService,
    IGuildMessagesService messagesService,
    IDiscordService discordService) : IGuildMembersManager
{
    public async Task AuthorizeGuildMemberAsync(ulong guildMemberId, string guildMemberMention)
    {
        try
        {
            await roleService.RemoveRoleAsync(guildMemberId, providersHub.Roles.GetRoleByKey("NotAuthorized").Value.Id);
            await roleService.AssignRolesAsync(
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
    public async Task<BaseResult> UpdateGuildMemberColorRoleAsync(ulong guildMemberId, string guildRoleKey)
    {
        try
        {
            var colorRolesIds = providersHub.Roles.GetColorRolesIds().Value;
            var targetColorRoleId = providersHub.Roles.GetRoleByKey(guildRoleKey).Value.Id; 

            await roleService.RemoveRolesByFilterModeAsync(guildMemberId, colorRolesIds, true);

            logger.LogInformation(
                "Удалены цветовые роли {colorRolesIds} у участника {guildMemberId}",
                string.Join(",", colorRolesIds),
                guildMemberId);

            await roleService.AssignRoleAsync(guildMemberId, targetColorRoleId);

            logger.LogInformation(
                "Добавлена цветовая роль {guildRoleId} участнику {guildMemberId}",
                targetColorRoleId, guildMemberId);

            return BaseResult.Success("Цвет имени успешно изменен");
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке обновить роль для смены цвета");

            return BaseResult.Fail(
                "Упс-с, произошла ошибка!", 
                new(ErrorCodes.ENTERNAL_ERROR,
                exception.Message));
        }
    }
    public async Task<BaseResult> SendWelcomeMessageAsync(ulong guildMemberId)
    {
        try
        {
            var newMember = (await discordService.GetGuildMemberAsync(guildMemberId)).Value;

            var message = new GuildMessageDto()
            {
                Message = newMember.Mention,
                Embed =
                    new GuildMessageEmbedDto()
                    {
                        Color = new(52, 52, 52),
                        Title = $"Привет, {newMember.DisplayName}!",
                        Description = "Добро пожаловать в гильдию маленьких\n" +
                            "Пройдите авторизацию по прикрепленной ссылке снизу",
                        AuthorDto =
                            new GuildMessageEmbedAuthorDto()
                            {
                                IconUrl = newMember.GetAvatarUrl(),
                                Name = newMember.DisplayName
                            },
                        FooterDto =
                            new GuildMessageEmbedFooterDto()
                            {
                                IconUrl = providersHub.DiscordConfig.DeveloperAvatarLink,
                                Text = providersHub.DiscordConfig.DeveloperName
                            },
                        WithTimestamp = true
                    }
            };

            await messagesService.SendMessageInChannelAsync(providersHub.Channels.WelcomeTextChannelId, message);

            logger.LogInformation(
                "Сообщение пользователю {NewMemberId} успешно отправлено",
                guildMemberId);

            return BaseResult.Success(
                $"Приветственное сообщение пользователю {newMember.DisplayName} успешно отправлено");
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Не удалось отправить приветственное сообщение новому пользователю\nСообщение ошибки: {ErrorMessage}",
                exception.Message);

            return BaseResult.Fail("Уп-с, произошла ошибка!",
                new(ErrorCodes.ENTERNAL_ERROR, exception.Message));

            throw;
        }
        
    }
}