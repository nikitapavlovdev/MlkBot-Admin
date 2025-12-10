using MediatR;
using MlkAdmin._2_Application.DTOs.Responses;

namespace MlkAdmin._2_Application.Commands.UserStat;

public class GuildMemberStatistic : IRequest<UserStatResponse>
{
    public ulong UserId { get; set; }
}
