using Discord;
using MlkAdmin.Shared.DTOs.GuildMessages;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Interfaces.Builders;

public interface IDiscordEmbedBuilder
{
    Task<BaseResult<Embed>> BuildEmbedAsync(GuildMessageEmbedDto embedDto);
}