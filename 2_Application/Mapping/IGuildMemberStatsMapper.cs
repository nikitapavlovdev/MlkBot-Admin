using MlkAdmin.Shared.DTOs.GuildData;
using MlkAdmin.Shared.DTOs.GuildMessages;

namespace MlkAdmin._1_Domain.Interfaces.Mappers;

public interface IGuildMemberStatsMapper
{
    Task<GuildMemberStatisticDto> MapToDto (GuildMemberStatsData dto); 
}
