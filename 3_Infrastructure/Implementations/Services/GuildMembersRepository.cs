using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.Users;
using MlkAdmin._3_Infrastructure.DataBase;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Managers.Users;

public class GuildMembersRepository(
    ILogger<GuildMembersRepository> logger,
    IDiscordService discordService,
    MlkAdminDbContext mlkAdminDbContext) : IGuildMembersRepository
{
    public async Task<BaseResult> UpsertGuildMemberAsync(GuildMember member, CancellationToken token)
    {
        try
        {
            var dbMember = await mlkAdminDbContext.GuildMembers.FirstOrDefaultAsync(x => x.DiscordId == member.DiscordId);

            if (dbMember is null)
            {
                await mlkAdminDbContext.GuildMembers.AddAsync(member);

                logger.LogInformation(
                    "Участник сервера {member.DisplayName} успешно занесен в базу данных", 
                    member.DisplayName);

            }
            else
            {
                dbMember.DisplayName = member.DisplayName;
            }

            await mlkAdminDbContext.SaveChangesAsync();

            return BaseResult.Success(
                $"Пользователь {member.DisplayName} успешно обновлен в базе данных");
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке добавить или обновить пользователя под Id {memberId} в базе данных\nОшибка: {ErrorMessage}",
                member.DiscordId,
                exception.Message);

            return BaseResult.Fail(
                $"Произошла ошибка при обновление участника {member.DisplayName} в базе данных!", 
                new(
                    ErrorCodes.NO_ERROR, 
                    $"{exception.Message}"));
        }
    }
    public async Task<BaseResult<GuildMember>> GetDbGuildMemberAsync(ulong memberId, CancellationToken token)
    {
        try
        {
            var member = await mlkAdminDbContext.GuildMembers.FirstOrDefaultAsync(x => x.DiscordId == memberId);

            if (member is null)
            {
                logger.LogInformation(
                    "Участник сервера не найден");

                return BaseResult<GuildMember>.Fail(
                    new(
                        ErrorCodes.VARIABLE_IS_NULL, 
                        "Поле member является null"));
            }

            return BaseResult<GuildMember>.Success(member);

        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке получить пользователя с Id {memberId} из базы даннах\nСообщение: {ErrorMessage}",
                memberId,
                exception.Message);

            return BaseResult<GuildMember>.Fail(
                    new(
                        ErrorCodes.ENTERNAL_ERROR,
                        $"{exception.Message}"));
        }
    }
    public async Task<BaseResult> RemoveDbGuildMemberAsync(GuildMember member, CancellationToken token)
    {
        try
        {
            mlkAdminDbContext.Remove(member);
            await mlkAdminDbContext.SaveChangesAsync(token);

            logger.LogInformation(
                "Участник {member.DisplayName} успешно удален из базы данных",
                member.DisplayName);

            return BaseResult.Success($"Участник сервера {member.DisplayName} успешно удален из базы данных");
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке удалить участника с Id {MemberId} из базы данных\nСообщение: {ErrorMessage}",
                member.DiscordId,
                exception.Message);

            return BaseResult.Fail(
                "Непредвиденная ошибка при удаление участника из базы данных",
                new(
                    ErrorCodes.NO_ERROR, ""));
        }
    }
    public async Task<BaseResult> SyncGuildMembersAsync(CancellationToken token)
    {
        var guild = discordService.GetGuild();

        await using var transaction = await mlkAdminDbContext.Database.BeginTransactionAsync(token);

        try
        {
            var dbMembersIds = (await mlkAdminDbContext.GuildMembers
                .Select(x => x.DiscordId)
                .ToListAsync(token))
                .ToHashSet();

            var clientMembersIds = guild.Users.Select(x => x.Id).ToHashSet();
            var clientMembers = guild.Users;

            var toUpdate = clientMembers.Where(x => dbMembersIds.Contains(x.Id));
            foreach(var guildMember in toUpdate)
            {
                var dbGuildMember = await mlkAdminDbContext.GuildMembers.FirstOrDefaultAsync(x => x.DiscordId == guildMember.Id);

                dbGuildMember?.DisplayName = guildMember.DisplayName;
            }

            var toRemove = dbMembersIds.Where(x => !clientMembersIds.Contains(x));
            await mlkAdminDbContext.GuildMembers
                .Where(x => toRemove.Contains(x.DiscordId))
                .ExecuteDeleteAsync(token);

            var toAdd = clientMembers.Where(x => !dbMembersIds.Contains(x.Id));
            foreach(var guildMember in toAdd)
            {
                await mlkAdminDbContext.GuildMembers.AddAsync(
                    new GuildMember()
                    {
                        DiscordId = guildMember.Id,
                        DisplayName = guildMember.DisplayName,
                        JoinedAt = guildMember.JoinedAt,
                    }
                );
            }

            await mlkAdminDbContext.SaveChangesAsync(token);
            await transaction.CommitAsync(token);

            logger.LogInformation(
                "Синхронизация прошла успешно");

            return BaseResult.Success(
                "Синхронизация прошла успешно");

        }
        catch (OperationCanceledException operationException)
        {
            await transaction.RollbackAsync(token);
            return BaseResult.Fail(
                "Ошибка при выполнения записи в базу данных", new(ErrorCodes.ENTERNAL_ERROR, $"{operationException.Message}"));
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(token);

            logger.LogError(
                exception,
                "Ошибка при попытке синхронизации участников сервера\nОшибка: {ErrorMessage}",
                exception.Message);

            return BaseResult.Fail(
                "Произошла ошибка :(", 
                new(
                    ErrorCodes.NO_ERROR, 
                    $"{exception.Message}"));
        }
    }
    public async Task<BaseResult<string>> GetPersonalRoomNameFromDbAsync(ulong memberId, CancellationToken token)
    {
        try
        {
            var dbMember = await mlkAdminDbContext.GuildMembers.FirstOrDefaultAsync(x => x.DiscordId == memberId, token);

            if (dbMember is null)
            {
                logger.LogInformation(
                    "Пользователь под Id {MemberId} не найден в базе данных",
                    memberId);

                return BaseResult<string>.Fail(
                    new(
                        ErrorCodes.VARIABLE_IS_NULL, 
                        "Пользователь не найден"));
            }

            return BaseResult<string>.Success(dbMember.PersonalRoomName);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке получить имя персональной комнаты участника сервера с Id {MemberId}\nОшибка: {ErrorMessage}",
                memberId,
                exception.Message);

            return BaseResult<string>.Fail(
                new(
                    ErrorCodes.ENTERNAL_ERROR, 
                    $"{exception.Message}"));
        }
    }

    public async Task<BaseResult> UpdatePersonalRoomNameAsync(ulong memberId, string roomName, CancellationToken token)
    {
        try
        {
            var member = await mlkAdminDbContext.GuildMembers.FirstOrDefaultAsync(x => x.DiscordId == memberId);
            
            if(member is null)
            {
                logger.LogInformation(
                    "Участник с Id {MemberId} не найден в базе данных",
                    memberId);

                return BaseResult.Fail("Ваши данные как участника сервера не найдены в базе данных. Обратитесь к никитке",
                    new(
                        ErrorCodes.ENTERNAL_ERROR, 
                        "В методе UpdatePersonalRoomNameAsync поле member пришло пустым"));
            }

            member.PersonalRoomName  = roomName;

            await mlkAdminDbContext.SaveChangesAsync();

            return BaseResult.Success(
                $"Имя личной комнаты успешно обновлено на {roomName}");
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка про попытке обновить имя личной комнаты\nСообщение: {ErrorMessage}",
                exception.Message);

            return BaseResult.Fail(
                "Упс, возникла ошибка :(",
                new(
                    ErrorCodes.ENTERNAL_ERROR,
                    exception.Message));
        }
    }
}