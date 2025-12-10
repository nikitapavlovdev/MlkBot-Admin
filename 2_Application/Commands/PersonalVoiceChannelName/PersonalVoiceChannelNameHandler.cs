using MediatR;
using Microsoft.Extensions.Logging;
using MlkAdmin._1_Domain.Interfaces.Users;
using MlkAdmin._2_Application.DTOs.Discord.Responses;

namespace MlkAdmin._2_Application.Commands.LobbyName;

public class PersonalVoiceChannelNameHandler(
    ILogger<PersonalVoiceChannelNameHandler> logger,
    IGuildMemberRepository memberRepository) : IRequestHandler<PersonalVoiceChannelName, PersonalRoomResponse>
{
    public async Task<PersonalRoomResponse> Handle(PersonalVoiceChannelName request, CancellationToken cancellationToken)
    {
        try
        {
            var member = await memberRepository.GetDbGuildMemberAsync(request.UserId);
            member.PersonalRoomName = request.PersonalRoomName;

            await memberRepository.UpsertGuildMemberAsync(member);

            return new()
            {
                IsSuccess = true,
                Error = string.Empty,
                PersonalRoomName = request.PersonalRoomName,
                Message = "Лобби успешно сохранено",
                Status = "Успех",
            };
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при попытке записать имя личной комнаты участника сервера {MemberId}. Ошибка: {Error}",
                request.UserId,
                exception.Message);

            throw;
        }
    }
}
