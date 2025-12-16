using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Managers;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._3_Infrastructure.Interfaces;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Commands.Autorize;

public class AuthorizeGuildMemberHandler(
	ILogger<AuthorizeGuildMemberHandler> logger,
	IGuildMembersManager membersManager,
	IDiscordService discordService) : IRequestHandler<AuthorizeGuildMember, BaseResult>
{
    public async Task<BaseResult> Handle(AuthorizeGuildMember request, CancellationToken token)
    {
		try
		{
			var memberMention = await discordService.GetGuildMemberMentionByIdAsync(request.MemberId);

            await membersManager.AuthorizeGuildMemberAsync(
				request.MemberId,
                memberMention);

			logger.LogInformation(
				"Успешная авторизация пользователя {MemberId}",
				request.MemberId);

			return BaseResult.Success(
				$"Пользователь {memberMention} успешно авторизован");

        }
		catch (Exception exception)
		{
			logger.LogError(
				"Ошибка при попыткае авторизаовать пользователя {MemberId}. Ошибка: {Error}",
				request.MemberId,
				exception);

			return BaseResult.Fail(
				$"Непредвиденная ошибка при попытке авторизации пользователя с Id {request.MemberId}", 
				new(
					ErrorCodes.ENTERNAL_ERROR, 
					exception.Message));
		}
    }
}
