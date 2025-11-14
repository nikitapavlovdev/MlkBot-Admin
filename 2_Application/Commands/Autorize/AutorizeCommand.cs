using Discord;
using MediatR;
using MlkAdmin._2_Application.DTOs.Responses;

namespace MlkAdmin._2_Application.Commands.Autorize;

public class AutorizeCommand : IRequest<AuResponse>
{
    public IUser? User { get; set; }
}
