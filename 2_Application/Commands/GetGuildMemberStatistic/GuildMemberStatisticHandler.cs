using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Mappers;
using MlkAdmin._1_Domain.Interfaces.Services;
using MlkAdmin._2_Application.DTOs.Responses.Abstraction;
using MlkAdmin._2_Application.DTOs.Responses.Specialized;

namespace MlkAdmin._2_Application.Commands.UserStat;

public class GuildMemberStatisticHandler(
    ILogger<GuildMemberStatisticHandler> logger,
    IGuildMemberService memberService,
    IGuildMemberStatsMapper guildMemberStatsMapper) : IRequestHandler<GuildMemberStatistic, BaseResult<GuildMemberStatisticDto>>
{
    public async Task<BaseResult<GuildMemberStatisticDto>> Handle(GuildMemberStatistic request, CancellationToken cancellationToken)
    {
        try
        {
            var memberStats = await memberService.GetGuildMemberStatisticsAsync(request.MemberId);

            if(memberStats is null)
            {
                logger.LogWarning(
                    "null поле membersStats после попытки маппинга");

                return BaseResult<GuildMemberStatisticDto>.Fail(new Error("101", "null поле membersStats после попытки маппинга", new Exception()));
            }

            return BaseResult<GuildMemberStatisticDto>.Success(await guildMemberStatsMapper.MapToDto(memberStats));
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Ошибка при попытке получить статистику по участнику {MemberId}",
                request.MemberId);

            return BaseResult<GuildMemberStatisticDto>.Fail(new Error("102", "Ошибка при попытке получить статистику по участнику {MemberId}", new Exception()));

        }
    }
}
