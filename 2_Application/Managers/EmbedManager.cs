using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.Discord;
using MlkAdmin._3_Infrastructure.Cache;
using MlkAdmin._3_Infrastructure.Providers.JsonProvider;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Managers.Discord
{
    public class EmbedManager(
        EmbedDescriptionsCache embedDescriptionsCache,
        JsonPicturesProvider jsonDiscordPicturesProvider) : IEmbedDtoCreator
    {
        public Task<GuildMessageEmbedDto> GetEmbedDto(DynamicMessageType type)
        {
            return Task.FromResult(type switch
            {
                DynamicMessageType.Authorization => new GuildMessageEmbedDto()
                {
                    Title = "Авторизация",
                    Description = embedDescriptionsCache.GetDescriptionForAutorization(),
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForAuMessageLink
                },

                DynamicMessageType.Rules => new GuildMessageEmbedDto()
                {
                    Title = "Правила",
                    Description = embedDescriptionsCache.GetDescriptionForRules(),
                    PicturesUrl = jsonDiscordPicturesProvider.PinterestPictureForRulesLink
                },

                DynamicMessageType.Roles => new GuildMessageEmbedDto()
                {
                    Title = "Роли",
                    //Description = embedDescriptionsCache.GetDiscriptionForMainRoles(), 
                    Description = embedDescriptionsCache.GetDiscriptionForMainRolesNewVers()
                },

                DynamicMessageType.NameColor => new GuildMessageEmbedDto()
                {
                    Title = "Цвет имени",
                    Description = embedDescriptionsCache.GetDescriptionForNameColor(),
                },

                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown type: {type}")
            });
        }
    }
}