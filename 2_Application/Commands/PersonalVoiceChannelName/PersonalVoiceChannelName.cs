using MediatR;
using MlkAdmin._2_Application.DTOs.Discord.Responses;

namespace MlkAdmin._2_Application.Commands.LobbyName 
{
    public class PersonalVoiceChannelName : IRequest<PersonalRoomResponse>
    {
        public ulong UserId { get; set; }
        public string PersonalRoomName { get; set; } = string.Empty;
    }
}
