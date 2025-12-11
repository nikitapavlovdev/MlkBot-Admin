using MediatR;
using MlkAdmin._2_Application.DTOs.Responses.Abstraction;
using MlkAdmin._2_Application.DTOs.Responses.Specialized;

namespace MlkAdmin._2_Application.Commands.UserStat;

public class GuildMemberStatistic : IRequest<BaseResult<GuildMemberStatisticDto>>
{
    public ulong MemberId { get; set; }
}
