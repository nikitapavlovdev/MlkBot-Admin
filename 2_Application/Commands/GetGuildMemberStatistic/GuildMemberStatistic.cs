using MediatR;
using MlkAdmin.Shared.DTOs.GuildMessages;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Commands.GetGuildMemberStatistic;

public class GuildMemberStatistic : IRequest<BaseResult<GuildMemberStatisticDto>>
{
    public ulong MemberId { get; set; }
}
