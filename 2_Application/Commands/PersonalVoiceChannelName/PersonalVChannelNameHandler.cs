using MediatR;
using MlkAdmin.Shared.Results;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Users;
using MlkAdmin._1_Domain.Enums;
using MlkAdmin._2_Application.Commands.PersonalVoiceChannelName;

namespace MlkAdmin._2_Application.Commands.LobbyName;

public class PersonalVChannelNameHandler(
    ILogger<PersonalVChannelNameHandler> logger,
    IGuildMembersRepository membersRepository) : IRequestHandler<PersonalVChannelName, BaseResult>
{
    public async Task<BaseResult> Handle(PersonalVChannelName request, CancellationToken token)
    {
        try
        {
            await membersRepository.UpdatePersonalRoomNameAsync(request.MemberId, request.PersonalRoomName, token);

            logger.LogInformation(
                "Успешное изменения имени персональной комнаты участника {MemberId} на \"{RoomName}\"",
                request.MemberId,
                request.PersonalRoomName);

            return BaseResult.Success(
                "Успешное изменения имени персональной комнаты участника {MemberId} на \\\"{RoomName}\\\"\""); 
            
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке записать имя личной комнаты участника сервера {MemberId}. Ошибка: {Error}",
                request.MemberId,
                exception.Message);

            return BaseResult.Fail(
                "Произошла ошибка :(", 
                new(
                    ErrorCodes.ENTERNAL_ERROR, 
                    exception.Message));
        }
    }
}
