using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Channels;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._1_Domain.Interfaces.Messages;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Services.Messages;

public class GuildMessageService(
    ILogger<GuildMessageService> logger,
    IGuildChannelsService channelsService,
	IEmbedService embedService) : IGuildMessagesService
{
    public async Task SendMessageInChannelAsync(ulong channelId, GuildMessageDto content)
    {
		try
		{
            var result = await channelsService.GetGuildChannelAsync(channelId);

			if (!result.IsSuccess)
			{
				logger.LogWarning(
					"Произошла ошибка при попытке получить канал. Код ошибки: {ErrorCode}", result.Error.Code);

				return;
			}

			var channel = result.Value;

			if(channel is not SocketTextChannel textChannel)
			{
                logger.LogWarning(
                    "Канал с Id {ChannelId} не найден или не является текстовым", channelId);

                return;
            }


			await textChannel.SendMessageAsync(
				embed: await embedService.BuildEmbedAsync(
					new()
					{
						Title = content.Title,
						Description = content.Description
					}
				)
			);
			
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
