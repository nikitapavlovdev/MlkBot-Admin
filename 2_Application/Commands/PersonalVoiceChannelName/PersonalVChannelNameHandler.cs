using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Services;
using MlkAdmin._2_Application.Commands.PersonalVoiceChannelName;
using MlkAdmin._2_Application.DTOs.Responses.Specialized;

namespace MlkAdmin._2_Application.Commands.LobbyName;

public class PersonalVChannelNameHandler(
    ILogger<PersonalVChannelNameHandler> logger,
    IGuildMemberService memberService) : IRequestHandler<PersonalVChannelName, PersonalVChannelNameResponse>
{
    public async Task<PersonalVChannelNameResponse> Handle(PersonalVChannelName request, CancellationToken token)
    {
        try
        {
            await memberService.UpdatePersonalRoomNameAsync(request.MemberId, request.PersonalRoomName, token);

            logger.LogInformation(
                "Успешное изменения имени персональной комнаты участника {MemberId} на \"{RoomName}\"",
                request.MemberId,
                request.PersonalRoomName);

            return new PersonalVChannelNameResponse()
            {
                IsSuccess = true,
                Error = string.Empty,
                Message = $"Имя вашей комнаты успешно изменено на {request.PersonalRoomName}!"
            };
            
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке записать имя личной комнаты участника сервера {MemberId}. Ошибка: {Error}",
                request.MemberId,
                exception.Message);

            return new PersonalVChannelNameResponse()
            {
                IsSuccess = false, 
                Error = $"Ошибка при попытке записать имя личной комнаты",
                Message = $"Напишите разрабу, возможно починет!",
            };
        }
    }
}
