using MlkAdmin.Shared.DTOs.GuildData;

namespace MlkAdmin._1_Domain.Interfaces.Services;

public interface IGuildMembersService
{
    Task UpdatePersonalRoomNameAsync(ulong memberId, string roomName, CancellationToken token);
    Task<string> GetPersonalRoomNameFromDbAsync(ulong memberId);
    Task<GuildMemberStatsData> GetGuildMemberStatisticsAsync(ulong memberId); 

}
