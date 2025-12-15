using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._2_Application.Managers.Discord;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Managers.Messages;

public class DynamicMessageManager(
    ILogger<DynamicMessageManager> logger,
    IJsonProvidersHub providersHub,
    IGuildChannelsService channelsService,
    IEmbedService embedService,
    ComponentsManager componentsManager) 
{
    private async Task SendOrUpdateAsync(
        GuildDynamicMessageDto DynamicMessageDto, 
        GuildMessageEmbedDto embedDto, 
        MessageComponent? messageComponent = null)
    {
        try
        {
            var response = await channelsService.GetGuildChannelAsync(DynamicMessageDto.ChannelId);
            var channel = response.Value as SocketTextChannel;

            if (await channel.GetMessageAsync(DynamicMessageDto.MessageId) is IUserMessage sentMessage)
            {
                await sentMessage.ModifyAsync(
                    async message =>
                    {
                        message.Embed = await embedService.BuildEmbedAsync(embedDto);
                        message.Components = messageComponent;
                    }
                );
            }
            else
            {
                await channel.SendMessageAsync(
                    embed: await embedService.BuildEmbedAsync(embedDto), 
                    components: messageComponent);
            } 
        }
        catch (Exception exception)
        {
            logger.LogError(
                "Ошибка при попытке оправить или обновить сообщение {DynamicMessage}. Ошибка {Error}",
                DynamicMessageDto.MessageId,
                exception.Message);

            return;
        }
    }

    public async Task SendMessageWithAutorization()
    {
        await SendOrUpdateAsync(
            new GuildDynamicMessageDto()
            {
                ChannelId = providersHub.Channels.HubTextChannelId,
                MessageId = providersHub.DynamicMessage.AutorizationMessageId
            },
            await embedService.GetEmbedDto(DynamicMessageType.Authorization)
        );
    }

    public async Task SendMessageWithRules()
    {
        await SendOrUpdateAsync(
            new GuildDynamicMessageDto()
            {
                ChannelId = providersHub.Channels.RulesTextChannelId,
                MessageId = providersHub.DynamicMessage.RulesMessageId
            },
            await embedService.GetEmbedDto(DynamicMessageType.Rules)
        );
    }

    public async Task SendMessageWithGuildRoles()
    {
        await SendOrUpdateAsync(
            new GuildDynamicMessageDto()
            {
                ChannelId = providersHub.Channels.RolesTextChannelId,
                MessageId = providersHub.DynamicMessage.GeneralRolesMessageId
            },
            await embedService.GetEmbedDto(DynamicMessageType.Roles)
        );
    }

    public async Task SendMessageWithNameColor()
    {
        await SendOrUpdateAsync(
            new GuildDynamicMessageDto()
            {
                ChannelId = providersHub.Channels.RolesTextChannelId,
                MessageId = providersHub.DynamicMessage.ColorNicknameMessageId
            },

            await embedService.GetEmbedDto(DynamicMessageType.NameColor), 
            await componentsManager.CreateMessageComponent(DynamicMessageType.NameColor)

        );
    }
}