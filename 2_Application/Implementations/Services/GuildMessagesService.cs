using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Implementations.Services;

public class GuildMessagesService(
    ILogger<GuildMessagesService> logger,
    IGuildChannelsService channelsService,
	IDiscordEmbedBuilder embedBuilder) : IGuildMessagesService
{
    public async Task SendMessageInChannelAsync(ulong channelId, GuildMessageDto content)
    {
		try
		{
            var result = await channelsService.GetGuildChannelAsync(channelId);

			if (!result.IsSuccess)
			{
				logger.LogWarning(
					"Произошла ошибка при попытке получить канал. Код ошибки: {ErrorCode}", result.Error.Details);

				return;
			}

			var channel = result.Value;

			if(channel is not SocketTextChannel textChannel)
			{
                logger.LogWarning(
                    "Канал с Id {ChannelId} не найден или не является текстовым", channelId);

                return;
            }

			var embed = (await embedBuilder.BuildEmbedAsync(
				new()
				{
					Title = content.Title,
					Description = content.Description
				})).Value;


            await textChannel.SendMessageAsync(embed: embed);
			
        }
		catch (Exception exception)
		{
			logger.LogError(
				exception, 
				"Ошибка при попытке отправить сообщение в канал {ChannelId}. Ошибка {Exception}",
				channelId,
				exception.Message);

			return;
		}
    }
	public async Task UpdateAllDynamicMessagesAsync()
	{
		try
		{

		}
		catch (Exception exception)
		{
			logger.LogError(
				"Ошибка при попытке обновить динамические сообщения. Ошибка {Error}",
				exception.Message);

			return;
		}
	}
}
