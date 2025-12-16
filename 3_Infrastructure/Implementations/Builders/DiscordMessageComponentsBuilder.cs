using Discord;
using MlkAdmin.Shared.Results;
using Microsoft.Extensions.Logging;
using MlkAdmin.Shared.DTOs.DiscordComponents;
using MlkAdmin._2_Application.Interfaces;
using MlkAdmin._1_Domain.Enums;

namespace MlkAdmin._3_Infrastructure.Services;

public class DiscordMessageComponentsBuilder(ILogger<DiscordMessageComponentsBuilder> logger) : IDiscordMessageComponentsBuilder
{
    public async Task<BaseResult<MessageComponent>> BuildSelectionMenuAsync(SelectionMenuConfigDto config)
    {
        try
        {
            var menu = new SelectMenuBuilder()
            .WithPlaceholder(config.Placeholder)
            .WithCustomId(config.CustomId);

            foreach (var option in config.Options)
                menu.AddOption(option.Label, option.Value);

            return BaseResult<MessageComponent>.Success(new ComponentBuilder().WithSelectMenu(menu).Build());
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке построить компонент сообщения\nСообщение: {ErrorMessage}",
                exception.Message);

            return BaseResult<MessageComponent>.Fail(
                new(
                    ErrorCodes.ENTERNAL_ERROR,
                    "Смотреть logger"));
        }
    }
}
