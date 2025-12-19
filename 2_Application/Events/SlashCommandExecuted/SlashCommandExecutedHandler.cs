using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin.Shared.DTOs.GuildMessages;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._2_Application.Commands.PersonalVoiceChannelName;

namespace MlkAdmin._2_Application.Events.SlashCommandExecuted;

public class SlashCommandExecutedHandler(
    ILogger<SlashCommandExecutedHandler> logger,
    IMediator mediator,
    IJsonProvidersHub providersHub,
    IDiscordEmbedBuilder embedBuilder) : INotificationHandler<SlashCommandExecuted>
{
    public async Task Handle(SlashCommandExecuted notification, CancellationToken token)
    {
        var command = notification.SocketSlashCommand;

        if (command.Channel.Id != providersHub.Channels.BotCommandsTextChannelId)
        {
            var result = await embedBuilder.BuildEmbedAsync(
                new GuildMessageEmbedDto()
                {
                    Description = $"Команды бота можно вызывать только в канале {providersHub.Channels.BotCommandsTextChannelLink}.",
                    Color = new(220, 20, 60)
                }
            );

            var embed = result.Value;

            await command.RespondAsync(embed: embed, ephemeral: true);
        }

        switch (command.CommandName)
        {
            case "set_lobby":
                try
                {
                    var personalRoomName = command.Data.Options.FirstOrDefault(x => x.Name.Equals("name", StringComparison.Ordinal)).Value.ToString() ?? "👀";

                    var result = await mediator.Send(
                        new PersonalVChannelName()
                        {
                            MemberId = command.User.Id,
                            PersonalRoomName = personalRoomName,
                        },
                        token
                    );

                    var embed = (await embedBuilder.BuildEmbedAsync(
                        new GuildMessageEmbedDto()
                        {
                            Description = result.ClientMessage,
                            Color = result.IsSuccess ? Discord.Color.Green : Discord.Color.Red
                        })).Value;

                    await command.RespondAsync(embed: embed, ephemeral: false);

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
        }
    }
}
