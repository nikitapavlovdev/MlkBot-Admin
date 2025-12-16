using MediatR;
using MlkAdmin.Shared.Results;

namespace MlkAdmin._2_Application.Commands.PersonalVoiceChannelName;

public class PersonalVChannelName : IRequest<BaseResult>
{
    public ulong MemberId { get; set; }
    public string PersonalRoomName { get; set; } = string.Empty;
}
