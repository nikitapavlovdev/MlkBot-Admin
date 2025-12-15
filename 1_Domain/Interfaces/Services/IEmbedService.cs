using Discord;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._1_Domain.Interfaces.Discord;

public interface IEmbedService
{
    Task<GuildMessageEmbedDto> GetEmbedDto(DynamicMessageType type);
    Task<Embed> BuildEmbedAsync(GuildMessageEmbedDto embedDto);
}