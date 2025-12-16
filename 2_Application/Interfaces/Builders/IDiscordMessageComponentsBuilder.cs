using Discord;
using MlkAdmin.Shared.DTOs.DiscordComponents;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Interfaces;

public interface IDiscordMessageComponentsBuilder
{
    Task<BaseResult<MessageComponent>> BuildSelectionMenuAsync(SelectionMenuConfigDto config);
}
