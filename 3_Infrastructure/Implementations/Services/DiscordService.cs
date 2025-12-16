using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._1_Domain.Interfaces.Providers;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._3_Infrastructure.Services;

public class DiscordService(
	ILogger<DiscordService> logger,
	IJsonProvidersHub providersHub,
	DiscordSocketClient discordSocketClient) : IDiscordService
{
	public DiscordSocketClient DiscordClient => discordSocketClient;
    public async Task<BaseResult<SocketGuildUser>> GetGuildMemberAsync(ulong memberId)
    {
		try
		{
			var guildMember = discordSocketClient.GetGuild(providersHub.DiscordConfig.GuildId).GetUser(memberId);

			if(guildMember is null)
			{
				logger.LogWarning(
					"Не удалось получить участника сервера");

				return BaseResult<SocketGuildUser>.Fail(
					new Error(
						ErrorCodes.VARIABLE_IS_NULL,
						"Переменная guildMember вернулась null"));
			}

			return BaseResult<SocketGuildUser>.Success(guildMember);
		}
		catch (Exception exception)
		{
			logger.LogError(
				exception,
				"Произошла ошибка при попытке получить участника сервера\nСообщение: {ErrorMessage}",
				exception.Message);

			return BaseResult<SocketGuildUser>.Fail(
				new Error(
					ErrorCodes.ENTERNAL_ERROR, 
					$"{exception.Message}"));
		}
    }
    public async Task<BaseResult<IReadOnlyCollection<SocketGuildUser>>> GetGuildMembersAsync()
    {
        try
        {
            var guildMembers = discordSocketClient.GetGuild(providersHub.DiscordConfig.GuildId).Users;

            if (guildMembers is null)
            {
                logger.LogWarning(
                    "Не удалось получить участников сервера");

                return BaseResult<IReadOnlyCollection<SocketGuildUser>>.Fail(
                    new Error(
                        ErrorCodes.VARIABLE_IS_NULL,
                        "Переменная guildMembers вернулась null"));
            }

            return BaseResult<IReadOnlyCollection<SocketGuildUser>>.Success(guildMembers);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Произошла ошибка при попытке получить участников сервера\nСообщение: {ErrorMessage}",
                exception.Message);

            return BaseResult<IReadOnlyCollection<SocketGuildUser>>.Fail(
                new Error(
                    ErrorCodes.ENTERNAL_ERROR,
                    $"{exception.Message}"));
        }
    }
    public async Task<string> GetGuildMemberMentionByIdAsync(ulong memberId)
    {
        return discordSocketClient.GetGuild(providersHub.DiscordConfig.GuildId).Users.FirstOrDefault(x => x.Id == memberId).Mention;
    }
    public SocketGuild GetGuild()
    {
        return discordSocketClient.GetGuild(providersHub.DiscordConfig.GuildId);
    }

}
