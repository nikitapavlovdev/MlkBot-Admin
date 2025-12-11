using MlkAdmin._1_Domain.Models;

namespace MlkAdmin._1_Domain.Interfaces.Services;

public interface IGuildMemberService
{
    Task UpdatePersonalRoomNameAsync(ulong memberId, string roomName, CancellationToken token);
    Task<GuildMemberStatsData> GetGuildMemberStatisticsAsync(ulong memberId); 

}
