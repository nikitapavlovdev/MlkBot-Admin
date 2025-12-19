using Discord;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin.Shared.Results;
using MlkAdmin._4_Presentation.Interfaces;

namespace MlkAdmin._4_Presentation.Discord;
public class DiscordSlashCommandsService(
    ILogger<DiscordSlashCommandsService> logger,
    IDiscordService discordService,
    IJsonProvidersHub providersHub) : IDiscordSlashCommandsService
{
    private List<SlashCommandProperties?> SlashGuildCommands { get; set; } = [];

    public async Task<BaseResult> RegistrateCommandsAsync()
    {
        try
        {
            await discordService.DiscordClient.Rest.BulkOverwriteGuildCommands([], providersHub.DiscordConfig.GuildId);

            SlashGuildCommands.Add(AddLobbyNameCommand());
            SlashGuildCommands.Add(GetUserStatistic());

            foreach (SlashCommandProperties? command in SlashGuildCommands)
                await discordService.DiscordClient.Rest.CreateGuildCommand(command, providersHub.DiscordConfig.GuildId);

            return BaseResult.Success("Команды успешно обновлены");

        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке добавить команды бота\nСообщение: {ErrorMessage}",
                exception.Message);

            return BaseResult.Fail(
                "Уп-с, произошла ошибка! Обратитесь к никитке..",
                new(
                    ErrorCodes.ENTERNAL_ERROR, 
                    exception.Message));
        }
    }
    private SlashCommandProperties? AddLobbyNameCommand()
    {
        return new SlashCommandBuilder()
            .WithName("set_lobby")
            .WithDescription("Настраивает имя для создаваемой вами личной комнаты.")
            .AddOption("name", ApplicationCommandOptionType.String, "Имя комнаты", isRequired: true)
            .Build();
    }

    private SlashCommandProperties? GetUserStatistic()
    {
        return new SlashCommandBuilder()
            .WithName("user_stat")
            .WithDescription("Получить статистику пользователя на сервере")
            .AddOption("user", ApplicationCommandOptionType.User, "Пользователь", isRequired: false)
            .Build();
    }
}
