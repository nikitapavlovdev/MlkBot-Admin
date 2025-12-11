using MediatR;
using MlkAdmin._2_Application.DTOs.Responses.Specialized;

namespace MlkAdmin._2_Application.Commands.PersonalVoiceChannelName;

public class PersonalVChannelName : IRequest<PersonalVChannelNameResponse>
{
    public ulong MemberId { get; set; }
    public string PersonalRoomName { get; set; } = string.Empty;
}
