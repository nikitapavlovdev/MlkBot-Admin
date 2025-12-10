using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._2_Application.DTOs.Responses;

namespace MlkAdmin._2_Application.Commands.UserStat;

public class GuildMemberStatisticHandler(
    IUserVoiceSessionRepository userVoiceSessionRepository,
    IUserMessageRepository userMessageRepository,
    ILogger<GuildMemberStatisticHandler> logger) : IRequestHandler<GuildMemberStatistic, UserStatResponse>
{
    public async Task<UserStatResponse> Handle(GuildMemberStatistic command, CancellationToken cancellationToken)
    {
        try
        {
            int mesCount = await userMessageRepository.GetMessagesNumberAsync(command.UserId);
            long totalSeconds = await userVoiceSessionRepository.GetVoiceSpendTimeAsync(command.UserId);

            return new UserStatResponse
            {
                IsSuccess = true,
                Message = $"Статистика по пользователю {command.UserId}",
                MessageCount = mesCount,
                TotalSeconds = totalSeconds,
                Status = "Успех"
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при попытке получить статистику по пользователю");

            return new UserStatResponse
            {
                Error = ex.Message,
                Message = ex.Message,
                IsSuccess = false,
                MessageCount = -1,
                Status = "Ошибка",
                TotalSeconds = -1
            };
        }
    }
}
