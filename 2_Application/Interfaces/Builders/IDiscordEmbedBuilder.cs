using Discord;
using MlkAdmin.Shared.DTOs.GuildMessages;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._1_Domain.Interfaces.Discord;

public interface IDiscordEmbedBuilder
{
    Task<BaseResult<Embed>> BuildEmbedAsync(GuildMessageEmbedDto embedDto);
}