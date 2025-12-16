using Discord;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin.Shared.DTOs.GuildMessages;
using MlkAdmin.Shared.Results;
using MlkAdmin._1_Domain.Enums;

namespace MlkAdmin._3_Infrastructure.Implementations.Builders;

public class DiscordEmbedBuilder(ILogger<DiscordEmbedBuilder> logger) : IDiscordEmbedBuilder
{
    public async Task<BaseResult<Embed>> BuildEmbedAsync(GuildMessageEmbedDto embedDto)
    {
        try
        {
            var embedBuilder = new EmbedBuilder()
                .WithTitle(embedDto.Title)
                .WithDescription(embedDto.Description)
                .WithColor(embedDto.Color);

            if (embedDto.AuthorDto is not null)
            {
                embedBuilder.WithAuthor(new EmbedAuthorBuilder()
                    .WithUrl(embedDto.AuthorDto.Url)
                    .WithName(embedDto.AuthorDto.Name)
                    .WithIconUrl(embedDto.AuthorDto.IconUrl));
            }

            if (embedDto.FooterDto is not null)
            {
                embedBuilder.WithFooter(new EmbedFooterBuilder()
                    .WithText(embedDto.FooterDto.Text)
                    .WithIconUrl(embedDto.FooterDto.IconUrl));
            }

            if (embedDto.WithTimestamp)
            {
                embedBuilder.WithCurrentTimestamp();
            }

            if (embedDto.EmbedFields is not null)
            {
                embedBuilder.WithFields(embedDto.EmbedFields);
            }

            logger.LogInformation(
                "Embed успешно сформирован");


            return BaseResult<Embed>.Success(embedBuilder.Build());
            
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке построить Embed");


            return BaseResult<Embed>.Fail(new(ErrorCodes.ENTERNAL_ERROR, "#затычка"));

        }
    }
}
