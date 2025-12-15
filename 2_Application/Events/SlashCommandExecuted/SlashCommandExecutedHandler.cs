using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._2_Application.Commands.Autorize;
using MlkAdmin._2_Application.Commands.LobbyName;
using MlkAdmin._2_Application.Commands.PersonalVoiceChannelName;
using MlkAdmin._2_Application.Commands.UserStat;
using MlkAdmin._2_Application.DTOs.Discord.Embed;
using MlkAdmin._2_Application.DTOs.Responses;
using MlkAdmin._3_Infrastructure.Discord.Extensions;

namespace MlkAdmin._2_Application.Events.SlashCommandExecuted;

public class SlashCommandExecutedHandler(
    ILogger<SlashCommandExecutedHandler> logger,
    IMediator mediator,
    IJsonProvidersHub providersHub,
    EmbedMessageExtension embedMessageExtension) : INotificationHandler<SlashCommandExecuted>
{
    public async Task Handle(SlashCommandExecuted notification, CancellationToken token)
    {
        var command = notification.SocketSlashCommand;

        if (command.Channel.Id != providersHub.Channels.BotCommandsTextChannelId)
        {
            await notification.SocketSlashCommand.RespondAsync(
                embed: embedMessageExtension.CreateEmbed(new GuildMessageEmbedDto()
                {
                    Description = $"Команды бота можно вызывать только в канале {providersHub.Channels.BotCommandsTextChannelLink}.",
                    Color = new(220, 20, 60)
                }), ephemeral: true);

            return;
        }

        switch (command.CommandName)
        {
            case "set_lobby":
                try
                {
                    var nameOption = command.Data.Options.FirstOrDefault(x => x.Name.Equals("name", StringComparison.Ordinal));

                    if (nameOption?.Value is null)
                    {
                        logger.LogWarning(
                            "Отсутствует параметр \"name\" в команде set_lobby для пользователя {userId}",
                            command.User.Id);

                        return;
                    }

                    var lobbyName = nameOption.Value.ToString()?.Trim();

                    if (string.IsNullOrEmpty(lobbyName))
                    {
                        logger.LogWarning(
                            "Пустое значение \"name\" в команде set_lobby для пользователя {userId}",
                            command.User.Id);

                        return;
                    }

                    var setLobbyResponse = await mediator.Send(
                        new PersonalVChannelName()
                        {
                            MemberId = command.User.Id,
                            PersonalRoomName = lobbyName,
                        },
                        token
                    );

                    await command.RespondAsync(
                        embed: embedMessageExtension.CreateEmbed(
                            new GuildMessageEmbedDto()
                            {
                                Description = setLobbyResponse.Message,
                                Color = setLobbyResponse.IsSuccess ? Discord.Color.Green : Discord.Color.Red
                            }),
                        ephemeral: false
                    );

                    break;
                }
                catch (Exception exception)
                {
                    logger.LogError(
                        exception,
                        "Не удалось обработать команду set_lobby для пользователя {UserId}. Ошибка: {ErrorMessage}",
                        command?.User.Id,
                        exception.Message
                    );

                    throw;
                }

            case "user_stat":

                ulong targetUserId = notification.SocketSlashCommand.User.Id;
                string targetUserName = notification.SocketSlashCommand.User.GlobalName;

                if (notification.SocketSlashCommand.Data.Options.Any(x => x.Name == "user"))
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

                UserStatResponse userStatResponse = await mediator.Send(new GuildMemberStatistic()
                {
                    UserId = targetUserId,
                }, new());

                await notification.SocketSlashCommand.RespondAsync(embed: embedMessageExtension.CreateEmbed(new GuildMessageEmbedDto()
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

                if (notification.SocketSlashCommand.User is SocketGuildUser user && !user.GuildPermissions.Administrator)
                {
                    await notification.SocketSlashCommand.RespondAsync(
                        embed: embedMessageExtension.CreateEmbed(
                            new()
                            {
                                Description = "Данную команду могут вызывать только администраторы сервера",
                                Color = Discord.Color.Red
                            }), 

                        ephemeral: true);

                    return;
                }

                var response = await mediator.Send(
                    new AuthorizeGuildMember()
                    {
                        MemberId = command.User.Id
                    }, 
                    
                    new()
                );

                await notification.SocketSlashCommand.RespondAsync(
                    embed: embedMessageExtension.CreateEmbed(new()
                    {
                        Description = response.Message,
                        Color = new Discord.Color(87, 206, 255)
                    }), 
                    ephemeral: true
                );

                break;

            default:
                await notification.SocketSlashCommand.RespondAsync(
                    embed: embedMessageExtension.CreateEmbed(new GuildMessageEmbedDto()
                    {
                        Description = "Команда пока что в разработке",
                        Color = Discord.Color.Red
                    }),
                    ephemeral: true
                );

            break;
        }
    }
}
