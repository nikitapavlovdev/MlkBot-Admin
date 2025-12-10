using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Entities;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._2_Application.Managers.Channels.VoiceChannels;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Services.Channels;

public class GuildChannelsService(
    ILogger<GuildChannelsService> logger,
    IJsonProvidersHub providersHub,
    IGuildChannelsRepository channelsRepository
    DiscordSocketClient client) : IGuildChannelsService
{
    private async Task<string> GetPersonalLobbyNameAsync(ulong userId)
    {
        try
        {
            var user = await userRepository.GetDbUserAsync(userId);

            if (user != null)
            {
                if (user.LobbyName != string.Empty && user.LobbyName != null)
                {
                    return user.LobbyName;
                }
            }

            return "ᴍʟᴋ_ʟᴏʙʙʏ";
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "");

            throw;
        }
        

    }
    public async Task<SocketChannel?> GetChannelAsync(ulong channelId)
    {
        try 
        {
            return await client.GetChannelAsync(channelId) as SocketChannel;
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "");

            throw;
        }
    }
    public async Task<RestVoiceChannel> CreateVoiceChannelAsync(SocketGuild socketGuild, SocketUser socketUser)
    {
        SocketGuildUser? leader = socketUser as SocketGuildUser;

        return await socketGuild.CreateVoiceChannelAsync(
            name: $"🔉 | {await GetPersonalLobbyNameAsync(socketUser.Id)}",
            func: properties =>
            {
                properties.CategoryId = providersHub.Categories.AutoCategoryId;
                properties.Bitrate = 64000;
                properties.PermissionOverwrites = new Overwrite[]
                {
                        new(
                            jsonDiscordRolesProvider.RootDiscordRoles.GeneralRole.Categories.Gamer.Id,
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
    }
    public async Task UpsertGuildVoiceChannelsAsync(ulong guildId)
    {
        try
        {
            var guild = client.GetGuild(guildId);
            if (guild is null) return;

            foreach (SocketVoiceChannel vChannel in guild.VoiceChannels)
            {
                GuildVoiceChannel dbVoiceChannel = new()
                {
                    DiscordId = vChannel.Id,
                    Name = vChannel.Name,
                    Category = vChannel.Category.Name ?? string.Empty,
                    IsGen = vChannel.Id == jsonDiscordChannelsMapProvider.AutoGameLobbyId,
                    IsTemp = vChannel.Category.Id == jsonDiscordCategoriesProvider.AutoLobbyCategoryId && vChannel.Id != jsonDiscordChannelsMapProvider.AutoGameLobbyId
                };

                await channelsRepository.UpsertDbVoiceChannelAsync(dbVoiceChannel);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "");

            throw;
        }
    }
}