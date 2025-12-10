using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.DTOs.Responses;
using MlkAdmin._2_Application.Managers.UserManagers;

namespace MlkAdmin._2_Application.Commands.Autorize;

public class AuthorizeGuildMemberHandler(ILogger<AuthorizeGuildMemberHandler> logger, AutorizationManager auManager) : IRequestHandler<AuthorizeGuildMember, AuResponse>
{
    public async Task<AuResponse> Handle(AuthorizeGuildMember command, CancellationToken token)
    {
		try
		{
			await auManager.AuthorizeUser(command.User);

			return new()
			{
				IsSuccess = true,
				Status = "Успех",
				Message = $"Пользователь {command.User.Mention} успешно авторизован на сервере"
			};

        }
		catch (Exception ex)
		{
			logger.LogError(ex, "Ошибка при попытки авторизовать пользователя");
			return new()
			{
				IsSuccess = false,
				Error = ex.Message,
				Status = "Ошибка",
				Message = "Ошибка при попытке авторизовать пользователя"
			};
		}
    }
}
