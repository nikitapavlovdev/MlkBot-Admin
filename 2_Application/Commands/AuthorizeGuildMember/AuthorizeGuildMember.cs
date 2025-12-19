using MediatR;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Commands.AuthorizeGuildMember;

public class AuthorizeGuildMember : IRequest<BaseResult>
{
    public ulong MemberId { get; set; }
}
