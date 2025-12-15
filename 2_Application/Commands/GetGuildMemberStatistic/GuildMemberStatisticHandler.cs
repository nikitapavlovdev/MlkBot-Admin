using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Mappers;
using MlkAdmin._1_Domain.Interfaces.Services;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin.Shared.Results;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Commands.UserStat;

public class GuildMemberStatisticHandler(
    ILogger<GuildMemberStatisticHandler> logger,
    IGuildMembersService memberService,
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

                return BaseResult<GuildMemberStatisticDto>.Fail(
                    new Error(
                        ErrorCodeEnums.VARIABLE_IS_NULL, 
                        "null поле membersStats после попытки маппинга")
                    );
            }

            return BaseResult<GuildMemberStatisticDto>.Success(
                await guildMemberStatsMapper.MapToDto(memberStats));
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception, 
                "Ошибка при попытке получить статистику по участнику {MemberId}",
                request.MemberId);

            return BaseResult<GuildMemberStatisticDto>.Fail(
                new Error(
                    ErrorCodeEnums.ENTERNAL_ERROR, 
                    $"Ошибка при попытке получить статистику по участнику {request.MemberId}")
                );
        }
    }
}
