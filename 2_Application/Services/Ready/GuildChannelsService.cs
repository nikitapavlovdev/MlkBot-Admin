using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Interfaces.Services;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Services.Channels;

public class GuildChannelsService(
    ILogger<GuildChannelsService> logger,
    IJsonProvidersHub providersHub,
    IGuildMembersService membersService,
    DiscordSocketClient client) : IGuildChannelsService
{
    private async Task<BaseResult<string>> GetPersonalRoomNameAsync(ulong memberId)
    {
        try
        {
            var personalRoomName = await membersService.GetPersonalRoomNameFromDbAsync(memberId);

            if (personalRoomName is null)
                return BaseResult<string>.Fail(new("103", "Переменная personalRoomName null", new()));

            return BaseResult<string>.Success(personalRoomName);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке получить имя персональной комнаты для пользователя {MemberId}",
                memberId);

            return BaseResult<string>.Fail(new("104", "ENTERNALL_ERROR", new()));
        }
    }
    public async Task<BaseResult<SocketGuildChannel>> GetGuildChannelAsync(ulong channelId)
    {
        try 
        {
            if (await client.GetChannelAsync(channelId) is not SocketGuildChannel channel)
                return BaseResult<SocketGuildChannel>.Fail(new("104", "Не удалось найти канал по id", new()));

            return BaseResult<SocketGuildChannel>.Success(channel);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Ошибка при попытке получить канал {ChannelId}",
                channelId);

            return BaseResult<SocketGuildChannel>.Fail(new("105", "ENTERNALL_ERROR", new()));
        }
    }
    public async Task<BaseResult<RestVoiceChannel>> CreateVoiceChannelAsync(ulong guildMemberId)
    {
        try
        {
            var guild = client.GetGuild(providersHub.DiscordConfig.GuildId);
            var leader = guild.GetUser(guildMemberId);

            if (leader is null)
                return BaseResult<RestVoiceChannel>.Fail(new("103", "Переменная leader null", new()));

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

                return BaseResult<RestVoiceChannel>.Fail(new("105", "Не удалось создать голосовую комнату", new()));
            }

            return BaseResult<RestVoiceChannel>.Success(voiceChannel);

        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Ошибка при попытке создать канал для пользователя {MemberId}",
                guildMemberId);

            return BaseResult<RestVoiceChannel>.Fail(new("105", "ENTERNALL_ERROR", new()));
        }
    }
}