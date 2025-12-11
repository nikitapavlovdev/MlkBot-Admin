using MediatR;
using MlkAdmin._2_Application.DTOs.Responses.Abstraction;
using MlkAdmin._2_Application.DTOs.Responses.Specialized;

namespace MlkAdmin._2_Application.Commands.Autorize;

public class AuthorizeGuildMember : IRequest<GuildMemberAuthorizationReponse>
{
    public ulong MemberId { get; set; }
}
