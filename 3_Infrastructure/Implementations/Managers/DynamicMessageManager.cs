using Discord;
using Discord.WebSocket;
using MlkAdmin.Shared.Results;
using Microsoft.Extensions.Logging;
using MlkAdmin.Shared.DTOs.GuildMessages;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._2_Application.Interfaces;

namespace MlkAdmin._2_Application.Managers.Messages;

public class DynamicMessageManager(
    ILogger<DynamicMessageManager> logger,
    IJsonProvidersHub providersHub,
    IGuildChannelsService channelsService,
    IDiscordEmbedBuilder embedBuilder,
    IDiscordMessageComponentsBuilder componentsBuilder) : IDynamicMessageManager
{
    public async Task<BaseResult> RefreshDynamicMessagesAsync()
    {
        await Task.WhenAll(
            SendMessageWithAutorization(),
            SendMessageWithRules(),
            SendMessageWithGuildRoles(),
            SendMessageWithNameColor());

        return BaseResult.Success(
            "Динамичные сообщение успешно обновлены");
    }

    private async Task SendOrUpdateAsync(
        GuildDynamicMessageDto DynamicMessageDto, 
        GuildMessageEmbedDto embedDto, 
        MessageComponent? messageComponent = null)
    {
        try
        {
            var response = await channelsService.GetGuildChannelAsync(DynamicMessageDto.ChannelId);
            var channel = response.Value as SocketTextChannel;

            var embed = (await embedBuilder.BuildEmbedAsync(embedDto)).Value;

            if (await channel.GetMessageAsync(DynamicMessageDto.MessageId) is IUserMessage sentMessage)
            {
                await sentMessage.ModifyAsync(
                    async message =>
                    {
                        message.Embed = embed;
                        message.Components = messageComponent;
                    }
                );
            }
            else
            {
                await channel.SendMessageAsync(
                    embed: embed, 
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

    private async Task SendMessageWithAutorization()
    {
        var dmDto = new GuildDynamicMessageDto()
        {
            ChannelId = providersHub.Channels.HubTextChannelId,
            MessageId = providersHub.DynamicMessage. AuthorizationMessageId
        };

        var embedDto = new GuildMessageEmbedDto()
        {

        };

        await SendOrUpdateAsync(dmDto, embedDto);
    }
    private async Task SendMessageWithRules()
    {
        var dmDto = new GuildDynamicMessageDto()
        {
            ChannelId = providersHub.Channels.RulesTextChannelId,
            MessageId = providersHub.DynamicMessage.RulesMessageId
        };
       
        var embedDto = new GuildMessageEmbedDto()
        {

        };

        await SendOrUpdateAsync(dmDto, embedDto);
    }
    private async Task SendMessageWithGuildRoles()
    {
        var dmDto = new GuildDynamicMessageDto()
        {
            ChannelId = providersHub.Channels.RolesTextChannelId,
            MessageId = providersHub.DynamicMessage.GeneralRolesMessageId
        };

        var embedDto = new GuildMessageEmbedDto()
        {

        };

        await SendOrUpdateAsync(dmDto, embedDto);
    }
    private async Task SendMessageWithNameColor()
    {
        var dmDto = new GuildDynamicMessageDto()
        {
            ChannelId = providersHub.Channels.RolesTextChannelId,
            MessageId = providersHub.DynamicMessage.ColorNicknameMessageId
        };

        var embedDto = new GuildMessageEmbedDto()
        {

        };

        var component = (await componentsBuilder.BuildSelectionMenuAsync(
            new()
            {
                Placeholder = "жʍяᴋни",
                CustomId = "GUILD_NAMECOLOR_CHANGE",
                Options = {
                    new() { Label = "💜", Value = "VioletColor"},
                    new() { Label = "💙", Value = "SlateblueColor"},
                    new() { Label = "🧡", Value = "CoralColor"},
                    new() { Label = "💛", Value = "KhakiColor"},
                    new() { Label = "💖", Value = "CrimsonColor"},
                    new() { Label = "💚", Value = "LimeColor"},
                    new() { Label = "ʀᴇᴍᴏᴠᴇ ᴄᴏʟᴏʀ", Value = "remove_color"} }
            })).Value;

        await SendOrUpdateAsync(dmDto, embedDto, component);
    }
}