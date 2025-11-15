using MediatR;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.Commands.Autorize;
using MlkAdmin._2_Application.Commands.LobbyName;
using MlkAdmin._2_Application.Commands.UserStat;
using MlkAdmin._2_Application.DTOs.Discord.Embed;
using MlkAdmin._2_Application.DTOs.Discord.Responses;
using MlkAdmin._2_Application.DTOs.Responses;
using MlkAdmin._3_Infrastructure.Discord.Extensions;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;

namespace MlkAdmin._2_Application.Events.SlashCommandExecuted
{
    public class SlashCommandExecutedHandler(
		ILogger<SlashCommandExecutedHandler> logger,
		IMediator mediator, 
		JsonDiscordChannelsMapProvider mapProvider,
        EmbedMessageExtension embedMessageExtension) : INotificationHandler<SlashCommandExecuted>
    {
        public async Task Handle(SlashCommandExecuted notification, CancellationToken cancellationToken)
        {
			try
			{
				if(notification.SocketSlashCommand.Channel.Id != mapProvider.BotCommandChannelId)
				{
					await notification.SocketSlashCommand.RespondAsync(
						embed: embedMessageExtension.CreateEmbed(new EmbedDto() 

                        {
                            Description = $"Команды бота можно вызывать только в канале {mapProvider.BotCommandChannelHttps}.",
                            Color = Discord.Color.Red
                        }),
						ephemeral: true);

					return;
				}

				switch (notification.SocketSlashCommand.CommandName)
				{
					case "set_lobby":

						LobbyNameResponse lobbyNameResponse = await mediator.Send(new LobbyNameCommand()
						{
							LobbyName = notification.SocketSlashCommand.Data.Options.FirstOrDefault(x => x.Name == "name").Value.ToString() ?? string.Empty,
							UserId = notification.SocketSlashCommand.User.Id
						}, new());

                        await notification.SocketSlashCommand.RespondAsync(
							embed: embedMessageExtension.CreateEmbed(new EmbedDto()
							{
								Description = lobbyNameResponse.Message,
                                Color = lobbyNameResponse.IsSuccess ? Discord.Color.Green : Discord.Color.Red
                            }), 
							ephemeral: false);
                        break;

					case "user_stat":

						ulong targetUserId = notification.SocketSlashCommand.User.Id;
						string targetUserName = notification.SocketSlashCommand.User.GlobalName;

						if(notification.SocketSlashCommand.Data.Options.Any(x => x.Name == "user"))
						{
                            if (notification.SocketSlashCommand.Data.Options.FirstOrDefault(x => x.Name == "user").Value is not SocketGuildUser optionUser || optionUser.IsBot)
                            {
                                await notification.SocketSlashCommand.RespondAsync(embed: embedMessageExtension.CreateEmbed(new()
                                {
                                    Description = "Пользователь не найден, либо пользователь бот",
                                    Color = Discord.Color.Gold
                                }), ephemeral: true);

                                return;
                            }

                            targetUserId = optionUser.Id;
							targetUserName = optionUser.GlobalName;
						}

						UserStatResponse userStatResponse = await mediator.Send(new UserStatCommand()
						{
							UserId = targetUserId,
						}, new());

						await notification.SocketSlashCommand.RespondAsync(embed: embedMessageExtension.CreateEmbed(new EmbedDto()
						{
							Description = $"Общая статистика {targetUserName}\n" +
							$"> Сообщений отправлено: **{(userStatResponse.MessageCount == -1 ? "-" : userStatResponse.MessageCount)}**\n" +
							$"> Времени в голосовом канале: **{(userStatResponse.TotalSeconds != -1 ? userStatResponse.TotalSeconds / 3600 : 0)}h " +
							$"{(userStatResponse.TotalSeconds != -1 ? userStatResponse.TotalSeconds % 3600 / 60 : 0)}m " +
							$"{(userStatResponse.TotalSeconds != -1 ? userStatResponse.TotalSeconds % 3600 % 60 : 0)}s**",
							Color = new(87, 206, 255)

						}), ephemeral: true);

						break;

					case "authorize":
						
						if(notification.SocketSlashCommand.User is SocketGuildUser user && !user.GuildPermissions.Administrator)
						{
							await notification.SocketSlashCommand.RespondAsync(embed: embedMessageExtension.CreateEmbed(new()
							{
								Description = "Данную команду могут вызывать только администраторы сервера",
								Color = Discord.Color.Red
							}), ephemeral: true);

							return;
						}

						AuResponse response = await mediator.Send(new AutorizeCommand()
						{
							User = notification.SocketSlashCommand.Data.Options.FirstOrDefault(x => x.Name == "user").Value as SocketGuildUser
						}, new());

						await notification.SocketSlashCommand.RespondAsync(embed: embedMessageExtension.CreateEmbed(new()
						{
							Description = response.Message,
							Color = new Discord.Color(87, 206, 255)
						}), ephemeral: true);

						break; 

					default:
						await notification.SocketSlashCommand.RespondAsync(
						embed: embedMessageExtension.CreateEmbed(new EmbedDto()
						{
							Description = "Команда пока что в разработке",
								Color = Discord.Color.Red
							}),
						ephemeral: true);
						break;
			}
		}
		catch (Exception ex)
		{
				logger.LogError("Ошибка в классе SlashCommandExecutedHandler: {ex}", ex.Message);
		}
    }
}
