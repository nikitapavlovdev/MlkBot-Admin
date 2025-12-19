using Discord;
using Discord.Rest;
using Discord.WebSocket;
using MlkAdmin.Shared.Results;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces;
using MlkAdmin._3_Infrastructure.Interfaces;

namespace MlkAdmin._2_Application.Implementations.Services;

public class GuildChannelsService(
    ILogger<GuildChannelsService> logger,
    IJsonProvidersHub providersHub,
    IDiscordService discordService,
    IGuildMembersRepository membersRepository) : IGuildChannelsService
{
    private async Task<BaseResult<string>> GetPersonalRoomNameAsync(ulong memberId)
    {
        try
        {
            var personalRoomName = (await membersRepository.GetPersonalRoomNameFromDbAsync(memberId, CancellationToken.None)).Value;

            if (personalRoomName is null)
                return BaseResult<string>.Fail(
                    new(
                        ErrorCodes.VARIABLE_IS_NULL, 
                        "personalRoomName является null"));

            return BaseResult<string>.Success(personalRoomName);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке получить имя персональной комнаты для пользователя {MemberId}",
                memberId);

            return BaseResult<string>.Fail(
                new(
                    ErrorCodes.ENTERNAL_ERROR,
                    exception.Message));
        }
    }
    public async Task<BaseResult<SocketGuildChannel>> GetGuildChannelAsync(ulong channelId)
    {
        try 
        {
            if (await discordService.DiscordClient.GetChannelAsync(channelId) is not SocketGuildChannel channel)
                return BaseResult<SocketGuildChannel>.Fail(
                    new(
                        ErrorCodes.NO_ERROR,
                        "channel не является SocketGuildChannel"));

            return BaseResult<SocketGuildChannel>.Success(channel);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Ошибка при попытке получить канал {ChannelId}",
                channelId);

            return BaseResult<SocketGuildChannel>.Fail(
                new(
                    ErrorCodes.VARIABLE_IS_NULL,
                    exception.Message));
        }
    }
    public async Task<BaseResult<RestVoiceChannel>> CreateVoiceChannelAsync(ulong guildMemberId)
    {
        try
        {
            var guild = discordService.DiscordClient.GetGuild(providersHub.DiscordConfig.GuildId);
            var leader = guild.GetUser(guildMemberId);

            if (leader is null)
                return BaseResult<RestVoiceChannel>.Fail(
                    new(
                        ErrorCodes.VARIABLE_IS_NULL,
                        "leader является null"));

            var voiceChannel = await guild.CreateVoiceChannelAsync(
                name: $"🔉 | {await GetPersonalRoomNameAsync(guildMemberId)}",
                func: properties =>
                {
                    properties.CategoryId = providersHub.Categories.AutoCategoryId;
                    properties.Bitrate = 64000;
                    properties.PermissionOverwrites = new Overwrite[]
                    {
                        new(
                            providersHub.Roles.GetRoleByKey("PlayersClub").Value.Id,
                            PermissionTarget.Role,
                            new OverwritePermissions(
                                connect: PermValue.Allow,
                                sendMessages: PermValue.Allow,
                                manageChannel: PermValue.Deny)
                        ),
                        new(
                            leader.Id,
                            PermissionTarget.User,
                            new OverwritePermissions(manageChannel: PermValue.Allow)
                        )
                    };
                }
            );

            if(voiceChannel is null)
            {
                logger.LogWarning(
                    "Ошибка при создании голосовой комнаты для пользователя {GuildMemberId}",
                    guildMemberId);

                return BaseResult<RestVoiceChannel>.Fail(
                    new(
                         ErrorCodes.VARIABLE_IS_NULL,
                         "voiceChannel является null"));
            }

            return BaseResult<RestVoiceChannel>.Success(voiceChannel);

        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Ошибка при попытке создать канал для пользователя {MemberId}",
                guildMemberId);

            return BaseResult<RestVoiceChannel>.Fail(
                new(
                    ErrorCodes.ENTERNAL_ERROR,
                    exception.Message));
        }
    }
}