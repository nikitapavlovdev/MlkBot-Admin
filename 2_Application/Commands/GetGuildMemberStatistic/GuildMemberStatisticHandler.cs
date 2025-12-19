using MediatR;
using MlkAdmin.Shared.Results;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._2_Application.Commands.GetGuildMemberStatistic;

public class GuildMemberStatisticHandler : IRequestHandler<GuildMemberStatistic, BaseResult<GuildMemberStatisticDto>>
{
    public async Task<BaseResult<GuildMemberStatisticDto>> Handle(GuildMemberStatistic request, CancellationToken cancellationToken)
    {
        return default;
    }
}
