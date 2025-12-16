using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.Roles;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._3_Infrastructure.Services;

public class GuildRolesService(
    ILogger<GuildRolesService> logger, 
    IDiscordService discordService) : IGuildRolesService
{
    public async Task<BaseResult> AssignRoleAsync(ulong memberId, ulong guildRoleId)
    {
        try
        {
            var guildMember = (await discordService.GetGuildMemberAsync(memberId)).Value;

            if (guildMember is null)
            {
                logger.LogWarning(
                    "Искомый участник сервера c Id {memberId} не найден",
                    memberId);

                return BaseResult.Fail(
                    $"{memberId} не является участником сервера",
                    new Error(
                        ErrorCodes.VARIABLE_IS_NULL,
                        $"Переменная guildMember является null. Участник с Id {memberId} не найден")
                );
            }

            await guildMember.AddRoleAsync(guildRoleId);

            logger.LogInformation(
                "Роль для участника {memberName} с Id {memberId} успешно выдана",
                guildMember.DisplayName,
                guildMember.Id);

            return BaseResult.Success(
                $"Роль для пользователя {guildMember.Mention} успешно выдана!");

        }
        catch (Exception exception)
        {
            logger.LogError(
                            exception,
                            "Ошибка при попытке выдать роль участнику {memberId}\nСообщение: {ErrorMessage}",
                            memberId,
                            exception.Message);

            return BaseResult.Fail(
                $"Произошла ошибка при попытке добавить роли для пользователя с Id {memberId}",
                new Error(
                    ErrorCodes.ROLE_ASSIGNMENT_FAILED,
                    $"Детали ошибки: {exception.Message}"));
        }
    }
    public async Task<BaseResult> AssignRolesAsync(ulong memberId, IReadOnlyCollection<ulong> guildRolesIds)
    {
        try
        {
            var guildMember = (await discordService.GetGuildMemberAsync(memberId)).Value;

            if (guildMember is null)
            {
                logger.LogWarning(
                    "Искомый участник сервера c Id {memberId} не найден",
                    memberId);

                return BaseResult.Fail(
                    $"{memberId} не является участником сервера",
                    new Error(
                        ErrorCodes.VARIABLE_IS_NULL, 
                        $"Переменная guildMember является null. Участник с Id {memberId} не найден")
                );
            }

            await guildMember.AddRolesAsync(guildRolesIds);

            logger.LogInformation(
                "Роли для участника {memberName} с Id {memberId} успешно выданы",
                guildMember.DisplayName,
                guildMember.Id);

            return BaseResult.Success(
                $"Роли для пользователя {guildMember.Mention} успешно выданы!"); 

        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке выдать роли участнику {memberId}\nСообщение: {ErrorMessage}",
                memberId, 
                exception.Message);

            return BaseResult.Fail(
                $"Произошла ошибка при попытке добавить роли для пользователя с Id {memberId}",
                new Error(
                    ErrorCodes.ROLE_ASSIGNMENT_FAILED, 
                    $"Детали ошибки: {exception.Message}"));
        }
    }
    public async Task<BaseResult> RemoveRoleAsync(ulong memberId, ulong guildRoleId)
    {
        try
        {
            var guildMember = (await discordService.GetGuildMemberAsync(memberId)).Value;

            if (guildMember is null)
            {
                logger.LogWarning(
                    "Искомый участник сервера c Id {memberId} не найден",
                    memberId);

                return BaseResult.Fail(
                    $"{memberId} не является участником сервера",
                    new Error(
                        ErrorCodes.VARIABLE_IS_NULL,
                        $"Переменная guildMember является null. Участник с Id {memberId} не найден")
                );
            }

            await guildMember.RemoveRoleAsync(guildRoleId);

            logger.LogInformation(
                "Роль участника {memberName} с Id {memberId} успешно удалена",
                guildMember.DisplayName,
                guildMember.Id);

            return BaseResult.Success(
                $"Роль пользователя {guildMember.Mention} успешно удалена!");

        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке удалить роль участника {memberId}\nСообщение: {ErrorMessage}",
                memberId,
                exception.Message);

            return BaseResult.Fail(
                $"Произошла ошибка при попытке удалить роль для пользователя с Id {memberId}",
                new Error(
                    ErrorCodes.ROLE_REMOVAL_FAILED,
                    $"Детали ошибки: {exception.Message}"));
        }
    }
    public async Task<BaseResult> RemoveRolesAsync(ulong memberId, IReadOnlyCollection<ulong> guildRolesIds)
    {
        try
        {
            var guildMember = (await discordService.GetGuildMemberAsync(memberId)).Value;

            if (guildMember is null)
            {
                logger.LogWarning(
                    "Искомый участник сервера c Id {memberId} не найден",
                    memberId);

                return BaseResult.Fail(
                    $"{memberId} не является участником сервера",
                    new Error(
                        ErrorCodes.VARIABLE_IS_NULL,
                        $"Переменная guildMember является null. Участник с Id {memberId} не найден")
                );
            }

            await guildMember.RemoveRolesAsync(guildRolesIds);

            logger.LogInformation(
                "Роли участника {memberName} с Id {memberId} успешно удалены",
                guildMember.DisplayName,
                guildMember.Id);

            return BaseResult.Success(
                $"Роли пользователя {guildMember.Mention} успешно удалены!");

        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке удалить роли участника {memberId}\nСообщение: {ErrorMessage}",
                memberId,
                exception.Message);

            return BaseResult.Fail(
                $"Произошла ошибка при попытке удалить роли для пользователя с Id {memberId}",
                new Error(
                    ErrorCodes.ROLE_REMOVAL_FAILED,
                    $"Детали ошибки: {exception.Message}"));
        }
    }
}