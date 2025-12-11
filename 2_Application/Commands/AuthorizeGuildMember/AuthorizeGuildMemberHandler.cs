using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Managers;
using MlkAdmin._2_Application.DTOs.Responses.Specialized;

namespace MlkAdmin._2_Application.Commands.Autorize;

public class AuthorizeGuildMemberHandler(
	ILogger<AuthorizeGuildMemberHandler> logger,
	IGuildMembersManager membersManager) : IRequestHandler<AuthorizeGuildMember, GuildMemberAuthorizationReponse>
{
    public async Task<GuildMemberAuthorizationReponse> Handle(AuthorizeGuildMember request, CancellationToken token)
    {
		try
		{
			await membersManager.AuthorizeGuildMemberAsync(request.MemberId);

			logger.LogInformation(
				"Успешная авторизация пользователя {MemberId}",
				request.MemberId);

			return new GuildMemberAuthorizationReponse()
			{
				IsSuccess = true,
				Error = string.Empty,
				Message = $"Успешная авторизация пользователя"
			};

        }
		catch (Exception exception)
		{
			logger.LogError(
				"Ошибка при попыткае авторизаовать пользователя {MemberId}. Ошибка: {Error}",
				request.MemberId,
				exception);

			return new GuildMemberAuthorizationReponse()
			{
				Error = "INTERNAL_ERROR",
				IsSuccess = false,
				Message = "Во время авторизации произошла непредвиденная ошибка"
			};
		}
    }
}
